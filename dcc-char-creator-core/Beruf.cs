using DccCharCreator.core.Dice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace DccCharCreator.core
{
    public class Beruf
    {
        public int Index { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Weapon { get; set; } = string.Empty;

        public string Equipment { get; set; } = string.Empty;

        private static readonly Lazy<Dictionary<int, Beruf>> Berufe = new Lazy<Dictionary<int, Beruf>>(Load);

        public static Beruf Random(ID100 berufDice)
        {
            return Berufe.Value[berufDice.Roll()];
        }

        public static Dictionary<int, Beruf> Load()
        {
            using var xmlReader = XmlReader.Create("jobs.xml");
            var xmlSerializer = new XmlSerializer(typeof(Beruf[]));
            var deserializedObject = xmlSerializer.Deserialize(xmlReader);
            var result = ((Beruf[])deserializedObject).ToDictionary(x => x.Index);
            Validate(result);
            return result;
        }

        private static void Validate(Dictionary<int, Beruf> result)
        {
            if(result.Count != 100)
            {
                throw new Exception("Die Jobliste muss genau 100 Einträge enthalten");
            }
            
            foreach(var i in Enumerable.Range(1, result.Count))
            {
                if (!result.ContainsKey(i))
                {
                    throw new Exception($"Für den Wert {i} fehlt ein Beruf");
                }
            }
        }

        public static void Save(List<Beruf> berufe)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Beruf>));
            using var xmlWriter = XmlWriter.Create("jobs.xml", new XmlWriterSettings { Indent = true });
            xmlSerializer.Serialize(xmlWriter, berufe);
        }

        public override string ToString()
        {
            return $"{Index}: {Name}; {Weapon}; {Equipment}";
        }
    }
}