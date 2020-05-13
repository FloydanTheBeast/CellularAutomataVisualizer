using Newtonsoft.Json;
using System;
using System.IO;

namespace CellularAutomata
{
    public class Automata
    {
        static readonly string pathToFile = Path.Combine(Environment.CurrentDirectory, "automata.json");

        public int CellSize { get; }

        public bool IsInfinite { get; set; }

        public int[][] Neighborhood { get; set; }

        public RuleSet RuleSet { get; set; }

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
            CellSize = cellSize;
            IsInfinite = isInfinite;
            Neighborhood = neighborhood;
            RuleSet = ruleSet;
            Name = name;
        }

        public static Automata Deserialize()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;

            Automata automata;

            try
            {
                using (StreamReader sr = new StreamReader(pathToFile))
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
                using (StreamWriter sw = new StreamWriter(pathToFile))
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
