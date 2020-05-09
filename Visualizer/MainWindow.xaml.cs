﻿using System;
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

namespace Visualizer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
        }

        private void NavigateToConstructor(object sender, RoutedEventArgs e)
        {
            
            MainFrame.Navigate(new AutomataConstructor());
        }

        private void NavigateToVisualizer(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AutomataVisualizer());
        }
    }
}
