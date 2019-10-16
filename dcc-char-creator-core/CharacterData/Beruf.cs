using DccCharCreator.core.Würfel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DccCharCreator.core.CharacterData
{
    public class Beruf
    {
        public int Wurf { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Startwaffe { get; set; } = string.Empty;

        public string Handelsware { get; set; } = string.Empty;

        public Rasse Rasse { get; set; }

        private const string FileName = "berufe.xml";
        private static readonly Lazy<Dictionary<int, Beruf>> Berufe = new Lazy<Dictionary<int, Beruf>>(Load);

        public static Beruf Random(IW100 berufDice)
        {
            return Berufe.Value[berufDice.Würfeln()];
        }

        public static Dictionary<int, Beruf> Load()
        {
            return Serializer.Load<Beruf>(FileName, Validate, x => x.Wurf);
        }

        private static void Validate(Dictionary<int, Beruf> result)
        {
            if (result.Count != 100)
            {
                throw new Exception("Die Jobliste muss genau 100 Einträge enthalten");
            }

            foreach (var i in Enumerable.Range(1, result.Count))
            {
                if (!result.ContainsKey(i))
                {
                    throw new Exception($"Für den Wert {i} fehlt ein Beruf");
                }
            }
        }

        public string Rassenvorteile()
        {
            return Rasse switch
            {
                Rasse.Mensch => string.Empty,
                Rasse.Halbling => "Dunkelsicht, Bewegungsrate 6m",
                Rasse.Elf => "Reagiert empfindlich auf Eisen, geschärfte Sinne",
                Rasse.Zwerg => "Dunkelsicht, Bewegungsrate 6m",
                _ => throw new Exception($"Rasse {Rasse} nicht definiert")
            };
        }


        public static void Save(List<Beruf> berufe)
        {
            Serializer.Save(FileName, berufe);
        }

        public override string ToString()
        {
            return $"{Wurf}: {Name}; {Startwaffe}; {Handelsware}; {Rassenvorteile()}";
        }
    }
}