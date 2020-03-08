using System.Xml.Serialization;

namespace DccCharCreator.core.Zauberbuch
{
    public enum Zaubertyp
    {
        [XmlEnum(Name = "None")] None,
        [XmlEnum(Name = "Zauberkundigenzauber")] Zauberkundigenzauber,
        [XmlEnum(Name = "Klerikerzauber")] Klerikerzauber,
        [XmlEnum(Name = "Patronzauber")] Patronzauber
    }
}