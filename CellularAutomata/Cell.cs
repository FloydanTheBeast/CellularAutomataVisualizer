using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    public class Cell : IComparable<Cell>, ICloneable
    {
        public Dictionary<string, object> Properties = new Dictionary<string, object>{
            { "isAlive", false }
        }; // TODO: Set default value in the constructor

        public static readonly Dictionary<string, object> DefaultProperties = new Dictionary<string, object>{
            { "isAlive", false }
        };
        
        // Position, coords (for various dimensions)
        // Cell comparator

        // List of default properties

        // List of properties
        
        // public CellProperties Properties { get; set; }

        /// <summary>
        /// Creates 
        /// </summary>
        /// <param name="propertiesNames">Array of properties names</param>
        /// <param name="propertiesValues">Array of properties values</param>
        /// <exception cref="ArgumentException">Length of two passed arrays should be the same</exception>
        /*public Cell(string[] propertiesNames, object[] propertiesValues)
        {
            // TODO: Join with _properties 
            
            if (propertiesNames?.Length != propertiesValues?.Length)
                throw new ArgumentException("There should be the same number of properties names and values corresponding to them");
            
            Properties = propertiesNames?.Select((name, index) => new {Key = name, Index = index})
                .ToDictionary(x => x.Key, x => propertiesValues?[x.Index]);
        }*/
        
        // TODO: Delete constructor 
        public Cell(bool isAlive = false)
        {
            Properties["isAlive"] = isAlive;
        }

        public static bool operator ==(Cell cell1, Cell cell2)
        {
            if (ReferenceEquals(null, cell1))
                return ReferenceEquals(null, cell2);

            if (ReferenceEquals(null, cell2))
                return false;

            return cell1.CompareTo(cell2) == 0;
        }
        
        public static bool operator !=(Cell cell1, Cell cell2)
        {
            return !(cell1 == cell2);
        }

        public void ChangeProperties() {} // TODO: Write change properties method

        /// <summary>
        /// Compare this cell with another cell
        /// </summary>
        /// <param name="otherCell">Other cell instance</param>
        /// <returns>CompareTo value</returns>
        public int CompareTo(Cell otherCell)
        {
            // TODO: Use LINQ instead
            foreach (var key in Properties.Keys)
            {
                if (Properties[key] is bool)
                    return ((bool?)Properties[key]).Value.CompareTo(otherCell[key] as bool?);
            }
            return 0;
        }
        
        public object this[string index] => Properties[index]; // TODO: Catch all possible exceptions
    
        public override string ToString()
        {
            if ((bool)Properties["isAlive"])
                return "■";
            return "-";
        }

        public object Clone()
        {    
            return new Cell(
                ((bool?) Properties["isAlive"]).Value
            );
        }
    }
}
