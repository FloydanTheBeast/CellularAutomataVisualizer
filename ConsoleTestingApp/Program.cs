﻿using System;
using CellularAutomata;

namespace ConsoleTestingApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Cell[][] startingField = CellListGenerator.Generate(199);
            startingField[0][100] = new Cell(true);

            GameField gf = new GameField(startingField, new[] {new[] {-1}, new[] {0}, new[] {1}});
            
            // RuleSet ruleSet = new RuleSet(new Rule[]
            // {
            //     new ExactPatternRule(new Cell(true), new Cell[]
            //     {
            //         new Cell(true),
            //         new Cell(false),
            //         new Cell(false)
            //     }), 
            //     new ExactPatternRule(new Cell(true), new Cell[]
            //     {
            //         new Cell(false),
            //         new Cell(true),
            //         new Cell(true)
            //     }),
            //     new ExactPatternRule(new Cell(true), new Cell[]
            //     {
            //         new Cell(false),
            //         new Cell(true),
            //         new Cell(false)
            //     }),
            //     new ExactPatternRule(new Cell(true), new Cell[]
            //     {
            //         new Cell(false),
            //         new Cell(false),
            //         new Cell(true)
            //     }),
            //     new ExactPatternRule(new Cell(false), new Cell[]
            //     {
            //         new Cell(true),
            //         new Cell(true),
            //         new Cell(true)
            //     }),
            //     new ExactPatternRule(new Cell(false), new Cell[]
            //     {
            //         new Cell(true),
            //         new Cell(true),
            //         new Cell(false)
            //     }),
            //     new ExactPatternRule(new Cell(false), new Cell[]
            //     {
            //         new Cell(true),
            //         new Cell(false),
            //         new Cell(true)
            //     }),
            //     new ExactPatternRule(new Cell(false), new Cell[]
            //     {
            //         new Cell(false),
            //         new Cell(false),
            //         new Cell(false)
            //     }),
            // }, new Cell());
            
            RuleSet xorRule90 = new RuleSet(new Rule[] {
                new XorRule(90)
            }, new Cell(), true);
            
            RuleSet rule90 = new RuleSet(new Rule[] {
                new NearbyNeighborsRule(
                    new Cell(true),
                    "isAlive",
                    true,
                    x => x == 1
                )
            }, new Cell(), true);

            int i = 1;
            
            // do
            // {
            //     Console.Write($"{i++:d3}.");
            //     gf.PrintToConsole();
            //     gf.ChangeField(ruleSet);
            // } while (Console.ReadKey().Key != ConsoleKey.Escape);
            
            Cell[][] startingField2D = CellListGenerator.Generate(20, 20);

            startingField2D[2][1] = new Cell(true);
            startingField2D[3][2] = new Cell(true);
            startingField2D[3][3] = new Cell(true);
            startingField2D[2][3] = new Cell(true);
            startingField2D[1][3] = new Cell(true);
            
            RuleSet ruleSet2D = new RuleSet(new []
            {
                new NearbyNeighborsRule(new Cell(true), "isAlive", true, x => x == 3, new Cell(false)),
                new NearbyNeighborsRule(new Cell(true), "isAlive", true, x => x == 3 || x == 2, new Cell(true))
            }, new Cell(), true);
            
            GameField gf2D = new GameField(startingField2D, new[]
            {
                new[] {-1, -1},
                new[] {-1, 0}, 
                new[] {-1, 1},
                new[] {0, -1},
                new[] {0, 0},
                new[] {0, 1},
                new[] {1, -1},
                new[] {1, 0},
                new[] {1, 1}
            });

            do
            {
                Console.Clear();
                gf2D.PrintToConsole();
                gf2D.ChangeField(ruleSet2D);
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}