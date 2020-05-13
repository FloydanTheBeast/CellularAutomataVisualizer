using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CellularAutomata;
using System.Windows;
using System.Collections.Generic;
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

        async private void LoadAutomata(object sender, RoutedEventArgs e)
        {
            RuleSetConstructor.ClearRuleSet();
            Automata automata;

            try
            {
                automata = Automata.Deserialize();

                IsInfinite = automata.IsInfinite;
                NeighborhoodPicker.SelectedNeighborhood = automata.Neighborhood;

                foreach (var item in new ObservableCollection<Rule>(new List<Rule>(automata.RuleSet.Rules)))
                    RuleSetConstructor.RuleSet.Add(item);

                await Task.Delay(1);
                RuleSetConstructor.UpdateViews();
            }
            catch (Exception)
            {
                MessageBox.Show("Error while loading an automata from the file", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void SaveAutomata(object sender, RoutedEventArgs e)
        {
            if (RuleSetConstructor.RuleSet.Count == 0)
            {
                MessageBox.Show("Rule set can't be empty", "Can't costruct an automata",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }


            if (Automata.Serialize(ConstructAutomata()))
                MessageBox.Show("Successfuly saved automata to the file", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Error while saving automata to the file", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void VisualizeAutomata(object sender, RoutedEventArgs e)
        {
            if (RuleSetConstructor.RuleSet.Count == 0)
            {
                MessageBox.Show("Rule set can't be empty", "Can't costruct an automata",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            NavigationService.Navigate(new AutomataVisualizer(ConstructAutomata()));
        }
    }
}
