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
        
        public int Grad { get; set; }

        private const string FileName = "zauber.xml";
        private static readonly Lazy<Dictionary<Zaubertyp, Dictionary<int, List<ZauberTemplate>>>> ZauberDict = new Lazy<Dictionary<Zaubertyp, Dictionary<int, List<ZauberTemplate>>>>(Load);

        public static List<ZauberTemplate> Get(Zaubertyp typ, int grad)
        {
            return ZauberDict.Value[typ][grad];
        }

        public static Dictionary<Zaubertyp, Dictionary<int, List<ZauberTemplate>>> Load()
        {
            var dict = Serializer.Load<ZauberTemplate>(FileName).GroupBy(x => x.Typ).ToDictionary(x => x.Key, x => x.GroupBy(y => y.Grad).ToDictionary(y => y.Key, y => y.ToList()));
            Validate(dict);
            return dict;
        }

        public static void Save(List<ZauberTemplate> zauber)
        {
            Serializer.Save(FileName, zauber);
        }

        public static void Validate(Dictionary<Zaubertyp, Dictionary<int, List<ZauberTemplate>>> dict)
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
