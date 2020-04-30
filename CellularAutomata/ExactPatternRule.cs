using System;

namespace CellularAutomata
{
    public class ExactPatternRule : Rule
    {
        public ExactPatternRule(Cell nextState, Cell[] cellNeighborhood) 
            : base(nextState, cellNeighborhood) {}

        public new bool CheckSuitability(Cell[] cellNeighborhood)
        {
            for (int i = 0; i < cellNeighborhood.GetLength(0); i++)
            {
                if (CellNeighborhood[i].CompareTo(cellNeighborhood[i]) != 0)
                    return false;
            }

            return true;
        }
    }
}