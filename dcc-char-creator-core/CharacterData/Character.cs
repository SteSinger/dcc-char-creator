using DccCharCreator.core.Würfel;
using System;
using System.Text;

namespace DccCharCreator.core.CharacterData
{
    public class Character
    {
        int Trefferpunkte { get; set; }

        Attribute Attribute { get; }

        Beruf Beruf { get; }

        Geburtszeichen Geburtszeichen { get; }

        Handelsware Handelsware { get; }

        string Geld { get; set; }

        string Zähigkeit => Attribute.Ausdauer.BonusFormatted;
        string Refelexe => Attribute.Geschicklichkeit.BonusFormatted;
        string Willenskraft => Attribute.Persönlichkeit.BonusFormatted;

        public Character(I3W6 attributWürfel, IW100 berufWürfel, IW30 zeichenWürfel, IW4 trefferpunkteWürfel, I5W12 geldWürfel, IW24 handelsWarenWürfel)
        {
            Attribute = new Attribute(attributWürfel);
            Beruf = Beruf.Random(berufWürfel);
            Geburtszeichen = Geburtszeichen.Random(zeichenWürfel, Attribute.Glück.Bonus);
            Trefferpunkte = Math.Max(trefferpunkteWürfel.Würfeln() + Attribute.Ausdauer.Bonus, 1);
            Geld = $"{geldWürfel.Würfeln()} KM";
            Handelsware = Handelsware.Random(handelsWarenWürfel);
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Attribute: ").AppendLine(Attribute.ToString());
            sb.Append("Trefferpunkte: ").AppendLine(Trefferpunkte.ToString());
            sb.Append("Beruf: ").AppendLine(Beruf.ToString());
            sb.Append("Geburtszeichen: ").AppendLine(Geburtszeichen.ToString());
            sb.Append("Rettungswürfe: ").AppendLine($"Zähigkeit: {Zähigkeit}, Reflexe: {Refelexe}, Willenskraft: {Willenskraft}");
            sb.Append("Geld: ").AppendLine(Geld);
            sb.Append("Handelsware: ").AppendLine(Handelsware.ToString());
            return sb.ToString();
        }

    }
}
