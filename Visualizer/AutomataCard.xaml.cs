using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CellularAutomata;

namespace Visualizer
{
    /// <summary>
    /// Логика взаимодействия для AutomataCard.xaml
    /// </summary>
    public partial class AutomataCard : UserControl
    {
        Automata automata;

        public AutomataCard(Automata automata)
        {
            InitializeComponent();

            AutomataNameText.Text = automata.Name;
            this.automata = automata;
        }

        private void VisualizeAutomata(object sender, RoutedEventArgs e)
            => NavigationService.GetNavigationService(this).Navigate(new AutomataVisualizer(automata));
    }
}
