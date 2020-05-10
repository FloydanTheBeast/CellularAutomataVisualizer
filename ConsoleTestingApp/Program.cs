using System;
using CellularAutomata;
using TestingLibrary;

namespace ConsoleTestingApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Code for testing 1D cellular automata 
            int i = 1;

            /*do
            {
                Console.Write($"{i++:d3}.");
                Automata1D.gameField.PrintToConsole();
                Automata1D.gameField.ChangeField(
                    Automata1D.xorRule90
                );
            } while (Console.ReadKey().Key != ConsoleKey.Escape);*/

            do
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Console.Clear();
                Automata2D.gameField.PrintToConsole();
                Automata2D.gameField.ChangeField(
                    Automata2D.ruleGol
                );
                watch.Stop();
                Console.WriteLine(watch.ElapsedMilliseconds);
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}