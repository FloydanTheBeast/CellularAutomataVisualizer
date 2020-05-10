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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CellularAutomata;

namespace Visualizer
{
    /// <summary>
    /// Логика взаимодействия для NearbyNeighborsRuleConstructor.xaml
    /// </summary>
    public partial class NearbyNeighborsRuleConstructor : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty CurrentRuleProperty;

        public NearbyNeighborsRule CurrentRule
        {
            get => (NearbyNeighborsRule)GetValue(CurrentRuleProperty);
            set
            {
                SetValue(CurrentRuleProperty, value);
            }
        }

        public string LowerBound { get; set; }

        public string UpperBound { get; set; }


        public NearbyNeighborsRuleConstructor(int collectionIndex)
        {
            InitializeComponent();

            Binding ruleCollectionBinding = new Binding();

            ruleCollectionBinding.Path = new PropertyPath($"RuleSet[{collectionIndex}]");
            ruleCollectionBinding.Mode = BindingMode.TwoWay;

            SetBinding(CurrentRuleProperty, ruleCollectionBinding);

            StartingStateCanvas.Children.Add(GenerateRect(
                    (int)StartingStateCanvas.Width, false
            ));

            EndingStateCanvas.Children.Add(GenerateRect(
                    (int)StartingStateCanvas.Width, true
            ));
        }


        static NearbyNeighborsRuleConstructor()
        {
            CurrentRuleProperty = DependencyProperty.Register(
                "CurrentRule",
                typeof(NearbyNeighborsRule),
                typeof(NearbyNeighborsRuleConstructor),
                new PropertyMetadata(
                    new NearbyNeighborsRule(
                        new Cell(),
                        "isAlive",
                        true,
                        x => x >= 0 && x <= 8,
                        new Cell(true)
                ))
            );
        }


        Rectangle GenerateRect(int cellSize, bool isAlive)
        {
            Rectangle cellRect = new Rectangle();

            cellRect.Tag = isAlive;

            cellRect.MouseLeftButtonDown += (sender, e) =>
            {
                Rectangle rect = (Rectangle)sender;
                rect.Tag = !(bool)rect.Tag;
                rect.Fill = (bool)rect.Tag ?
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2ecc71")) :
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#34495e"));

                UpdateRule();
            };

            cellRect.StrokeThickness = 1;
            cellRect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#95a5a6"));

            cellRect.Width = cellSize;
            cellRect.Height = cellSize;
            cellRect.Fill = isAlive ?
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2ecc71")) :
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#34495e"));

            return cellRect;
        }


        Func<int, bool> ConstructPredicate() =>
            (x) => (String.IsNullOrEmpty(LowerBound) || x >= int.Parse(LowerBound))
                && (String.IsNullOrEmpty(UpperBound) || x <= int.Parse(UpperBound));


        private void BoundaryInput(object sender, TextCompositionEventArgs e) =>
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private void BoundaryEditedHandler(object sender, RoutedEventArgs e) {
            LowerBound = LowerBoundInput.Text;
            UpperBound = UpperBoundInput.Text;
            UpdateRule();
        }


        void UpdateRule()
        {
            CurrentRule = new NearbyNeighborsRule(
                new Cell((bool)((Rectangle)EndingStateCanvas.Children[0]).Tag),
                "isAlive",
                true,
                ConstructPredicate(),
                new Cell((bool)((Rectangle)StartingStateCanvas.Children[0]).Tag)
            );

            OnPropertyChanged("CurrenRule");
        }
    }
}
