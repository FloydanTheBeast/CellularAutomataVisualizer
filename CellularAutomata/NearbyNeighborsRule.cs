using System;
using System.Collections.Generic;
using System.Linq;

namespace CellularAutomata
{
    public class NearbyNeighborsRule : Rule
    {
        readonly string _propertyName;
        readonly object _propertyValue;
        readonly Func<int, bool> _conditionFunc;
        readonly Cell _centerCellState;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="nextState">Next state of a cell</param>
        /// <param name="propertyName">Name of a property to be counted</param>
        /// <param name="propertyValue">Required value of a property</param>
        /// <param name="conditionFunc">Predicate that accepts a number of properties
        /// that has been counter and returns a boolean depending on a condition</param>
        /// <param name="centerCellState">(Optional) Required state of a center cell</param>
        public NearbyNeighborsRule(Cell nextState, string propertyName, object propertyValue,
            Func<int, bool> conditionFunc, Cell centerCellState = null)
            : base(nextState)
        { 
            _conditionFunc = conditionFunc;
            _propertyValue = propertyValue;
            _propertyName = propertyName;
            _centerCellState = centerCellState;
        }

        public new bool CheckSuitability(Cell[] cellNeighborhood)
        {
            /*
             TODO: Center coords might not actually be in the middle
             consider to create a class 'cellNeighborhood' containing
             center cell's coordinates. Find it by searching for a zero 
             in neighborCoords (e.g. {-1, 0, 1} => return 1 (index of a zero)) 
            */
            int centerCoords = cellNeighborhood.Length / 2;

            // Check if center cell has a required state
            if (_centerCellState != null && _centerCellState != cellNeighborhood[centerCoords])
                return false;

            // Number of cells from neighborhood with required property value  
            int propertyValueCounter = 0;

            for (int i = 0; i < cellNeighborhood.Length; i++)
                // TODO: Check property type
                if (i != centerCoords && (bool)cellNeighborhood[i].Properties[_propertyName] == (bool)_propertyValue)
                    propertyValueCounter++;

            // Console.WriteLine(propertyValueCounter);
            
            return _conditionFunc(propertyValueCounter);
        }
    }
}