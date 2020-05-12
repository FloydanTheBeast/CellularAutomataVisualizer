using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomata;

namespace AutomatasGallery
{
    public static class Automatas2D
    {
        public static List<Automata> automatas = new List<Automata>();

        static Automatas2D()
        {
            automatas.Add(new Automata(
                Constants.CellSize,
                true,
                Constants.MooreNeighborhood,
                new RuleSet(
                    new Rule[] {
                        new NearbyNeighborsRule(new Cell(true), "isAlive", true, 3, 3, new Cell()),
                        new NearbyNeighborsRule(new Cell(true), "isAlive", true, 2, 3, new Cell(true))
                    },
                    new Cell(), true
                ),
                "Classic Game of Life"
            ));

            automatas.Add(new Automata(
                Constants.CellSize,
                true,
                Constants.MooreNeighborhood,
                new RuleSet(
                    new Rule[] {
                        new NearbyNeighborsRule(new Cell(true), "isAlive", true, 2, 2, new Cell()),
                        new NearbyNeighborsRule(new Cell(true), "isAlive", true, 2, 3, new Cell(true)),
                    },
                    new Cell(), true
                ),
                "Game of Life B2/S23"
            ));

            automatas.Add(new Automata(
                Constants.CellSize,
                true,
                Constants.MooreNeighborhood,
                new RuleSet(
                    new Rule[] {
                        new NearbyNeighborsRule(new Cell(true), "isAlive", true, 3, 3, new Cell()),
                        new NearbyNeighborsRule(new Cell(true), "isAlive", true, 3, 3, new Cell(true)),
                        new NearbyNeighborsRule(new Cell(true), "isAlive", true, 5, 5, new Cell(true))
                    },
                    new Cell(), true
                ),
                "Game of Life B3/S35"
            ));

            automatas.Add(new Automata(
                Constants.CellSize,
                true,
                Constants.NeumannNeigborhood,
                new RuleSet(
                    new Rule[] {
                        new NearbyNeighborsRule(new Cell(true), "isAlive", true, 2, 2, new Cell(true)),
                        new NearbyNeighborsRule(new Cell(true), "isAlive", true, 4, 4, new Cell(true)),
                        new NearbyNeighborsRule(new Cell(true), "isAlive", true, 1, 1, new Cell()),
                        new NearbyNeighborsRule(new Cell(true), "isAlive", true, 3, 3, new Cell())
                    },
                    new Cell(), true
                ),
                "Neumann XOR"
            ));
        }
    }
}
