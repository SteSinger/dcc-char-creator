using DccCharCreator.core.Würfel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DccCharCreator.core.CharacterData
{
    public class Handelsware
    {
        public int Wurf { get; set; }
        public string Gegenstand { get; set; } = string.Empty;
        public string Preis { get; set; } = string.Empty;

        private const string fileName = "handelswaren.xml";
        private static readonly Lazy<Dictionary<int, Handelsware>> Handelswaren = new Lazy<Dictionary<int, Handelsware>>(Load());

        public static Handelsware Random(IW24 würfel)
        {
            return Handelswaren.Value[würfel.Würfeln()];
        }

        public static Dictionary<int, Handelsware> Load()
        {
            return Serializer.Load<Handelsware>(fileName, Validate, x => x.Wurf);
        }

        private static void Validate(Dictionary<int, Handelsware> result)
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

        public static void Save(List<Handelsware> handelswaren)
        {
            Serializer.Save(fileName, handelswaren);
        }

        public override string ToString()
        {
            return $"{Wurf}: {Gegenstand} {Preis}";
        }
    }
}
