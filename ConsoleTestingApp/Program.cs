using System;
using System.Linq;
using System.Runtime.InteropServices;
using CellularAutomata;

namespace ConsoleTestingApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Cell[] startingField = CellListGenerator.OneDimDefaultCells(101);
            startingField[49] = new Cell(true);

            GameField gf = new GameField(startingField, new int[] {-1, 0 , 1});
            
            RuleSet ruleSet = new RuleSet(new Rule[]
            {
                new ExactPatternRule(new Cell(true), new Cell[]
                {
                    new Cell(true),
                    new Cell(false),
                    new Cell(false)
                }), 
                new ExactPatternRule(new Cell(true), new Cell[]
                {
                    new Cell(false),
                    new Cell(true),
                    new Cell(true)
                }),
                new ExactPatternRule(new Cell(true), new Cell[]
                {
                    new Cell(false),
                    new Cell(true),
                    new Cell(false)
                }),
                new ExactPatternRule(new Cell(true), new Cell[]
                {
                    new Cell(false),
                    new Cell(false),
                    new Cell(true)
                }),
                new ExactPatternRule(new Cell(false), new Cell[]
                {
                    new Cell(true),
                    new Cell(true),
                    new Cell(true)
                }),
                new ExactPatternRule(new Cell(false), new Cell[]
                {
                    new Cell(true),
                    new Cell(true),
                    new Cell(false)
                }),
                new ExactPatternRule(new Cell(false), new Cell[]
                {
                    new Cell(true),
                    new Cell(false),
                    new Cell(true)
                }),
                new ExactPatternRule(new Cell(false), new Cell[]
                {
                    new Cell(false),
                    new Cell(false),
                    new Cell(false)
                }),
            }, new Cell());

            for (int i = 0; i < 50; i++)
            {
                gf.PrintToConsole();
                gf.ChangeField(ruleSet);
                Console.ReadKey();
            }
        }
    }
}