using DccCharCreator.core.Würfel;

namespace DccCharCreator.core.CharacterData
{
    public class Attribute
    {
        public Attribut Stärke { get; set; }
        public Attribut Geschicklichkeit { get; set; }
        public Attribut Ausdauer { get; set; }
        public Attribut Persönlichkeit { get; set; }
        public Attribut Intelligenz { get; set; }
        public Attribut Glück { get; set; }

        public Attribute(I3W6 dice)
        {
            Stärke = new Attribut(dice.Würfeln());
            Geschicklichkeit = new Attribut(dice.Würfeln());
            Ausdauer = new Attribut(dice.Würfeln());
            Persönlichkeit = new Attribut(dice.Würfeln());
            Intelligenz = new Attribut(dice.Würfeln());
            Glück = new Attribut(dice.Würfeln());
        }
        public override string ToString()
        {
            return $"ST: {Stärke.Value}({Stärke.BonusFormatted}); GE: {Geschicklichkeit.Value}({Geschicklichkeit.BonusFormatted}); AU: {Ausdauer.Value}({Ausdauer.BonusFormatted}); PE: {Persönlichkeit.Value}({Persönlichkeit.BonusFormatted}); IN: {Intelligenz.Value}({Intelligenz.BonusFormatted}); GL: {Glück.Value}({Glück.BonusFormatted}); ";
        }
    }
}
