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
        }; // FIXME: Set default value in the constructor

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
        public Cell(string[] propertiesNames, object[] propertiesValues)
        {
            // TODO: Join with _properties 
            
            if (propertiesNames?.Length != propertiesValues?.Length)
                throw new ArgumentException("There should be the same number of properties names and values corresponding to them");
            
            Properties = propertiesNames?.Select((name, index) => new {Key = name, Index = index})
                .ToDictionary(x => x.Key, x => propertiesValues?[x.Index]);
        }
        
        // TODO: Delete constructor 
        public Cell(bool isAlive)
        {
            Properties["isAlive"] = isAlive;
        }
        
        public Cell() {}
        
        public void ChangeProperties() {} // TODO: Write change properties method

        /// <summary>
        /// Compare this cell with another cell
        /// </summary>
        /// <param name="otherCell">Other cell instance</param>
        /// <returns>CompareTo value</returns>
        public int CompareTo(Cell otherCell)
        {
            // FIXME: Use LINQ instead
            foreach (var key in Properties.Keys)
            {
                if (Properties[key] is bool)
                    return (Properties[key] as bool?).Value.CompareTo(otherCell[key] as bool?);
            }
            return 0;
        }
        
        public object this[string index] => Properties[index]; // FIXME: Catch all possible exceptions
    
        public override string ToString()
        {
            if ((bool)Properties["isAlive"])
                return "■";
            return "-";
        }

        public object Clone()
        {    
            return new Cell(
                (Properties["isAlive"] as bool?).Value
            );
        }
    }
}
