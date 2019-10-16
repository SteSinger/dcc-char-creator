using DccCharCreator.core.CharacterData;

namespace DccCharCreator.web
{
    public static class FormatExtensions
    {
        public static string Format(this Attribut attribut) => $"{attribut.Value} ({attribut.BonusFormatted})";

        public static string Format(this Geburtszeichen gz) => $"{gz.Name}; {gz.Schicksalswurf} ({gz.Bonus})";

    }
}
