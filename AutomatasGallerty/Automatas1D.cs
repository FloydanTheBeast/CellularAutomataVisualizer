using CellularAutomata;
using System.Collections.Generic;

namespace AutomatasGallery
{
    public static class Automatas1D
    {
        public static List<Automata> automatas = new List<Automata>();

        static Automatas1D()
        {
            for (int i = 0; i < 256; i++)
                automatas.Add(new Automata(
                    Constants.CellSize,
                    true,
                    Constants.ThreeBitsNeigborhood,
                    new RuleSet(
                        new Rule[] { new XorRule(i) },
                        new Cell()
                    ),
                    $"Rule {i}"
                ));
        }
    }
}
