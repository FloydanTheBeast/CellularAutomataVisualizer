using System;

namespace CellularAutomata
{
    [Serializable]
    public class Automata
    {
        public int _cellSize { get; }

        public bool _isInfinite { get; set; }

        public int[][] _neighborhood { get; set; }

        public RuleSet _ruleSet { get; set; }

        public Automata(int cellSize, bool isInfinite, int[][] neighborhood, RuleSet ruleSet)
        {
            _cellSize = cellSize;
            _isInfinite = isInfinite;
            _neighborhood = neighborhood;
            _ruleSet = ruleSet;
        }
    }
}
