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
            return $"ST: {Stärke.Value}({Stärke.ModifikatorFormattiert}); GE: {Geschicklichkeit.Value}({Geschicklichkeit.ModifikatorFormattiert}); AU: {Ausdauer.Value}({Ausdauer.ModifikatorFormattiert}); PE: {Persönlichkeit.Value}({Persönlichkeit.ModifikatorFormattiert}); IN: {Intelligenz.Value}({Intelligenz.ModifikatorFormattiert}); GL: {Glück.Value}({Glück.ModifikatorFormattiert}); ";
        }
    }
}
