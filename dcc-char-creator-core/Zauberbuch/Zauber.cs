using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core.Zauberbuch
{
    public class Zauber
    {
        public Zauber(ZauberTemplate zauberTemplate) : this(zauberTemplate.Wurf, zauberTemplate.Seite,zauberTemplate.Name, zauberTemplate.Beschreibung)
        {
        }

        public Zauber(int wurf, string seite, string name, string beschreibung)
        {
            Wurf = wurf;
            Seite = seite;
            Name = name;
            Beschreibung = beschreibung;
        }

        public int Wurf { get; }
        public string Seite { get; }
        public string Name { get; }
        public string Beschreibung { get; }
        public Manifestation Manifestation { get; set; }
        public IList<LauneDerMagie> LaunenDerMagie { get; set; }

        public int Grad { get; }
    }
}
