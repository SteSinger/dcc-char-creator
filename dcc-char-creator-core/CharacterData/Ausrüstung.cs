using DccCharCreator.core.Würfel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DccCharCreator.core.CharacterData
{
    public class Ausrüstung
    {
        public int Wurf { get; set; }
        public string Gegenstand { get; set; } = string.Empty;
        public string Preis { get; set; } = string.Empty;

        private const string fileName = "ausruestung.xml";
        private static readonly Lazy<Dictionary<int, Ausrüstung>> Handelswaren = new Lazy<Dictionary<int, Ausrüstung>>(Load());

        public static Ausrüstung Random(IW24 würfel)
        {
            return Handelswaren.Value[würfel.Würfeln()];
        }

        public static Dictionary<int, Ausrüstung> Load()
        {
            return Serializer.Load<Ausrüstung>(fileName, Validate, x => x.Wurf);
        }

        private static void Validate(Dictionary<int, Ausrüstung> result)
        {
            if (result.Count != 24)
            {
                throw new Exception("Handelswaren müssen genau 24 Einträge besitzen.");
            }

            foreach (var i in Enumerable.Range(1, result.Count))
            {
                if (!result.ContainsKey(i))
                {
                    throw new Exception($"Für den Wert {i} fehlt eine Handelsware.");
                }
            }
        }

        public static void Save(List<Ausrüstung> handelswaren)
        {
            Serializer.Save(fileName, handelswaren);
        }

        public override string ToString()
        {
            return $"{Wurf}: {Gegenstand} {Preis}";
        }
    }
}
