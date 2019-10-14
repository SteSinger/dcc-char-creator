using DccCharCreator.core.Würfel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DccCharCreator.core.CharacterData
{
    public class Geburtszeichen
    {
        public int Wurf { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Schicksalswurf { get; set; } = string.Empty;
        public int Bonus { get; set; }

        private const string fileName = "geburtszeichen.xml";
        private static Lazy<Dictionary<int, Geburtszeichen>> GeburtszeichenDict = new Lazy<Dictionary<int, Geburtszeichen>>(Load);

        public Geburtszeichen()
        {
        }

        public Geburtszeichen(Geburtszeichen zeichen, int bonus)
        {
            Wurf = zeichen.Wurf;
            Name = zeichen.Name;
            Schicksalswurf = zeichen.Schicksalswurf;
            Bonus = bonus;
        }

        public static Geburtszeichen Random(IW30 dice, int bonus)
        {
            return new Geburtszeichen(GeburtszeichenDict.Value[dice.Würfeln()], bonus);
        }

        public static Dictionary<int, Geburtszeichen> Load()
        {
            return Serializer.Load<Geburtszeichen>(fileName, Validate, x => x.Wurf);
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
            Serializer.Save(fileName, geburtszeichen);
        }

        public override string ToString()
        {
            return $"{Wurf}: {Name}; {Schicksalswurf} ({Bonus.ToString("+0;-0;0", CultureInfo.InvariantCulture)})";
        }
    }
}