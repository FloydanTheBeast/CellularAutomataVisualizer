using System;
using System.Linq;

namespace CellularAutomata
{
    public class XorRule : Rule
    {
        readonly int _ruleNumber;
        
        public XorRule(int ruleNumber)
        {
            _ruleNumber = ruleNumber;
        }

        public new bool CheckSuitability(Cell[] cellNeighborhood)
        {
            char[] cellsStateBits = new char[cellNeighborhood.Length];
                
                for (int i = 0; i < cellNeighborhood.Length; i++)
                    cellsStateBits[i] = (bool)cellNeighborhood[i].Properties["isAlive"] ? '1' : '0';

                // Rule for transition of a neighborhood
                int[] transitionBits = ConvertToBits(_ruleNumber, (int) Math.Pow(2, cellNeighborhood.Length));

                NextState = new Cell(
                    transitionBits[
                        transitionBits.Length - Convert.ToInt16(new string(cellsStateBits), 2) - 1
                    ] > 0);

            return true;
        }
        
        // TODO: Move to Utilities
        // TODO: Check number and length parameters to be positive
        // TODO: Check length to be greater or equal to a number of bits in number's value  
        /// <summary>
        /// Converts given integer to an array of bits of given length
        /// </summary>
        /// <param name="number">Value that needs to be converted</param>
        /// <param name="length">Required number of bits</param>
        /// <returns></returns>
        int[] ConvertToBits(int number, int length) =>
            Convert.ToString(number, 2)
                .PadLeft(length, '0')
                .Select(charBit => int.Parse(charBit.ToString()))
                .ToArray();
    }
}