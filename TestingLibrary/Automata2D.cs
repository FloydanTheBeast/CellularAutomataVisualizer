using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomata;

namespace TestingLibrary
{
    public static class Automata2D
    {
        static Cell[][] startingField;
        public static RuleSet ruleGol;
        public static RuleSet ruleGolB3S35;
        public static GameField gameField;

        static Automata2D()
        {
            startingField = CellListGenerator.GenerateRandom(20, 20);

            ruleGol = new RuleSet(new[]
{
                new NearbyNeighborsRule(new Cell(true), "isAlive", true, x => x == 3, new Cell()),
                new NearbyNeighborsRule(new Cell(true), "isAlive", true, x => x == 3 || x == 2, new Cell(true))
            }, new Cell(), true);

            GameField gameField = new GameField(startingField, new[]
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
            }, true);

            // B3/S35 ruleset
            ruleGolB3S35 = new RuleSet(new[]
            {
                new NearbyNeighborsRule(new Cell(true), "isAlive", true, x => x == 3, new Cell()),
                new NearbyNeighborsRule(new Cell(true), "isAlive", true, x => x == 3 || x == 5, new Cell(true))
            }, new Cell(), true);

            void CreateGlider()
            {
                // Glided for game of life B3/S25
                startingField[2][1] = new Cell(true);
                startingField[3][2] = new Cell(true);
                startingField[3][3] = new Cell(true);
                startingField[2][3] = new Cell(true);
                startingField[1][3] = new Cell(true);
            }

            void CreateGliderB3S35()
            {
                // Glider for game of life B3/S35
                startingField[3][2] = new Cell(true);
                startingField[3][3] = new Cell(true);
                startingField[3][4] = new Cell(true);
                startingField[3][5] = new Cell(true);
                startingField[4][5] = new Cell(true);
                startingField[4][6] = new Cell(true);
                startingField[5][5] = new Cell(true);
                startingField[5][4] = new Cell(true);
                startingField[5][3] = new Cell(true);
                startingField[5][2] = new Cell(true);
            }
        }
    }
}
