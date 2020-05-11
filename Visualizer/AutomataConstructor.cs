using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CellularAutomata;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

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

        public int Size { get; set; }

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

            Automata automata = new Automata(cellSize, IsInfinite, ruleSet, neighborhood);
            Serializer(automata);

            MessageBox.Show("Automata has been constructed successfuly", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            /*NavigationService.Navigate(new AutomataVisualizer(cellSize, IsInfinite, ruleSet, neighborhood));*/
        }


        void Serializer(Automata automata)
        {
            JsonSerializer serializer = new JsonSerializer();
            /*serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;*/

            try
            {
                /*using (StreamWriter sw = new StreamWriter("test.json"))
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(writer, automata);
                        // {"ExpiryDate":new Date(1230375600000),"Price":0}
                        MessageBox.Show("Success");
                    }
*/
                using (StreamWriter sw = new StreamWriter("test2.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    Func<int, int> test = (x) => x;
                    serializer.Serialize(writer, test);
                    // {"ExpiryDate":new Date(1230375600000),"Price":0}
                    MessageBox.Show("Success");
                }

                using (StreamReader sw = new StreamReader("test2.json"))
                using (JsonReader reader = new JsonTextReader(sw))
                {
                    Func<int, int> test2 = (x) => 0;
                    test2 = (Func<int, int>)serializer.Deserialize(reader, typeof(Func<int, int>));
                    // {"ExpiryDate":new Date(1230375600000),"Price":0}
                    MessageBox.Show(test2(5).ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
