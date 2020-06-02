using DccCharCreator.core.Würfel;
using DccCharCreator.core.Zauberbuch;
using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core.CharacterData.Klasse
{
    public class Elf : KlasseBase
    {
        public IList<Zauber> Zauberbuch { get; private set; }
        public int Zauberstufe => Stufe;

        public Elf(int seed, int charakterNummer, int stufe, Gesinnung gesinnung) : base(seed, charakterNummer, stufe, 6, gesinnung)
        {
            Sprachen.Add($"Elfensprache");
            // Eine Sprache pro Intelligenzmodifikator
            for (var i = 1; i <= Character.Attribute.Intelligenz.Modifikator; i++)
            {
                Sprachen.Add($"Magier Sprache {i}");
            }

            var startGold = stufe switch
            {
                1 => wf.W(12, 2),
                2 => wf.W(12, 3) + 500,
                _ => wf.W(12, 3) + 2000,
            };

            Startkapital = $"{startGold} GM";

            var factory = new WürfelFactory(random);
            var zauberFactory = new ZauberFactory(factory.W100, factory.W27, factory._4W20, factory.W4, factory.W6, factory.W8, factory.W10, factory.W3, factory.W11);

            Zauberbuch = zauberFactory.ElfenZauberErstellen(stufe, Character.Attribute.Glück.Modifikator);
        }

        protected override int ReflexLookup(int stufe) => stufe switch
        {
            1 => 1,
            2 => 1,
            3 => 1,
            4 => 2,
            5 => 2,
            6 => 2,
            7 => 3,
            8 => 3,
            9 => 3,
            10 => 4,
            _ => 0
        };

        protected override int ZähigkeitLookup(int stufe) => stufe switch
        {
            1 => 1,
            2 => 1,
            3 => 1,
            4 => 2,
            5 => 2,
            6 => 2,
            7 => 3,
            8 => 3,
            9 => 3,
            10 => 4,
            _ => 0
        };

        protected override int WillenskraftLookup(int stufe) => stufe switch
        {
            1 => 1,
            2 => 1,
            3 => 2,
            4 => 2,
            5 => 3,
            6 => 4,
            7 => 4,
            8 => 5,
            9 => 5,
            10 => 6,
            _ => 0
        };

        protected override string TitelLookup(Gesinnung gesinnung, int stufe)
        =>
            stufe switch
            {
                1 => "Wanderer",
                2 => "Seher",
                3 => "Sucher",
                4 => "Gelehrter",
                5 => "Ältester",
                _ => ""
            };

        protected override string KritWürfelLookup(int stufe) => stufe switch
        {
            1 => "1W6",
            2 => "1W8",
            3 => "1W8",
            4 => "1W10",
            5 => "1W10",
            6 => "1W12",
            7 => "1W12",
            8 => "1W14",
            9 => "1W14",
            10 => "1W16",
            _ => ""
        };

        protected override string KritTabelleLookup(int stufe) => "II";

        protected override string AktionswürfelLookup(int stufe) => stufe switch
        {
            1 => "1W20",
            2 => "1W20",
            3 => "1W20",
            4 => "1W20",
            5 => "1W20+1W14",
            6 => "1W20+1W16",
            7 => "1W20+1W20",
            8 => "1W20+1W20",
            9 => "1W20+1W20",
            10 => "1W20+1W20+1W14",
            _ => ""
        };

        protected override string AngriffsmodifikatorLookup(int stufe)
        => stufe switch
        {
            1 => "1",
            2 => "1",
            3 => "2",
            4 => "2",
            5 => "3",
            6 => "3",
            7 => "4",
            8 => "4",
            9 => "5",
            10 => "5",
            _ => ""
        };

    }
}
