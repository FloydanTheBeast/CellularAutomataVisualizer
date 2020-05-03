using System;

namespace CellularAutomata
{
    public static class CellListGenerator
    {
        public static Cell[][] Generate(int width, int height = 1)
        {
            if (height < 1)
                throw new ArgumentException("Height of an automata field should be a positive number");
            
            Cell[][] cellList = new Cell[height][];

            for (int i = 0; i < height; i++)
            {
                cellList[i] = new Cell[width];
                for (int j = 0; j < width; j++)
                    cellList[i][j] = new Cell();
            }

            return cellList;
        }
    }
}