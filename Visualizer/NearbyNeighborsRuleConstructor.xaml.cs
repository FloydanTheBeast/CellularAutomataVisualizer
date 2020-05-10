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
using System.Text.RegularExpressions;

namespace Visualizer
{
    /// <summary>
    /// Логика взаимодействия для NearbyNeighborsRuleConstructor.xaml
    /// </summary>
    public partial class NearbyNeighborsRuleConstructor : UserControl
    {
        public string LowerBound { get; set; }

        public string UpperBound { get; set; }

        public NearbyNeighborsRuleConstructor()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void BoundaryInput(object sender, TextCompositionEventArgs e) =>
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
    }
}
