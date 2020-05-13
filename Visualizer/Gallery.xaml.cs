using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CellularAutomata;
using AutomatasGallery;

namespace Visualizer
{
    /// <summary>
    /// Логика взаимодействия для Gallerty.xaml
    /// </summary>
    public partial class Gallery : Page
    {
        public Gallery()
        {
            InitializeComponent();

            ConstructGallerty(Gallery2D, Automatas2D.automatas);
            ConstructGallerty(Gallery1D, Automatas1D.automatas);
        }

        void ConstructGallerty(StackPanel gallery, List<Automata> automatas)
        {
            for (int i = 0; i < automatas.Count; i++)
            {
                StackPanel row = new StackPanel();

                row.Orientation = Orientation.Horizontal;
                row.Width = 0;

                double cardWidth;

                do
                {
                    var card = new AutomataCard(automatas[i]);

                    card.Margin = new Thickness(8);

                    card.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    row.Children.Add(card);

                    cardWidth = card.DesiredSize.Width;
                    row.Width += cardWidth;
                }
                while (row.Width + cardWidth < 1280 && ++i < automatas.Count);

                gallery.Children.Add(row);
            }
        }
    }
}
