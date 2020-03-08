using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace DccCharCreator.core.Zauberbuch
{
    public class ZauberTemplate
    {
        public int Wurf { get; set; }

        public string Seite { get; set; } = string.Empty;

        [XmlArray("Manifestationen")]
        [XmlArrayItem("Manifestation")]
        public List<Manifestation> Manifestationen { get; set; } = new List<Manifestation>();

        public Zaubertyp Typ { get; set; }
        
        public string Beschreibung { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        private const string FileName = "zauber.xml";
        private static readonly Lazy<Dictionary<Zaubertyp, Dictionary<int, ZauberTemplate>>> ZauberDict = new Lazy<Dictionary<Zaubertyp, Dictionary<int, ZauberTemplate>>>(Load);

        public static ZauberTemplate Get(Zaubertyp typ, int wurf)
        {
            return ZauberDict.Value[typ][wurf];
        }

        public static Dictionary<Zaubertyp, Dictionary<int, ZauberTemplate>> Load()
        {
            var dict = Serializer.Load<ZauberTemplate>(FileName).GroupBy(x => x.Typ).ToDictionary(x => x.Key, x => x.ToDictionary(y => y.Wurf));
            Validate(dict);
            return dict;
        }

        public static void Save(List<ZauberTemplate> zauber)
        {
            Serializer.Save(FileName, zauber);
        }

        public static void Validate(Dictionary<Zaubertyp, Dictionary<int, ZauberTemplate>> dict)
        {
            foreach(var entry in dict)
            {
                if(entry.Key != Zaubertyp.Zauberkundigenzauber && entry.Key != Zaubertyp.Klerikerzauber && entry.Key != Zaubertyp.Patronzauber)
                {
                    throw new Exception("Ungültiger Zaubertyp in Zauberliste");
                }
            }
        }
    }
}
