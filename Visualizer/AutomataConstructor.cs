using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CellularAutomata;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Visualizer
{
    /// <summary>
    /// Логика взаимодействия для AutomataConstructor.xaml
    /// </summary>
    public partial class AutomataConstructor : Page, INotifyPropertyChanged
    {
        public int Size { get; set; }

        private bool _isInfinite;
        public bool IsInfinite
        {
            get => _isInfinite;
            set
            {
                _isInfinite = value;
                OnPropertyChanged("IsInfinite");
            }
        }

        public AutomataConstructor()
        {
            DataContext = this;
            InitializeComponent();

            Size = 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        bool Deserialize()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;

            try
            {
                using (StreamReader sr = new StreamReader("test.json"))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        Automata automata = (Automata)serializer.Deserialize(sr, typeof(Automata));

                        IsInfinite = automata._isInfinite;
                        NeighborhoodPicker.SelectedNeighborhood = automata._neighborhood;

                        foreach (var item in new ObservableCollection<Rule>(new List<Rule>(automata._ruleSet.Rules)))
                            RuleSetConstructor.RuleSet.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при загрузке");
                return false;
            }

            return true;
        }

        Automata ConstructAutomata()
        {
            Rule[] ruleArray = new List<Rule>(RuleSetConstructor.RuleSet).ToArray();
            int[][] neighborhood = NeighborhoodPicker.SelectedNeighborhood;

            RuleSet ruleSet = new RuleSet(
                ruleArray,
                new Cell(),
                true
            );

            int cellSize = 8;

            switch (Size)
            {
                case 0:
                    cellSize = 16;
                    break;
                case 1:
                    cellSize = 8;
                    break;
                case 2:
                    cellSize = 6;
                    break;
            }

            return new Automata(cellSize, IsInfinite, neighborhood, ruleSet);
        }


        void Serialize(Automata automata)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;

            try
            {
                using (StreamWriter sw = new StreamWriter("test.json"))
                    using (JsonWriter writer = new JsonTextWriter(sw))                
                        serializer.Serialize(writer, automata);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show("Successfuly saved the automata", "Successfuly", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }


        async private void LoadAutomata(object sender, RoutedEventArgs e)
        {
            RuleSetConstructor.ClearRuleSet();
            Deserialize();
            await Task.Delay(1);
            RuleSetConstructor.UpdateViews();
        }


        private void SaveAutomata(object sender, RoutedEventArgs e)
        {
            if (RuleSetConstructor.RuleSet.Count == 0)
            {
                MessageBox.Show("Rule set can't be empty", "Can't costruct an automata", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Serialize(ConstructAutomata());
        }


        private void VisualizeAutomata(object sender, RoutedEventArgs e)
        {
            if (RuleSetConstructor.RuleSet.Count == 0)
            {
                MessageBox.Show("Rule set can't be empty", "Can't costruct an automata", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            NavigationService.Navigate(new AutomataVisualizer(ConstructAutomata()));
        }
    }
}
