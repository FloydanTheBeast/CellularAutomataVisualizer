using System;

namespace CellularAutomata
{
    public class GameField
    {
        Cell[][] _cells;
        readonly int[][] _neighborhoodCoords;
        bool _isInfinite;
        
        public GameField(Cell[][] cells, int[][] neighborhoodCoords, bool isInfinite = false)
        {
            _cells = cells;
            _neighborhoodCoords = neighborhoodCoords;
            _isInfinite = isInfinite;
        }

        Cell[][] CopyCells()
        {
            Cell[][] copiedCells = new Cell[_cells.Length][];

            for (int i = 0; i < _cells.Length; i++)
            {
                copiedCells[i] = new Cell[_cells[i].Length];
                for (int j = 0; j < _cells[i].Length; j++)
                    copiedCells[i][j] = _cells[i][j].Clone() as Cell;
            }

            return copiedCells;
        }

        public void ChangeField(RuleSet ruleSet)
        {
            Cell[][] nextState = CopyCells();
            
            for (int i = 0; i < _cells.Length; i++)
                for (int j = 0; j < _cells[i].Length; j++)
                {
                    Rule properRule = ruleSet.FindProperRule(GetNeighborhood(j, i));
                    properRule.Apply(ref nextState[i][j]);
                }

            _cells = nextState;
        }

        Cell[] GetNeighborhood(int xCoordinate, int yCoordinate = 0)
        {
            Cell[] neighborhood = new Cell[_neighborhoodCoords.Length];
            
            for (int i = 0; i < _neighborhoodCoords.Length; i++)
            {
                // 1D automata - only one coordinate
                try
                {
                    if (_isInfinite)
                        neighborhood[i] = _cells[(yCoordinate + (_neighborhoodCoords[i].Length >= 2 ? _neighborhoodCoords[i][1] : 0) + _cells.Length) % _cells.Length]
                                                [(xCoordinate + _neighborhoodCoords[i][0] + _cells[i].Length) % _cells[i].Length];
                    else
                        neighborhood[i] = _cells[yCoordinate + (_neighborhoodCoords[i].Length >= 2 ? _neighborhoodCoords[i][1] : 0)]
                                                [xCoordinate + _neighborhoodCoords[i][0]];
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
            /* if _cells.Length is 1 then there's only one
                dimension otherwise there are two */
            
            switch (_cells.Length)
            {
                case 1:
                    foreach (Cell cell in _cells[0])
                        Console.Write(cell);
                    Console.WriteLine();
                    break;
                default:
                    for (int i = 0; i < _cells.Length; i++)
                    {
                        for (int j = 0; j < _cells[i].Length; j++)
                            Console.Write(_cells[i][j]);
                        Console.WriteLine();
                    }
                    break;
            }
        }
    }
}