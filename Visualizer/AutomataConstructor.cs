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

        private void ConstructAutomata(object sender, RoutedEventArgs e)
        {
            if (RuleSetConstructor.RuleSet.Count == 0)
            {
                MessageBox.Show("Rule set can't be empty", "Can't costruct an automata", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

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

            Automata automata = new Automata(cellSize, IsInfinite, neighborhood, ruleSet);
            Serialize(automata);

            MessageBox.Show("Automata has been constructed successfuly", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new AutomataVisualizer(cellSize, IsInfinite, ruleSet, neighborhood));
        }

        void Deserialize()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;

            using (StreamReader sr = new StreamReader("test.json"))
            {
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    Automata a = (Automata)serializer.Deserialize(sr, typeof(Automata));
                    RuleSetConstructor.RuleSet.Clear();

                    IsInfinite = a._isInfinite;

                    foreach (var item in new ObservableCollection<Rule>(new List<Rule>(a._ruleSet.Rules)))
                        RuleSetConstructor.RuleSet.Add(item);

                    NeighborhoodPicker.SelectedNeighborhood = a._neighborhood;

                    MessageBox.Show("Successful deserialization");
                }
            }
        }

        void Serialize(Automata automata)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;

            try
            {
                using (StreamWriter sw = new StreamWriter("test.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, automata);
                        MessageBox.Show("Success");
                    }

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadAutomata(object sender, RoutedEventArgs e)
        {
            Deserialize();
            RuleSetConstructor.UpdateViews();
        }
    }
}
