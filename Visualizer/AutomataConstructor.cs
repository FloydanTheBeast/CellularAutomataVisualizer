using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CellularAutomata;
using System.Windows;
using System.Collections.Generic;

namespace Visualizer
{
    /// <summary>
    /// Логика взаимодействия для AutomataConstructor.xaml
    /// </summary>
    public partial class AutomataConstructor : Page, INotifyPropertyChanged
    {
        public AutomataConstructor()
        {
            DataContext = this;
            InitializeComponent();
        }

        public string Size { get; set; }

        private bool _isInfinite;
        public bool IsInfinite
        {
            get => _isInfinite;
            set
            {
                if (_isInfinite!= value)
                {
                    _isInfinite = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void ConstructAutomata(object sender, System.Windows.RoutedEventArgs e)
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
                case "Small":
                    cellSize = 16;
                    break;
                case "Medium":
                    cellSize = 8;
                    break;
                case "Large":
                    cellSize = 6;
                    break;
            }

            MessageBox.Show("Automata has been constructed successfuly", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new AutomataVisualizer(cellSize, IsInfinite, ruleSet, neighborhood));
        }
    }
}
