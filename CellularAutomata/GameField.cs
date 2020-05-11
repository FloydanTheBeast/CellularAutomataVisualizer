using System;

namespace CellularAutomata
{
    public class GameField
    {
        public Cell[][] Cells;
        readonly int[][] _neighborhoodCoords;
        bool _isInfinite;
        public Int64 CurrentGeneration = 1;
        
        public GameField(Cell[][] cells, int[][] neighborhoodCoords, bool isInfinite = false)
        {
            Cells = cells;
            _neighborhoodCoords = neighborhoodCoords;
            _isInfinite = isInfinite;
        }

        Cell[][] CopyCells()
        {
            Cell[][] copiedCells = new Cell[Cells.Length][];

            for (int i = 0; i < Cells.Length; i++)
            {
                copiedCells[i] = new Cell[Cells[i].Length];
                for (int j = 0; j < Cells[i].Length; j++)
                    copiedCells[i][j] = Cells[i][j].Clone() as Cell;
            }

            return copiedCells;
        }

        /// <summary>
        /// Changes field according to passed rule set
        /// </summary>
        /// <param name="ruleSet">Set of rules</param>
        public void ChangeField(RuleSet ruleSet)
        {
            Cell[][] nextState = CopyCells();
            
            for (int i = 0; i < Cells.Length; i++)
                for (int j = 0; j < Cells[i].Length; j++)
                {
                    Rule properRule = ruleSet.FindProperRule(GetNeighborhood(j, i));
                    properRule.Apply(ref nextState[i][j]);
                }

            Cells = nextState;

            // TODO: Catch overflow 
            CurrentGeneration++;
        }

        Cell[] GetNeighborhood(int xCoordinate, int yCoordinate = 0)
        {
            Cell[] neighborhood = new Cell[_neighborhoodCoords.Length];

            for (int i = 0; i < _neighborhoodCoords.Length; i++)
            {
                try
                {
                    if (_isInfinite)
                        neighborhood[i] = Cells[(yCoordinate + (_neighborhoodCoords[i].Length >= 2 ? _neighborhoodCoords[i][1] : 0) + Cells.Length) % Cells.Length]
                                                [(xCoordinate + _neighborhoodCoords[i][0] + Cells[0].Length) % Cells[0].Length];
                    else
                    {
                        int newYCoordinate = yCoordinate + (_neighborhoodCoords[i].Length >= 2 ? _neighborhoodCoords[i][1] : 0);
                        int newXCoordinate = xCoordinate + _neighborhoodCoords[i][0];

                        if (newYCoordinate >= 0 && newYCoordinate < Cells.Length &&
                            newXCoordinate >= 0 && newXCoordinate < Cells[i].Length)
                        {
                            neighborhood[i] = Cells[yCoordinate + (_neighborhoodCoords[i].Length >= 2 ? _neighborhoodCoords[i][1] : 0)]
                                            [xCoordinate + _neighborhoodCoords[i][0]];
                        }
                        else
                            neighborhood[i] = new Cell();
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    neighborhood[i] = new Cell(); // DEFAULT CELL
                }

                if (_neighborhoodCoords[i].Length > 2)
                    // TODO: Throw custom exception
                    throw new Exception(
                            "Neighbor should have the same number of coordinates as there are dimensions in the automata"
                        );
            }
            
            return neighborhood;
        }

        public void PrintToConsole()
        {
            /* if Cells.Length is 1 then there's only one
                dimension otherwise there are two */
            
            switch (Cells.Length)
            {
                case 1:
                    foreach (Cell cell in Cells[0])
                        Console.Write(cell);
                    Console.WriteLine();
                    break;
                default:
                    for (int i = 0; i < Cells.Length; i++)
                    {
                        for (int j = 0; j < Cells[i].Length; j++)
                            Console.Write(Cells[i][j]);
                        Console.WriteLine();
                    }
                    break;
            }
        }
    }
}