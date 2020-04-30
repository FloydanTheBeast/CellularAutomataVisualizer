namespace CellularAutomata
{
    public static class CellListGenerator
    {
        public static Cell[] OneDimDefaultCells(int size)
        {
            Cell[] cellList = new Cell[size];
            
            for (int i = 0; i < size; i++)
                cellList[i] = new Cell();

            return cellList;
        }
    }
}