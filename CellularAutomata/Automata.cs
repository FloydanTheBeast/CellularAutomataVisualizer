using System;

namespace CellularAutomata
{
    [Serializable]
    public class Automata
    {
        public int _cellSize { get; }

        public bool _isInfinite { get; }

        public RuleSet _ruleSet { get; }

        public int[][] _neighborhood { get; }

        public Automata(int cellSize, bool isInfinite, RuleSet ruleSet, int[][] neighborhood)
        {
            _cellSize = cellSize;
            _isInfinite = isInfinite;
            _ruleSet = ruleSet;
            _neighborhood = neighborhood;
        }
    }
}
