namespace CellularAutomata
{
    public class NearbyNeighborsRule : Rule
    {
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
        public Cell CenterCellState { get; set; }

        int _lowerBound;
        public int LowerBound {
            get => _lowerBound;
            set
            {
                _lowerBound = value >= 0 ? value : 0;
            }
        }

        int _upperBound;
        public int UpperBound
        {
            get => _upperBound;
            set
            {
                _upperBound = value >= 0 ? value : 0;
            }
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="nextState">Next state of a cell</param>
        /// <param name="propertyName">Name of a property to be counted</param>
        /// <param name="propertyValue">Required value of a property</param>
        /// <param name="centerCellState">(Optional) Required state of a center cell</param>
        public NearbyNeighborsRule(Cell nextState, string propertyName, object propertyValue,
            int lowerBound, int upperBound, Cell centerCellState = null)
            : base(nextState)
        { 
            PropertyValue = propertyValue;
            PropertyName = propertyName;
            LowerBound = lowerBound;
            UpperBound = upperBound;
            CenterCellState = centerCellState;
        }

        /// <summary>
        /// Checks suitability to a condition 
        /// </summary>
        /// <param name="x">Number of neighbors</param>
        /// <returns>If x suits to a condition</returns>
        public bool CheckCondition(int x) =>
            (LowerBound < 0 || x >= LowerBound) &&
            (UpperBound < 0 || x <= UpperBound);

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
            if (CenterCellState != null && CenterCellState != cellNeighborhood[centerCoords])
                return false;

            // Number of cells from neighborhood with required property value  
            int propertyValueCounter = 0;

            for (int i = 0; i < cellNeighborhood.Length; i++)
                // TODO: Check property type
                if (i != centerCoords && (bool)cellNeighborhood[i][PropertyName] == (bool)PropertyValue)
                    propertyValueCounter++;

            // Console.WriteLine(propertyValueCounter);
            
            return CheckCondition(propertyValueCounter);
        }
    }
}