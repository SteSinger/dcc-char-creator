using DccCharCreator.core.Würfel;
using DccCharCreator.core.Zauberbuch;
using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core.CharacterData.Klasse
{
    public class Zauberkundiger : KlasseBase
    {
        public Zauberkundiger(int seed, int charakterNummer, int stufe, Gesinnung gesinnung) : base(seed, charakterNummer, stufe, 4, gesinnung)
        {
            // Zwei Sprachen pro Intelligenzmodifikator
            for (var i = 1; i <= Character.Attribute.Intelligenz.Modifikator * 2; i++)
            {
                Sprachen.Add($"Magier Sprache {i}");
            }

            var startGold = wf.W(10, 3) + stufe switch
            {
                1 => 0,
                2 => wf.W(4, 2) * 100,
                _ => wf.W(4, 5) * 100,
            };

            Startkapital = $"{startGold} GM";

            var factory = new WürfelFactory(random);
            var zauberFactory = new ZauberFactory(factory.W100, factory.W27, factory._4W20, factory.W4, factory.W6, factory.W8, factory.W10, factory.W3, factory.W11);

            Zauberbuch = zauberFactory.ZauberkundigenZauberErstellen(stufe, Character.Attribute.Glück.Modifikator);
        }

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
            1 => 0,
            2 => 0,
            3 => 1,
            4 => 1,
            5 => 1,
            6 => 2,
            7 => 2,
            8 => 2,
            9 => 3,
            10 => 3,
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
            (gesinnung, stufe) switch
            {
                (Gesinnung.Rechtschaffen, 1) => "Hervorrufer",
                (Gesinnung.Rechtschaffen, 2) => "Beherrscher",
                (Gesinnung.Rechtschaffen, 3) => "Zauberer",
                (Gesinnung.Rechtschaffen, 4) => "Beschwörer",
                (Gesinnung.Rechtschaffen, 5) => "Elementarist",
                (Gesinnung.Neutral, 1) => "Sterndeuter",
                (Gesinnung.Neutral, 2) => "Verzauberer",
                (Gesinnung.Neutral, 3) => "Magier",
                (Gesinnung.Neutral, 4) => "Thaumaturg",
                (Gesinnung.Neutral, 5) => "Hexenmeister",
                (Gesinnung.Chaotisch, 1) => "Kultist",
                (Gesinnung.Chaotisch, 2) => "Schamane",
                (Gesinnung.Chaotisch, 3) => "Teufelspaktierer",
                (Gesinnung.Chaotisch, 4) => "Hexe",
                (Gesinnung.Chaotisch, 5) => "Nekromant",
                (_, _) => ""
            };

        protected override string KritWürfelLookup(int stufe) => stufe switch
        {
            1 => "1W6",
            2 => "1W6",
            3 => "1W8",
            4 => "1W8",
            5 => "1W10",
            6 => "1W10",
            7 => "1W12",
            8 => "1W12",
            9 => "1W14",
            10 => "1W14",
            _ => ""
        };

        protected override string KritTabelleLookup(int stufe) => "I";

        protected override string AngriffsmodifikatorLookup(int stufe) => stufe switch
        {
            1 => "0",
            2 => "1",
            3 => "1",
            4 => "1",
            5 => "2",
            6 => "2",
            7 => "3",
            8 => "3",
            9 => "4",
            10 => "4",
            _ => ""
        };

        public int Zauberstufe => Stufe;

        public IList<Zauber> Zauberbuch { get; set; }
    }
}
