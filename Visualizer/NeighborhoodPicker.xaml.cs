using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Visualizer
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class NeighborhoodPicker : UserControl, INotifyPropertyChanged
    {
        static int _cellSize;

        static int[][] MooreNeighborhood = new[]
        {
            new [] {-1, -1},
            new [] {-1, 0},
            new [] {-1, 1},
            new [] {0, -1},
            new [] {0, 0},
            new [] {0, 1},
            new [] {1, -1},
            new [] {1, 0},
            new [] {1, 1},
        };

        static int[][] NeumannNeigborhood = new[]
        {
            new [] {0, -1},
            new [] {-1, 0},
            new [] {0, 0},
            new [] {0, 1},
            new [] {1, 0},
        };

        int[][] _selectedNeighborhood;

        public int[][] SelectedNeighborhood
        {
            get => _selectedNeighborhood;
            set
            {
                if (_selectedNeighborhood != value)
                {
                    _selectedNeighborhood = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public NeighborhoodPicker()
        {
            DataContext = this;
            InitializeComponent();
            _cellSize = (int)(NeighborhoodVisualizer.Width / 3);

            // Update view if selectedNeighbor was changed
            PropertyChanged += (obj, e) => DrawNeighborhood();

            // Set default neighborhood to be Moore's
            TypePicker.SelectedIndex = 0;
        }

        private void TypeSelectHandler(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null) {
                var item = (ComboBoxItem)((ComboBox)sender).SelectedItem;

                switch (item.Tag)
                {
                    case "Moore":
                        SelectedNeighborhood = MooreNeighborhood;
                        break;
                    case "Neumann":
                        SelectedNeighborhood = NeumannNeigborhood;
                        break;
                    default:
                        MessageBox.Show("There is no such neighborhood", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                }
            }
        }

        void DrawNeighborhood()
        {
            NeighborhoodVisualizer.Children.Clear();

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    Rectangle cellRect = GenerateRect();
                    Canvas.SetLeft(cellRect, j * _cellSize);
                    Canvas.SetTop(cellRect, i * _cellSize);
                    NeighborhoodVisualizer.Children.Add(cellRect);
                }
            
            foreach (var coords in SelectedNeighborhood)
            {
                Rectangle cellRect = GenerateRect(true);
                Canvas.SetLeft(cellRect, (coords[0] + 1) * _cellSize);
                Canvas.SetTop(cellRect, (coords[1] + 1) * _cellSize);
                NeighborhoodVisualizer.Children.Add(cellRect);
            }   
        }

        Rectangle GenerateRect(bool isSelected = false)
        {
            Rectangle cellRect = new Rectangle();

            if (isSelected)
            {
                cellRect.StrokeThickness = 1;
                cellRect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#95a5a6"));
            }

            cellRect.Width = _cellSize;
            cellRect.Height = _cellSize;
            cellRect.Fill = isSelected ? 
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#34495e")) :
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ecf0f1"));

            return cellRect;
        }
    }
}
