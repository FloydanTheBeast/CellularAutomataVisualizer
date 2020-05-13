using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
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

        public BindingExpressionBase CurrentRuleBinding;

        public NearbyNeighborsRule CurrentRule
        {
            get => (NearbyNeighborsRule)GetValue(CurrentRuleProperty);
            set
            {
                SetValue(CurrentRuleProperty, value);
            }
        }

        string _lowerBound = "-1";
        public string LowerBound {
            get => _lowerBound;
            set => _lowerBound = String.IsNullOrEmpty(value) ? "-1" : value;
        }

        string _upperBound = "-1";
        public string UpperBound
        {
            get => _upperBound;
            set => _upperBound = String.IsNullOrEmpty(value) ? "-1" : value;
        }

        public NearbyNeighborsRuleConstructor(int collectionIndex)
        {
            Binding ruleCollectionBinding = new Binding();

            ruleCollectionBinding.Path = new PropertyPath($"RuleSet[{collectionIndex}]");
            ruleCollectionBinding.Mode = BindingMode.TwoWay;

            CurrentRuleBinding = SetBinding(CurrentRuleProperty, ruleCollectionBinding);

            InitializeComponent();

            StartingStateCanvas.Children.Add(GenerateRect(
                    (int)StartingStateCanvas.Width, (bool)CurrentRule.CenterCellState["isAlive"]
            ));

            EndingStateCanvas.Children.Add(GenerateRect(
                    (int)StartingStateCanvas.Width, (bool)CurrentRule.NextState["isAlive"]
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
                        new Cell(true),
                        "isAlive",
                        true,
                        0,
                        9,
                        new Cell()
                ))
            );
        }

        public void UpdateView()
        {
            Rectangle startingStateRect = (Rectangle)StartingStateCanvas.Children[0];

            startingStateRect.Tag = (bool)CurrentRule.CenterCellState["isAlive"];
            startingStateRect.Fill = (bool)CurrentRule.CenterCellState["isAlive"] ?
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2ecc71")) :
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#34495e"));

            Rectangle endingStateRect = (Rectangle)EndingStateCanvas.Children[0];

            endingStateRect.Tag = (bool)CurrentRule.NextState["isAlive"];
            endingStateRect.Fill = (bool)CurrentRule.NextState["isAlive"] ?
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2ecc71")) :
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#34495e"));

            LowerBoundInput.Text = CurrentRule.LowerBound.ToString();
            LowerBound = CurrentRule.LowerBound.ToString();

            UpperBoundInput.Text = CurrentRule.UpperBound.ToString();
            UpperBound = CurrentRule.UpperBound.ToString();
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
                int.Parse(LowerBound),
                int.Parse(UpperBound),
                new Cell((bool)((Rectangle)StartingStateCanvas.Children[0]).Tag)
            );

            OnPropertyChanged("CurrenRule");
        }
    }
}
