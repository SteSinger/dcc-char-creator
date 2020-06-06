using DccCharCreator.core.Würfel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core.CharacterData.Klasse
{
    public class Krieger : KlasseBase
    {
        public Krieger(int seed, int charakterNummer, int stufe, Gesinnung gesinnung) : base(seed, charakterNummer, stufe, 12, gesinnung)
        {
            var startGold = wf.W(12, 5) + stufe switch
            {
                1 => 0,
                2 => 500,
                _ => 1500,
            };

            Startkapital = $"{startGold} GM";
        }

        public override int Initiative => base.Initiative + Stufe;

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

        protected override string TitelLookup(Gesinnung gesinnung, int stufe)
        =>
            (gesinnung, stufe) switch
            {
                (Gesinnung.Rechtschaffen, 1) => "Knappe",
                (Gesinnung.Rechtschaffen, 2) => "Recke",
                (Gesinnung.Rechtschaffen, 3) => "Ritter",
                (Gesinnung.Rechtschaffen, 4) => "Kavalier",
                (Gesinnung.Rechtschaffen, 5) => "Paladin",
                (Gesinnung.Neutral, 1) => "Wildling",
                (Gesinnung.Neutral, 2) => "Barbar",
                (Gesinnung.Neutral, 3) => "Berserker",
                (Gesinnung.Neutral, 4) => "Anführer",
                (Gesinnung.Neutral, 5) => "Häuptling",
                (Gesinnung.Chaotisch, 1) => "Bandit",
                (Gesinnung.Chaotisch, 2) => "Brigant",
                (Gesinnung.Chaotisch, 3) => "Marodeur",
                (Gesinnung.Chaotisch, 4) => "Verwüster",
                (Gesinnung.Chaotisch, 5) => "Plünderer",
                (_, _) => ""
            };

        private string[] KritBereicheLookup = new[] { "19-20", "19-20", "19-20", "19-20", "18-20", "18-20", "18-20", "18-20", "17-20", "17-20" };
        protected override string KritWürfelLookup(int stufe) => stufe switch
        {
            1 => "1W12",
            2 => "1W14",
            3 => "1W16",
            4 => "1W20",
            5 => "1W24",
            6 => "1W30",
            7 => "1W30",
            8 => "2W20",
            9 => "2W20",
            10 => "2W20",
            _ => ""
        };

        protected override string KritTabelleLookup(int stufe) => stufe switch
        {
            1 => "III",
            2 => "III",
            3 => "IV",
            4 => "IV",
            5 => "V",
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

        public string KritBereich => KritBereicheLookup[Stufe];
    }
}
