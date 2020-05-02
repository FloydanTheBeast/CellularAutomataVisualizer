using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    public class EmptyRule : Rule
    {
        internal bool _shouldCellUpdateToDefault;
        
        public EmptyRule(bool shouldCellUpdateToDefault)
        {
            _shouldCellUpdateToDefault = shouldCellUpdateToDefault;
        }

        public new bool CheckSuitability(Cell[] cellNeighborhood)
        {
            return true;
        }
    }
}
