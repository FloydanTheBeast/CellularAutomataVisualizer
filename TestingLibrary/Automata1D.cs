using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomata;

namespace TestingLibrary
{
    public class Automata1D
    {
        public static Cell[][] startingField;
        public static GameField gameField;
        public static RuleSet xorRule90;
        public static RuleSet exactRule30;
        public static RuleSet nearbyCounterRule151;

        static Automata1D()
        {
            startingField = CellListGenerator.Generate(199); 
            startingField[0][99] = new Cell(true);
            gameField = new GameField(startingField, new[] { new[] { -1 }, new[] { 0 }, new[] { 1 } }, true); 

            nearbyCounterRule151 = new RuleSet(new Rule[] {
                new NearbyNeighborsRule(
                    new Cell(true),
                    "isAlive",
                    true,
                    1,
                    1
                )
            }, new Cell(), true);

            xorRule90 = new RuleSet(new Rule[] {
                new XorRule(90)
            }, new Cell(), true);

           /* exactRule30 = new RuleSet(new Rule[]
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
            }, new Cell(), true);*/
        }
    }
}
