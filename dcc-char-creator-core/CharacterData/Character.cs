using DccCharCreator.core.Würfel;
using System;
using System.Text;

namespace DccCharCreator.core.CharacterData
{
    public class Character
    {
        public int Trefferpunkte { get; }

        public Attribute Attribute { get; }

        public Beruf Beruf { get; }

        public Geburtszeichen Geburtszeichen { get; }

        public Ausrüstung Ausrüstung { get; }

        public string Startkapital { get; set; }

        public string Zähigkeit => Attribute.Ausdauer.BonusFormatted;
        public string Reflexe => Attribute.Geschicklichkeit.BonusFormatted;
        public string Willenskraft => Attribute.Persönlichkeit.BonusFormatted;
        public string Initiative => Attribute.Geschicklichkeit.BonusFormatted;
        public int Rüstungsklasse => 10 + Attribute.Geschicklichkeit.Bonus;

        public Character(I3W6 attributWürfel, IW100 berufWürfel, IW30 zeichenWürfel, IW4 trefferpunkteWürfel, I5W12 geldWürfel, IW24 handelsWarenWürfel)
        {
            Attribute = new Attribute(attributWürfel);
            Beruf = Beruf.Random(berufWürfel);
            Geburtszeichen = Geburtszeichen.Random(zeichenWürfel, Attribute.Glück.Bonus);
            Trefferpunkte = Math.Max(trefferpunkteWürfel.Würfeln() + Attribute.Ausdauer.Bonus, 1);
            Startkapital = $"{geldWürfel.Würfeln()} KM";
            Ausrüstung = Ausrüstung.Random(handelsWarenWürfel);
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Attribute: ").AppendLine(Attribute.ToString());
            sb.Append("TP: ").Append(Trefferpunkte.ToString());
            sb.Append(" RK: ").Append(Rüstungsklasse.ToString());
            sb.Append(" INI: ").AppendLine(Initiative);
            sb.Append("Beruf: ").AppendLine(Beruf.ToString());
            sb.Append("Geburtszeichen: ").AppendLine(Geburtszeichen.ToString());
            sb.Append("Rettungswürfe: ").AppendLine($"Zähigkeit: {Zähigkeit}, Reflexe: {Reflexe}, Willenskraft: {Willenskraft}");
            sb.Append("Geld: ").AppendLine(Startkapital);
            sb.Append("Ausrüstung: ").AppendLine(Ausrüstung.ToString());
            
            return sb.ToString();
        }

    }
}
