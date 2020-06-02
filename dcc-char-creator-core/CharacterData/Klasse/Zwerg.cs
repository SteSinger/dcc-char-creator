using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core.CharacterData.Klasse
{
    public class Zwerg : KlasseBase
    {
        public Zwerg(int seed, int charakterNummer, int stufe, Gesinnung gesinnung) : base(seed, charakterNummer, stufe, 10, gesinnung)
        {
            Sprachen.Add($"Zwergensprache");

            var startGold = wf.W(12, 5) + stufe switch
            {
                1 => 0,
                2 => 700,
                _ => 2000,
            };

            Startkapital = $"{startGold} GM";
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

        protected override int WillenskraftLookup(int stufe) => stufe switch
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

        protected override string TitelLookup(Gesinnung gesinnung, int stufe)
        =>
            (gesinnung, stufe) switch
            {
                (Gesinnung.Rechtschaffen, 1) => "Sachwalter",
                (Gesinnung.Rechtschaffen, 2) => "Vermittler",
                (Gesinnung.Rechtschaffen, 3) => "Emissär",
                (Gesinnung.Rechtschaffen, 4) => "Legat",
                (Gesinnung.Rechtschaffen, 5) => "Syndikus",
                (Gesinnung.Neutral, 1) => "Lehrling",
                (Gesinnung.Neutral, 2) => "Novize",
                (Gesinnung.Neutral, 3) => "Wandergeselle",
                (Gesinnung.Neutral, 4) => "Kunsthandwerker",
                (Gesinnung.Neutral, 5) => "Thegn",
                (Gesinnung.Chaotisch, 1) => "Rebell",
                (Gesinnung.Chaotisch, 2) => "Dissident",
                (Gesinnung.Chaotisch, 3) => "Exilant",
                (Gesinnung.Chaotisch, 4) => "Bilderstürmer",
                (Gesinnung.Chaotisch, 5) => "Renegat",
                (_, _) => ""
            };

        protected override string KritWürfelLookup(int stufe) => stufe switch
        {
            1 => "1W10",
            2 => "1W12",
            3 => "1W14",
            4 => "1W16",
            5 => "1W20",
            6 => "1W24",
            7 => "1W30",
            8 => "1W30",
            9 => "2W20",
            10 => "2W20",
            _ => ""
        };

        protected override string KritTabelleLookup(int stufe) => stufe switch
        {
            1 => "III",
            2 => "III",
            3 => "III",
            4 => "IV",
            5 => "IV",
            6 => "V",
            7 => "V",
            8 => "V",
            9 => "V",
            10 => "V",
            _ => ""
        };

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
            1 => "W3",
            2 => "W4",
            3 => "W5",
            4 => "W6",
            5 => "W7",
            6 => "W8",
            7 => "W10+1",
            8 => "W10+2",
            9 => "W10+3",
            10 => "W10+4",
            _ => ""
        };
    }
}
