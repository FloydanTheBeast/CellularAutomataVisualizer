namespace CellularAutomata
{
    public class ExactPatternRule : Rule
    {
        public ExactPatternRule(Cell nextState, Cell[] cellNeighborhood) 
            : base(nextState, cellNeighborhood) {}

        /// <summary>
        /// Initialize ExactPatternRule with default cells
        /// </summary>
        /// <param name="numberOfNeighbors">Number of cells in the neighborhood</param>
        public ExactPatternRule(Cell nextState, int numberOfNeighbors)
        {
            Cell[] cellNeighborhood = new Cell[numberOfNeighbors];

            for (int i = 0; i < cellNeighborhood.Length; i++)
                cellNeighborhood[i] = new Cell();

            NextState = nextState;
            CellNeighborhood = cellNeighborhood;
        }

        public new bool CheckSuitability(Cell[] cellNeighborhood)
        {
            for (int i = 0; i < cellNeighborhood.GetLength(0); i++)
            {
                if (CellNeighborhood[i].CompareTo(cellNeighborhood[i]) != 0)
                    return false;
            }

            return true;
        }

        public bool IsAlive(int index) =>
            (bool)CellNeighborhood[index]["isAlive"];

        /// <summary>
        /// Toggles "isAlive" property of the neighbor at passed index
        /// </summary>
        /// <param name="index">Index of the neighbor</param>
        public void ToggleNeighbor(int index) =>
            CellNeighborhood[index].Properties["isAlive"] = !(bool) CellNeighborhood[index].Properties["isAlive"];
    }
}