using Newtonsoft.Json;
using System;
using System.IO;

namespace CellularAutomata
{
    public class Automata
    {
        public int _cellSize { get; }

        public bool _isInfinite { get; set; }

        public int[][] _neighborhood { get; set; }

        public RuleSet _ruleSet { get; set; }

        string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                    _name = $"Cellular automata ${GetHashCode().ToString().Substring(0, 5)}";
                else
                    _name = value;
            }
        } 

        public Automata(int cellSize, bool isInfinite, int[][] neighborhood, RuleSet ruleSet, string name = "")
        {
            _cellSize = cellSize;
            _isInfinite = isInfinite;
            _neighborhood = neighborhood;
            _ruleSet = ruleSet;
            Name = name;
        }


        public static Automata Deserialize()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;

            Automata automata;

            try
            {
                using (StreamReader sr = new StreamReader("test.json"))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        automata = (Automata)serializer.Deserialize(sr, typeof(Automata));
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error while deserializing an automata object");
            }

            return automata;
        }


        public static bool Serialize(Automata automata)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;

            try
            {
                using (StreamWriter sw = new StreamWriter("test.json"))
                using (JsonWriter writer = new JsonTextWriter(sw))
                    serializer.Serialize(writer, automata);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
