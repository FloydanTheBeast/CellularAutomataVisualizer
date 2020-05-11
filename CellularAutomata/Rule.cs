using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CellularAutomata;

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
        public Cell NextState;

        // List of all cell's neighbors
        public Cell[] CellNeighborhood;

        protected Rule(Cell nextState, Cell[] cellNeighborhood)
        {
            NextState = nextState;
            CellNeighborhood = cellNeighborhood;
        }

        protected Rule(Cell nextState)
        {
            NextState = nextState;
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
                ? ((ExactPatternRule) this).CheckSuitability(cellNeighborhood)
                
                : this is XorRule
                ? ((XorRule) this).CheckSuitability(cellNeighborhood)
                    
                : this is NearbyNeighborsRule 
                ? ((NearbyNeighborsRule) this).CheckSuitability(cellNeighborhood)
                
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
                cell.Properties = NextState.Properties;
            else if (((EmptyRule) this).ShouldCellUpdateToDefault)
                cell = new Cell();
        }
    }
}
