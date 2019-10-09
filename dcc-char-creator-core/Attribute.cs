using DccCharCreator.core.Dice;
using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core
{
    public class Attribute
    {
        public Attribut Stärke { get; set; }
        public Attribut Geschicklichkeit { get; set; }
        public Attribut Ausdauer { get; set; }
        public Attribut Persönlichkeit { get; set; }
        public Attribut Intelligenz { get; set; }
        public Attribut Glück { get; set; }

        public Attribute(I3D6 dice)
        {
            Stärke = new Attribut(dice.Roll());
            Geschicklichkeit = new Attribut(dice.Roll());
            Ausdauer = new Attribut(dice.Roll());
            Persönlichkeit = new Attribut(dice.Roll());
            Intelligenz = new Attribut(dice.Roll());
            Glück = new Attribut(dice.Roll());
        }
        public override string ToString()
        {
            return $"ST: {Stärke.value}({Stärke.BonusFormatted}); GE: {Geschicklichkeit.value}({Geschicklichkeit.BonusFormatted}); AU: {Ausdauer.value}({Ausdauer.BonusFormatted}); PE: {Persönlichkeit.value}({Persönlichkeit.BonusFormatted}); IN: {Intelligenz.value}({Intelligenz.BonusFormatted}); GL: {Glück.value}({Glück.BonusFormatted}); ";
        }
    }
}
