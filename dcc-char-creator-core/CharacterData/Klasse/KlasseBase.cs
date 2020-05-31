using DccCharCreator.core.Würfel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core.CharacterData.Klasse
{
    public abstract class KlasseBase
    {
        protected Character Character { get; }

        protected Random random;

        public int Trefferpunkte { get { return trefferpunkte + Character.Trefferpunkte; } }
        private int trefferpunkte = 0;

        public string Bewegung => Beruf.Rasse switch
        {
            Rasse.Halbling => "6",
            Rasse.Zwerg => "6",
            _ => "9",
        };

        public List<string> Sprachen { get; } = new List<string> { "Gemeinsprache" };

        public Attribute Attribute { get { return Character.Attribute; } }

        public Beruf Beruf { get { return Character.Beruf; } }

        public Geburtszeichen Geburtszeichen { get { return Character.Geburtszeichen; } }

        public Ausrüstung Ausrüstung { get { return Character.Ausrüstung; } }

        public virtual string Startkapital { get => $"{startkapital} {Character.Startkapital}"; protected set => startkapital = value; }

        public string Erfahrungspunkte => Stufe switch
        {
            1 => "10",
            2 => "50",
            3 => "110",
            4 => "190",
            5 => "290",
            6 => "410",
            7 => "550",
            8 => "710",
            9 => "890",
            _ => "1090"
        };

        public int Zähigkeit => Attribute.Ausdauer.Modifikator + ZähigkeitLookup(Stufe);

        public int Reflexe => Attribute.Geschicklichkeit.Modifikator + ReflexLookup(Stufe);

        public int Willenskraft => Attribute.Persönlichkeit.Modifikator + WillenskraftLookup(Stufe);

        public virtual int Initiative => Attribute.Geschicklichkeit.Modifikator;

        public virtual int Rüstungsklasse => 10 + Attribute.Geschicklichkeit.Modifikator;

        public string Titel => TitelLookup(Gesinnung, Stufe);

        protected abstract int ReflexLookup(int stufe);

        protected abstract int ZähigkeitLookup(int stufe);

        protected abstract int WillenskraftLookup(int stufe);

        protected abstract string TitelLookup(Gesinnung gesinnung, int stufe);

        public string Angriffsmodifikator => AngriffsmodifikatorLookup(Stufe);

        protected abstract string AngriffsmodifikatorLookup(int stufe);

        public int Stufe { get; }

        internal WürfelFunktionen wf;
        private string startkapital;

        public Gesinnung Gesinnung { get; }

        public string Aktionswürfel => AktionswürfelLookup(Stufe);

        public string KritWürfel => KritWürfelLookup(Stufe);

        protected abstract string KritWürfelLookup(int stufe);

        public string KritTabelle => KritTabelleLookup(Stufe);

        protected abstract string KritTabelleLookup(int stufe);

        protected abstract string AktionswürfelLookup(int stufe);

        public KlasseBase(int seed, int charakterNummer, int stufe, int trefferwürfel, Gesinnung gesinnung)
        {
            random = new Random(seed);
            var factory = new CharacterFactory(random);
            for (int i = 1; i <= charakterNummer; i++)
            {
                Character = factory.Default();
            }

            Stufe = Math.Clamp(stufe, 1, 10);

            wf = new WürfelFunktionen(random);
            trefferpunkte = wf.W(trefferwürfel, stufe);
            Gesinnung = gesinnung;

            var intMod = Character.Attribute.Intelligenz.Modifikator;
            for (int i = 1; i <= intMod; i++)
            {
                Sprachen.Add($"zusätzliche Sprache {i}");
            }
        }


    }
}
