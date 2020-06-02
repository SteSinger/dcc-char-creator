using DccCharCreator.core.Würfel;
using DccCharCreator.core.Zauberbuch;
using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core.CharacterData.Klasse
{
    public class Kleriker : KlasseBase
    {
        public Kleriker(int seed, int charakterNummer, int stufe, Gesinnung gesinnung) : base(seed, charakterNummer, stufe, 8, gesinnung)
        {
            var factory = new WürfelFactory(random);
            var zauberFactory = new ZauberFactory(factory.W100, factory.W27, factory._4W20, factory.W4, factory.W6, factory.W8, factory.W10, factory.W3, factory.W11);

            Zauberbuch = zauberFactory.KlerikerZauberErstellen(stufe, Character.Attribute.Glück.Modifikator, false, random);

            var startGold = wf.W(20, 4) + stufe switch
            {
                1 => 0,
                2 => 400,
                _ => 1300,
            };

            Startkapital = $"{startGold} GM";
        }

        public IList<Zauber> Zauberbuch {get; set;}

        protected override string AktionswürfelLookup(int stufe) => stufe switch
        {
            1 => "1W20",
            2 => "1W20",
            3 => "1W20",
            4 => "1W20",
            5 => "1W20",
            6 => "1W20+1W14",
            7 => "1W20+1W16",
            8 => "1W20+1W20",
            9 => "1W20+1W20",
            10 => "1W20+1W20",
            _ => ""
        };

        protected override string AngriffsmodifikatorLookup(int stufe) => 
            stufe switch
            {
                1 => "0",
                2 => "1",
                3 => "2",
                4 => "2",
                5 => "3",
                6 => "4",
                7 => "5",
                8 => "5",
                9 => "6",
                10 => "7",
                _ => "0",
            };

        protected override string KritTabelleLookup(int stufe) => "III";

        protected override string KritWürfelLookup(int stufe) => stufe switch
        {
            1 => "1W8",
            2 => "1W8",
            3 => "1W10",
            4 => "1W10",
            5 => "1W12",
            6 => "1W12",
            7 => "1W14",
            8 => "1W14",
            9 => "1W16",
            10 => "1W16",
            _ => ""
        };
        protected override int ReflexLookup(int stufe) => stufe switch
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
            _ => 0,
        };


        protected override string TitelLookup(Gesinnung gesinnung, int stufe) => (gesinnung, stufe) switch
        {
            (Gesinnung.Rechtschaffen, 1) => "Akolyth",
            (Gesinnung.Rechtschaffen, 2) => "Heidentöter",
            (Gesinnung.Rechtschaffen, 3) => "Bruder",
            (Gesinnung.Rechtschaffen, 4) => "Kurator",
            (Gesinnung.Rechtschaffen, 5) => "Vater",
            (Gesinnung.Neutral, 1) => "Zelot",
            (Gesinnung.Neutral, 2) => "Konvertit",
            (Gesinnung.Neutral, 3) => "Kultist",
            (Gesinnung.Neutral, 4) => "Apostel",
            (Gesinnung.Neutral, 5) => "Hohepriester",
            (Gesinnung.Chaotisch, 1) => "Zeuge",
            (Gesinnung.Chaotisch, 2) => "Schüler",
            (Gesinnung.Chaotisch, 3) => "Chronist",
            (Gesinnung.Chaotisch, 4) => "Richter",
            (Gesinnung.Chaotisch, 5) => "Druide",
            _ => ""
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
            _ => 0,
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
            _ => 0,
        };

        public int Zauberstufe => Stufe;


    }
}
