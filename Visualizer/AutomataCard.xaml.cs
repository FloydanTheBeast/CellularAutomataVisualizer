using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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
