﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
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
using TestingLibrary;

namespace Visualizer
{
    /// <summary>
    /// Логика взаимодействия для AutomataVisualizer.xaml
    /// </summary>
    public partial class AutomataVisualizer : UserControl
    {
        readonly int cellSize = 6;

        readonly int width;

        readonly int height;

        readonly bool isOneDimensional;

        readonly GameField gameField;

        readonly RuleSet ruleSet;

        // Delay before next update
        double delay = 0.0125;

        DispatcherTimer timer = new DispatcherTimer();

        public AutomataVisualizer(Automata automata)
        {
            InitializeComponent();

            ruleSet = automata._ruleSet;
            cellSize = automata._cellSize;

            // Size of gameField in cells
            width = (int)GameField.Width / cellSize;
            height = (int)GameField.Height / cellSize;

            timer.Tick += (object sender, EventArgs e) => UpdateAutomata();
            timer.Interval = new TimeSpan((int)(delay * TimeSpan.TicksPerSecond));

            /*ruleSet = new RuleSet(new Rule[] {
                new XorRule(30)
            }, new Cell(), true);*/

            /*ruleSet = Automata2D.ruleGolB3S35;*/

            /*this.ruleSet = new RuleSet(new[]
            {
                new NearbyNeighborsRule(new Cell(true), "isAlive", true, x => x == 1, new Cell()),
                *//*new NearbyNeighborsRule(new Cell(true), "isAlive", true, x => x == 3 || x == 2, new Cell(true))*//*
            }, new Cell(), true);*/

            Cell[][] startingField = CellListGenerator.Generate(width, height);
            /*startingField[5][5] = new Cell(true);*/
            /*startingField[5][6] = new Cell(true);*/

            /*gameField = new GameField(startingField, new[] { new[] { -1 }, new[] { 0 }, new[] { 1 } }, true);*/

            /*startingField[2][1] = new Cell(true);
            startingField[3][2] = new Cell(true);
            startingField[3][3] = new Cell(true);
            startingField[2][3] = new Cell(true);
            startingField[1][3] = new Cell(true);*/

            gameField = new GameField(
                startingField,
                automata._neighborhood,
                automata._isInfinite
            );

            isOneDimensional = gameField.Cells.Length == 1;

            DrawAutomata();
        }

        // Guess what, dumbass
        private void DisableWheelScroll(object sender, MouseWheelEventArgs e)
            => e.Handled = true;

        public void DrawAutomata()
        {
            for (int i = 0; i < gameField.Cells.Length; i++)
            {
                for (int j = 0; j < gameField.Cells[i].Length; j++)
                {
                    Rectangle cellRect = CellToRectangle(gameField.Cells[i][j]);
                    cellRect.Tag = (bool)gameField.Cells[i][j]["isAlive"];
                    Canvas.SetLeft(cellRect, j * cellSize);
                    Canvas.SetTop(cellRect, (i + (isOneDimensional ? gameField.CurrentGeneration : 0)) * cellSize);
                    GameField.Children.Add(cellRect);
                }
            }   
        }

        public void UpdateAutomataView()
        {
            for (int i = 0; i < gameField.Cells.Length; i++)
            {
                for (int j = 0; j < gameField.Cells[i].Length; j++)
                {
                    // If cell's state has changed
                    if ((bool)gameField.Cells[i][j]["isAlive"] != (bool)((Rectangle)GameField.Children[i * width + j]).Tag)
                        ToggleRectangle(j, i);
                }
            }
        }

        void ToggleRectangle(int xCoord, int yCoord)
        {
            Rectangle cellRect = (Rectangle)GameField.Children[yCoord * width + xCoord];

            cellRect.Tag = !(bool)cellRect.Tag;

            cellRect.Fill = (bool)cellRect.Tag ?
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#34495e")) :
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ecf0f1"));
        }

        // TODO: Move to utilities
        Rectangle CellToRectangle(Cell cell, bool withBorder = false)
        {
            Rectangle cellRect = new Rectangle();

            if (withBorder)
            {
                cellRect.StrokeThickness = 2;
                cellRect.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#95a5a6"));
            }

            cellRect.Width = cellSize;
            cellRect.Height = cellSize;
            cellRect.Fill = (bool)cell["isAlive"] ?
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#34495e")) :
                new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ecf0f1"));

            return cellRect;
        }

        private void UpdateAutomataBtnClick(object sender, RoutedEventArgs e)
        {
            UpdateAutomata();
        }

        void UpdateAutomata()
        {
            gameField.ChangeField(ruleSet);

            switch (isOneDimensional)
            {
                case true: // 1D automata
                    if (gameField.CurrentGeneration > height)
                    {
                        GameField.Height += cellSize;
                        GameFieldScroll.ScrollToBottom();
                        GameField.Children.RemoveRange(0, width);
                    }
                    DrawAutomata();
                    break;
                case false: // 2D automata
                    /*GameField.Children.Clear();*/
                    UpdateAutomataView();
                    break;
            }
        }

        private void StartAutomata(object sender, RoutedEventArgs e) => timer.Start();

        private void PauseAutomata(object sender, RoutedEventArgs e) => timer.Stop();

        private void GenerateRandomField(object sender, RoutedEventArgs e)
        {
            gameField.GenerateRandomField(width, height);
            UpdateAutomataView();
        }

        private void DrawField(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPoint = e.GetPosition((Canvas)sender);

                int y = (int)(currentPoint.Y / cellSize);
                int x = (int)(currentPoint.X / cellSize);

                Coords.Content = $"X: {x}, Y: {y}";

                gameField.Cells[y][x] = new Cell(!(bool)gameField.Cells[y][x]["isAlive"]);
                ToggleRectangle(x, y);
            }
        }

        private void DrawCell(object sender, MouseButtonEventArgs e)
        {
            Point currentPoint = e.GetPosition((Canvas)sender);

            int y = (int)(currentPoint.Y / cellSize);
            int x = (int)(currentPoint.X / cellSize);

            Coords.Content = $"X: {x}, Y: {y}";

            gameField.Cells[y][x] = new Cell(!(bool)gameField.Cells[y][x]["isAlive"]);
            ToggleRectangle(x, y);
        }

        private void ClearField(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            gameField.GenerateDefault(width, height);
            UpdateAutomataView();
        }
    }
}
