using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    /// <summary>
    /// Class that defines a single rule which determines
    /// the next state of a cell by checking if this cell's neighborhood
    /// is suitable for the changes
    /// </summary>
    public abstract class Rule
    {
        // Next state of a cell
        readonly Cell _nextState;

        // List of all cell's neighbors
        internal readonly Cell[] CellNeighborhood;

        protected Rule(Cell nextState, Cell[] cellNeighborhood)
        {
            _nextState = nextState;
            CellNeighborhood = cellNeighborhood;
        }

        protected Rule() { }

        /// <summary>
        /// Checks if a passed neighborhood is proper for this rule
        /// </summary>
        /// <param name="cellNeighborhood">Cell's neighborhood</param>
        /// <returns>True if neighborhood is suitable, false otherwise</returns>
        public bool CheckSuitability(Cell[] cellNeighborhood)
        {
            return this is ExactPatternRule
                ? (this as ExactPatternRule).CheckSuitability(cellNeighborhood)
                : false;
        }
        
        /// <summary>
        /// Changes passed cell's properties to nextState properties by creating
        /// a clone of a passed cell and assigning nextState to it's properties
        /// </summary>
        /// <param name="cell"></param>
        public void Apply(ref Cell cell)
        {
            // If this is empty rule then the next state doesn't need to be changed
            if (!(this is EmptyRule))
                cell.Properties = _nextState.Properties;
        }
    }
}
