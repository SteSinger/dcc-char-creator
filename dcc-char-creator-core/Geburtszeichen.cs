using DccCharCreator.core.Dice;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace DccCharCreator.core
{
    public class Geburtszeichen
    {
        public int Index { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Schicksalswurf { get; set; } = string.Empty;
        public int Bonus { get; set; }

        private static Lazy<Dictionary<int, Geburtszeichen>> GeburtszeichenDict = new Lazy<Dictionary<int, Geburtszeichen>>(Load);

        public Geburtszeichen()
        {
        }

        public Geburtszeichen(Geburtszeichen zeichen, int bonus)
        {
            Index = zeichen.Index;
            Name = zeichen.Name;
            Schicksalswurf = zeichen.Schicksalswurf;
            Bonus = bonus;
        }

        public static Geburtszeichen Random(ID30 dice, int bonus)
        {
            return new Geburtszeichen(GeburtszeichenDict.Value[dice.Roll()], bonus);
        }

        public static Dictionary<int, Geburtszeichen> Load()
        {
            using var reader = XmlReader.Create("geburtszeichen.xml");
            var serializer = new XmlSerializer(typeof(Geburtszeichen[]));
            var deserializedObject = serializer.Deserialize(reader);
            var result = ((Geburtszeichen[])deserializedObject).ToDictionary(x => x.Index);
            Validate(result);
            return result;
        }

        private static void Validate(Dictionary<int, Geburtszeichen> result)
        {
            if (result.Count != 30)
            {
                throw new Exception("Geburtszeichen müssen genau 30 Einträge besitzen.");
            }

            foreach (var i in Enumerable.Range(1, result.Count))
            {
                if (!result.ContainsKey(i))
                {
                    throw new Exception($"Für den Wert {i} fehlt ein Geburtszeichen");
                }
            }
        }

        public static void Save(List<Geburtszeichen> geburtszeichen)
        {
            using var writer = XmlWriter.Create("geburtszeichen.xml", new XmlWriterSettings { Indent = true });
            var serializer = new XmlSerializer(typeof(List<Geburtszeichen>));
            serializer.Serialize(writer, geburtszeichen);
        }

        public override string ToString()
        {
            return $"{Index}: {Name}; {Schicksalswurf} ({Bonus.ToString("+0;-0;0", CultureInfo.InvariantCulture)})";
        }
    }
}