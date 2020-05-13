using System;

namespace CellularAutomata
{
    public static class CellListGenerator
    {
        static Random rnd = new Random();

        /// <summary>
        /// Generates an array of default cells
        /// </summary>
        /// <param name="width">Width of an array</param>
        /// <param name="height">Height of an array</param>
        /// <returns>Generated array</returns>
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

        /// <summary>
        /// Generates a random array of cells
        /// </summary>
        /// <param name="width">Width of an array</param>
        /// <param name="height">Height of an array</param>
        /// <returns>Generated array</returns>
        public static Cell[][] GenerateRandom(int width, int height = 1)
        {
            if (height < 1)
                throw new ArgumentException("Height of an automata field should be a positive number");

            Cell[][] cellList = new Cell[height][];

            for (int i = 0; i < height; i++)
            {
                cellList[i] = new Cell[width];
                for (int j = 0; j < width; j++)
                    cellList[i][j] = rnd.NextDouble() > 0.5 ? new Cell() : new Cell(true);
            }

            return cellList;
        }
    }
}