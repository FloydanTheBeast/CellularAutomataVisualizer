using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    public class EmptyRule : Rule
    {
        public EmptyRule() { }

        public new bool CheckSuitability(Cell[] cellNeighborhood)
        {
            return true;
        }
    }
}
