using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    public class EmptyRule : Rule
    {
        internal readonly bool ShouldCellUpdateToDefault;
        
        public EmptyRule(bool shouldCellUpdateToDefault)
        {
            ShouldCellUpdateToDefault = shouldCellUpdateToDefault;
        }

        public new bool CheckSuitability(Cell[] cellNeighborhood)
        {
            return true;
        }
    }
}
