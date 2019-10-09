using DccCharCreator.core.Dice;
using System;
using System.Text;

namespace DccCharCreator.core
{
    public class Character
    {
        int Trefferpunkte { get; set; }

        Attribute Attribute { get; }

        Beruf Beruf { get; }

        Geburtszeichen Geburtszeichen { get; }

        string Geld { get; set; }

        string Zähigkeit => Attribute.Ausdauer.BonusFormatted;
        string Refelexe => Attribute.Geschicklichkeit.BonusFormatted;
        string Willenskraft => Attribute.Persönlichkeit.BonusFormatted;

        public Character(I3D6 attributWürfel, ID100 berufWürfel, ID30 zeichenWürfel, ID4 trefferpunkteWürfel, I5D12 geldWürfel)
        {
            Attribute = new Attribute(attributWürfel);
            Beruf = Beruf.Random(berufWürfel);
            Geburtszeichen = Geburtszeichen.Random(zeichenWürfel, Attribute.Glück.Bonus);
            Trefferpunkte = Math.Max(trefferpunkteWürfel.Roll() + Attribute.Ausdauer.Bonus, 1);
            Geld = $"{geldWürfel.Roll()} KM";
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

            return sb.ToString();
        }

    }
}
