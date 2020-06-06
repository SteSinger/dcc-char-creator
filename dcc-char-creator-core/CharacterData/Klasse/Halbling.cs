using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core.CharacterData.Klasse
{
    public class Halbling : KlasseBase
    {
        public Halbling(int seed, int charakterNummer, int stufe, Gesinnung gesinnung) : base(seed, charakterNummer, stufe, 6, gesinnung)
        {
            Sprachen.Add($"Halblingssprache");

            var startGold = wf.W(20, 3) + stufe switch
            {
                1 => 0,
                2 => 250,
                _ => 1500,
            };

            Startkapital = $"{startGold} GM";
        }

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

        protected override string AngriffsmodifikatorLookup(int stufe) => stufe switch
        {
            1 => "1",
            2 => "2",
            3 => "2",
            4 => "3",
            5 => "4",
            6 => "5",
            7 => "5",
            8 => "6",
            9 => "7",
            10 => "8",
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


        protected override string TitelLookup(Gesinnung gesinnung, int stufe) => stufe switch
        {
            1 => "Wanderer",
            2 => "Entdecker",
            3 => "Sammler",
            4 => "Großsammler",
            5 => "Weiser",
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

        public int schleichenVerstecken => Stufe switch
        {
            1 => 3,
            2 => 5,
            3 => 7,
            4 => 8,
            5 => 9,
            6 => 11,
            7 => 12,
            8 => 13,
            9 => 14,
            10 => 15,
            _ => 0
        };
    }
}
