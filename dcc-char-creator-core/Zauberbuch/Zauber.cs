using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DccCharCreator.core.Zauberbuch
{
    public class Zauber
    {
        public int Wurf { get; set; }

        public string Seite { get; set; } = string.Empty;
        public string Manifestation { get; set; } = string.Empty;

        public Zaubertyp Typ { get; set; }

        [XmlIgnore]
        public IList<LauneDerMagie> LaunenDerMagie { get; set; } = new List<LauneDerMagie>();

        public string Beschreibung { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        private const string FileName = "zauber.xml";
        private static readonly Lazy<Dictionary<Zaubertyp, Dictionary<int, Zauber>>> ZauberDict = new Lazy<Dictionary<Zaubertyp, Dictionary<int, Zauber>>>(Load);

        public static Zauber Get(Zaubertyp typ, int wurf)
        {
            return (Zauber)ZauberDict.Value[typ][wurf].MemberwiseClone();
        }

        public static Dictionary<Zaubertyp, Dictionary<int, Zauber>> Load()
        {
            return Serializer.Load<Zauber>(FileName).GroupBy(x => x.Typ).ToDictionary(x => x.Key, x => x.ToDictionary(y => y.Wurf));
        }
    }
}
