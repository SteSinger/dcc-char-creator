using DccCharCreator.core.Würfel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DccCharCreator.core.Zauberbuch
{
    public class ZauberFactory
    {
        private readonly IW100 w100;
        private readonly IW27 w27;
        private readonly I4W20 _4W20;

        public ZauberFactory(IW100 w100, IW27 w27, I4W20 _4w20)
        {
            this.w100 = w100;
            this.w27 = w27;
            this._4W20 = _4w20;
        }

        public IList<Zauber> ZauberkundigenZauberErstellen(int anzahl, int glueck)
        {
            if(anzahl < 1 || anzahl > 27)
            {
                throw new ArgumentOutOfRangeException(nameof(anzahl), "Muss zwischen 1 und 27 liegen.");
            }

            var zauber = new List<Zauber>(anzahl);
            var patronzauber = false;
            var patronAnrufenPakt = false;
            while(zauber.Count < anzahl)
            {
                var wurf = w27.Würfeln();
                while(zauber.Any(x => x.Wurf == wurf))
                {
                    wurf = w27.Würfeln();
                }

                if (wurf == 27) patronzauber = true;
                else if (wurf == 1 || wurf == 14)
                {
                    wurf = 14;
                    anzahl++;

                    patronAnrufenPakt = true;
                    var anrufungPatron = Zauber.Get(Zaubertyp.Zauberkundigenzauber, 1);
                    anrufungPatron.LaunenDerMagie = CreateLaunenDerMagie(glueck);
                    zauber.Add(anrufungPatron);
                }

                var z = Zauber.Get(Zaubertyp.Zauberkundigenzauber, wurf);
                z.LaunenDerMagie = CreateLaunenDerMagie(glueck);
                zauber.Add(z);

                if (zauber.Count == anzahl && patronzauber && !patronAnrufenPakt)
                {
                    zauber = zauber.Where(x => x.Wurf != 27).ToList();
                }
            }


            return zauber;
        }

        public IList<Zauber> ElfenZauberErstellen(int anzahl, int glueck)
        {
            if (anzahl < 1 || anzahl > 27)
            {
                throw new ArgumentOutOfRangeException(nameof(anzahl), "Muss zwischen 1 und 27 liegen.");
            }
            // Anrufung und Pakt zählen als ein Zauber
            anzahl++;

            var zauber = new List<Zauber>(anzahl);
            var anrufungPatron = Zauber.Get(Zaubertyp.Zauberkundigenzauber, 1);
            anrufungPatron.LaunenDerMagie = CreateLaunenDerMagie(glueck);
            zauber.Add(anrufungPatron);

            var paktPatron = Zauber.Get(Zaubertyp.Zauberkundigenzauber, 14);
            anrufungPatron.LaunenDerMagie = CreateLaunenDerMagie(glueck);
            zauber.Add(anrufungPatron);

            while (zauber.Count < anzahl)
            {
                var wurf = w27.Würfeln();
                while (zauber.Any(x => x.Wurf == wurf))
                {
                    wurf = w27.Würfeln();
                }

                var z = Zauber.Get(Zaubertyp.Zauberkundigenzauber, wurf);
                z.LaunenDerMagie = CreateLaunenDerMagie(glueck);
                zauber.Add(z);
            }


            return zauber;
        }

        public IList<Zauber> KlerikerZauberErstellen(int anzahlZauber, int glueck)
        {
            throw new NotImplementedException();
        }

        private IList<LauneDerMagie> CreateLaunenDerMagie(int glueck)
        {
            var wurf = w100.Würfeln() + glueck * 10;
            wurf = Math.Clamp(wurf, 1, 100);
            var launen = new List<LauneDerMagie>(3);
            launen.Add(LauneDerMagie.Get(wurf));
            if(wurf == 99)
            {
                launen.AddRange(CreateLaunenDerMagie(glueck));
                launen.AddRange(CreateLaunenDerMagie(glueck));
            } 
            else if(wurf == 100)
            {
                launen.AddRange(CreateLaunenDerMagie100(glueck));
                launen.AddRange(CreateLaunenDerMagie100(glueck));
            }
            return launen;
        }

        private IList<LauneDerMagie> CreateLaunenDerMagie100(int glueck)
        {
            var wurf = _4W20.Würfeln() + glueck * 10;
            wurf = Math.Clamp(wurf, 1, 100);
            var launen = new List<LauneDerMagie>(3);
            launen.Add(LauneDerMagie.Get(wurf));
            if (wurf == 99)
            {
                launen.AddRange(CreateLaunenDerMagie(glueck));
                launen.AddRange(CreateLaunenDerMagie(glueck));
            }
            else if (wurf == 100)
            {
                launen.AddRange(CreateLaunenDerMagie(glueck));
                launen.AddRange(CreateLaunenDerMagie(glueck));
            }
            return launen;
        }
    }
}
