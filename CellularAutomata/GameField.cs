using System;

namespace CellularAutomata
{
    public class GameField
    {
        Cell[] _cells;
        readonly int[] _neighborhoodCoords;
        bool _isInfinite;
        
        public GameField(Cell[] cells, int[] neighborhoodCoords, bool isInfinite = false)
        {
            _cells = cells;
            _neighborhoodCoords = neighborhoodCoords;
            _isInfinite = isInfinite;
        }

        Cell[] CopyCells()
        {
            Cell[] copiedCells = new Cell[_cells.Length];

            for (int i = 0; i < _cells.Length; i++)
            {
                copiedCells[i] = _cells[i].Clone() as Cell;
            }
            
            return copiedCells;
        }

        public void ChangeField(RuleSet ruleSet)
        {
            Cell[] nextState = CopyCells();
            
            for (int i = 0; i < _cells.Length; i++)
            {
                Rule properRule = ruleSet.FindProperRule(GetNeighborhood(i));
                
                properRule.Apply(ref nextState[i]);
            }

            _cells = nextState;
        }

        Cell[] GetNeighborhood(int xCoordinate)
        {
            Cell[] neighborhood = new Cell[_neighborhoodCoords.Length];
            
            switch (_cells.Rank)
            {
                case 1:
                    int i = 0;
                    
                    foreach (int neighborCoords in _neighborhoodCoords)
                    {
                        try
                        {
                            neighborhood[i] = _cells[xCoordinate + neighborCoords];
                        }
                        catch (IndexOutOfRangeException)
                        {
                            neighborhood[i] = new Cell(); // DEFAULT CELL
                        }
                        finally
                        {
                            i++;
                        }
                    }
                    break;
            }

            return neighborhood;
        }

        public void PrintToConsole()
        {
            foreach (Cell cell in _cells)
                Console.Write(cell);
            
            Console.WriteLine();
        }
    }
}