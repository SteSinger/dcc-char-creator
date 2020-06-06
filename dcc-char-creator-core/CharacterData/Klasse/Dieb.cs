using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core.CharacterData.Klasse
{
    public class Dieb : KlasseBase
    {
        public Dieb(int seed, int charakterNummer, int stufe, Gesinnung gesinnung) : base(seed, charakterNummer, stufe, 6, gesinnung)
        {
            Sprachen.Add("Rotwelsch");

            var startGold = wf.W(10, 3) + stufe switch
            {
                1 => 0,
                2 => wf.W(6, 1) * 100,
                _ => wf.W(6, 3) * 100,
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

        public string Glückswürfel => GlückswürfelLookup(Stufe);

        private string GlückswürfelLookup(int stufe) => stufe switch
        {
            1 => "3",
            2 => "4",
            3 => "5",
            4 => "6",
            5 => "7",
            6 => "8",
            7 => "10",
            8 => "12",
            9 => "14",
            10 => "16",
            _ => ""
        };

        protected override string AngriffsmodifikatorLookup(int stufe) => stufe switch
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

        protected override string KritTabelleLookup(int stufe) => "II";


        protected override string KritWürfelLookup(int stufe) => stufe switch
        {
            1 => "1W10",
            2 => "1W12",
            3 => "1W14",
            4 => "1W16",
            5 => "1W20",
            6 => "1W24",
            7 => "1W30",
            8 => "1W30+2",
            9 => "1W30+4",
            10 => "1W30+6",
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


        protected override string TitelLookup(Gesinnung gesinnung, int stufe) => (gesinnung, stufe) switch
        {
            (Gesinnung.Rechtschaffen, 1) => "Gedungener",
            (Gesinnung.Rechtschaffen, 2) => "Lehrling",
            (Gesinnung.Rechtschaffen, 3) => "Schurke",
            (Gesinnung.Rechtschaffen, 4) => "Capo",
            (Gesinnung.Rechtschaffen, 5) => "Pate",
            (Gesinnung.Neutral, 1) => "Schläger",
            (Gesinnung.Neutral, 2) => "Mörder",
            (Gesinnung.Neutral, 3) => "Halsabschneider",
            (Gesinnung.Neutral, 4) => "Henker",
            (Gesinnung.Neutral, 5) => "Attentäter",
            (Gesinnung.Chaotisch, 1) => "Bettler",
            (Gesinnung.Chaotisch, 2) => "Beutelschneider",
            (Gesinnung.Chaotisch, 3) => "Einbrecher",
            (Gesinnung.Chaotisch, 4) => "Räuber",
            (Gesinnung.Chaotisch, 5) => "Betrüger",
            _ => ""
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

        private static int progression00(int stufe) => stufe switch
        {
            1 => 0,
            2 => 0,
            3 => 1,
            4 => 2,
            5 => 3,
            6 => 4,
            7 => 5,
            8 => 6,
            9 => 7,
            10 => 8,
            _ => 0
        };

        private static int progression0(int stufe) => stufe switch
        {
            1 => 0,
            2 => 1,
            3 => 2,
            4 => 3,
            5 => 4,
            6 => 5,
            7 => 6,
            8 => 7,
            9 => 8,
            10 => 9,
            _ => 0
        };

        private static int progression1(int stufe) => stufe switch
        {
            1 => 1,
            2 => 3,
            3 => 5,
            4 => 7,
            5 => 8,
            6 => 9,
            7 => 10,
            8 => 11,
            9 => 12,
            10 => 13,
            _ => 0
        };

        private static int progression3(int stufe) => stufe switch
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

        public int HinterhältigerAngriff => Gesinnung switch
        {
            Gesinnung.Rechtschaffen => progression1(Stufe),
            Gesinnung.Chaotisch => progression3(Stufe),
            Gesinnung.Neutral => progression0(Stufe),
            _ => 0

        };
        public int LautlosSchleichen => Gesinnung switch
        {
            Gesinnung.Rechtschaffen => progression1(Stufe),
            Gesinnung.Chaotisch => progression3(Stufe),
            Gesinnung.Neutral => progression3(Stufe),
            _ => 0

        };

        public int ImSchattenVerstecken => Gesinnung switch
        {
            Gesinnung.Rechtschaffen => progression3(Stufe),
            Gesinnung.Chaotisch => progression1(Stufe),
            Gesinnung.Neutral => progression1(Stufe),
            _ => 0

        };

        public int Taschendiebstahl => Gesinnung switch
        {
            Gesinnung.Rechtschaffen => progression1(Stufe),
            Gesinnung.Chaotisch => progression0(Stufe),
            Gesinnung.Neutral => progression3(Stufe),
            _ => 0

        };

        public int Fassadenklettern => Gesinnung switch
        {
            Gesinnung.Rechtschaffen => progression3(Stufe),
            Gesinnung.Chaotisch => progression1(Stufe),
            Gesinnung.Neutral => progression3(Stufe),
            _ => 0

        };

        public int SchlösserKnacken => Gesinnung switch
        {
            Gesinnung.Rechtschaffen => progression1(Stufe),
            Gesinnung.Chaotisch => progression1(Stufe),
            Gesinnung.Neutral => progression1(Stufe),
            _ => 0

        };

        public int FallenEntdecken => Gesinnung switch
        {
            Gesinnung.Rechtschaffen => progression3(Stufe),
            Gesinnung.Chaotisch => progression1(Stufe),
            Gesinnung.Neutral => progression1(Stufe),
            _ => 0

        };

        public int FallenEntschärfen => Gesinnung switch
        {
            Gesinnung.Rechtschaffen => progression3(Stufe),
            Gesinnung.Chaotisch => progression0(Stufe),
            Gesinnung.Neutral => progression1(Stufe),
            _ => 0

        };

        public int UrkundeFälschen => Gesinnung switch
        {
            Gesinnung.Rechtschaffen => progression00(Stufe),
            Gesinnung.Chaotisch => progression00(Stufe),
            Gesinnung.Neutral => progression3(Stufe),
            _ => 0

        };

        public int Verkleiden => Gesinnung switch
        {
            Gesinnung.Rechtschaffen => progression0(Stufe),
            Gesinnung.Chaotisch => progression3(Stufe),
            Gesinnung.Neutral => progression00(Stufe),
            _ => 0

        };

        public int SprachenLesen => Gesinnung switch
        {
            Gesinnung.Rechtschaffen => progression00(Stufe),
            Gesinnung.Chaotisch => progression00(Stufe),
            Gesinnung.Neutral => progression0(Stufe),
            _ => 0

        };

        public int GiftMischen => Gesinnung switch
        {
            Gesinnung.Rechtschaffen => progression0(Stufe),
            Gesinnung.Chaotisch => progression3(Stufe),
            Gesinnung.Neutral => progression00(Stufe),
            _ => 0

        };

        public string ZauberVonEinerSchriftrolleWirken => (Gesinnung, Stufe) switch
        {
            (Gesinnung.Rechtschaffen, 1) => "10",
            (Gesinnung.Rechtschaffen, 2) => "10",
            (Gesinnung.Rechtschaffen, 3) => "12",
            (Gesinnung.Rechtschaffen, 4) => "12",
            (Gesinnung.Rechtschaffen, 5) => "14",
            (Gesinnung.Rechtschaffen, 6) => "14",
            (Gesinnung.Rechtschaffen, 7) => "16",
            (Gesinnung.Rechtschaffen, 8) => "16",
            (Gesinnung.Rechtschaffen, 9) => "20",
            (Gesinnung.Rechtschaffen, 10) => "20",

            (Gesinnung.Chaotisch, 1) => "10",
            (Gesinnung.Chaotisch, 2) => "10",
            (Gesinnung.Chaotisch, 3) => "12",
            (Gesinnung.Chaotisch, 4) => "12",
            (Gesinnung.Chaotisch, 5) => "14",
            (Gesinnung.Chaotisch, 6) => "14",
            (Gesinnung.Chaotisch, 7) => "16",
            (Gesinnung.Chaotisch, 8) => "16",
            (Gesinnung.Chaotisch, 9) => "20",
            (Gesinnung.Chaotisch, 10) => "20",

            (Gesinnung.Neutral, 1) => "12",
            (Gesinnung.Neutral, 2) => "12",
            (Gesinnung.Neutral, 3) => "14",
            (Gesinnung.Neutral, 4) => "14",
            (Gesinnung.Neutral, 5) => "16",
            (Gesinnung.Neutral, 6) => "16",
            (Gesinnung.Neutral, 7) => "20",
            (Gesinnung.Neutral, 8) => "20",
            (Gesinnung.Neutral, 9) => "20",
            (Gesinnung.Neutral, 10) => "20",
            _ => ""

        };
    }
}
