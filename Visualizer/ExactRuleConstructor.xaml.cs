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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Visualizer
{
    /// <summary>
    /// Логика взаимодействия для ExactRuleConstructor.xaml
    /// </summary>
    public partial class ExactRuleConstructor : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty CellNeighborhoodProperty;
        public static readonly DependencyProperty CurrentRuleProperty;

        // Coordinates of neighbors
        public int[][] CellNeighborhood
        {
            get => (int[][])GetValue(CellNeighborhoodProperty);
            set
            {
                SetValue(CellNeighborhoodProperty, value);
            }
        }

        public ExactPatternRule CurrentRule
        {
            get => (ExactPatternRule)GetValue(CurrentRuleProperty);
            set
            {
                SetValue(CurrentRuleProperty, value);
            }
        }

        public ExactRuleConstructor(int collectionIndex)
        {
            InitializeComponent();

            Binding neighborhoodBinding = new Binding();

            neighborhoodBinding.Path = new PropertyPath("Tag");
            neighborhoodBinding.Mode = BindingMode.TwoWay;

            SetBinding(CellNeighborhoodProperty, neighborhoodBinding);

            Binding ruleBinding = new Binding();

            ruleBinding.Path = new PropertyPath($"RuleSet[{collectionIndex}]");
            ruleBinding.Mode = BindingMode.TwoWay;

            SetBinding(CurrentRuleProperty, ruleBinding);

            /*PropertyChanged += (obj, e) => MessageBox.Show("TEST");*/
        }

        public void CreateView()
        {
            // Remove everything from the canvas
            NeighborhoodCanvas.Children.Clear();

            int cellSize = (int)(NeighborhoodCanvas.Width / 3);

            Rectangle nextStateRect = GenerateRect((int)NextStateCanvas.Width, (bool)CurrentRule.NextState["isAlive"]);
            nextStateRect.MouseLeftButtonDown += NextStateEditHandler;
            NextStateCanvas.Children.Add(nextStateRect);

            // Draw neighborhood and add click handlers
            for (int i = 0; i < CellNeighborhood.Length; i++)
            {
                switch (CellNeighborhood[i].Length)
                {
                    case 1:
                        break;
                    default:
                        Rectangle cellRect = GenerateRect(cellSize, CurrentRule.IsAlive(i), CellNeighborhood[i]);

                        cellRect.MouseLeftButtonDown += CellRectSelectHandler;

                        Canvas.SetLeft(cellRect, (CellNeighborhood[i][0] + 1) * cellSize);
                        Canvas.SetTop(cellRect, (CellNeighborhood[i][1] + 1) * cellSize);

                        NeighborhoodCanvas.Children.Add(cellRect);
                        break;
                }
            }
        }

        int GetNeighborhoodIndex(int[] coords)
        {
            return CellNeighborhood.Select((neighborCoords, index) => new { neighborCoords, index })
                .First(i => Enumerable.SequenceEqual(i.neighborCoords, coords)).index;
        }

        Rectangle GenerateRect(int cellSize, bool isAlive, int[] coords = null)
        {
            Rectangle cellRect = new Rectangle();

            cellRect.StrokeThickness = 1;
            cellRect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#95a5a6"));

            if (coords != null)
                cellRect.Tag = coords;

            cellRect.Width = cellSize;
            cellRect.Height = cellSize;
            cellRect.Fill = isAlive ?
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2ecc71")) :
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#34495e"));

            return cellRect;
        }

        void NextStateEditHandler(object sender, MouseButtonEventArgs e)
        {
            CurrentRule.NextState = new Cell(!(bool)CurrentRule.NextState["isAlive"]);
            OnPropertyChanged("CurrentRule");

            // Redraw the view
            CreateView();
        }

        void CellRectSelectHandler(object sender, MouseButtonEventArgs e)
        {
            Rectangle cellRect = (Rectangle)sender;

            // Index of a current rectangle in the list of neighbors
            int currentIndex = GetNeighborhoodIndex((int[])cellRect.Tag);

            CurrentRule.ToggleNeighbor(currentIndex);
            OnPropertyChanged("CurrentRule");

            // Redraw the view
            CreateView();
        }

        static ExactRuleConstructor()
        {
            CellNeighborhoodProperty = DependencyProperty.Register(
                "CellNeighborhood",
                typeof(int[][]),
                typeof(ExactRuleConstructor),
                new PropertyMetadata(new int[0][], new PropertyChangedCallback(OnNeighborChanged))
            );

            CurrentRuleProperty = DependencyProperty.Register(
                "CurrentRule",
                typeof(ExactPatternRule),
                typeof(ExactRuleConstructor),
                new PropertyMetadata(new ExactPatternRule(new Cell(), 9))
            );
        }

        private static void OnNeighborChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ExactRuleConstructor exactRuleConstructor = sender as ExactRuleConstructor;

            exactRuleConstructor.CurrentRule = new ExactPatternRule(new Cell(), exactRuleConstructor.CellNeighborhood.Length);

            exactRuleConstructor.CreateView();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
