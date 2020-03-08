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
        private readonly IW4 w4;
        private readonly IW6 w6;
        private readonly IW8 w8;
        private readonly IW10 w10;

        public ZauberFactory(IW100 w100, IW27 w27, I4W20 _4w20, IW4 w4, IW6 w6, IW8 w8, IW10 w10)
        {
            this.w100 = w100;
            this.w27 = w27;
            this._4W20 = _4w20;
            this.w4 = w4;
            this.w6 = w6;
            this.w8 = w8;
            this.w10 = w10;
        }

        public IList<Zauber> ZauberkundigenZauberErstellen(int anzahl, int glueck)
        {
            if (anzahl < 1 || anzahl > 26)
            {
                throw new ArgumentOutOfRangeException(nameof(anzahl), "Muss zwischen 1 und 26 liegen.");
            }

            var zauberBuch = new List<Zauber>(anzahl);
            var patronzauber = false;
            var patronAnrufenPakt = false;
            while (zauberBuch.Count < anzahl)
            {
                var wurf = w27.Würfeln();
                while (zauberBuch.Any(x => x.Wurf == wurf))
                {
                    wurf = w27.Würfeln();
                }
                ZauberTemplate zt;

                if (wurf == 27) patronzauber = true;
                else if (wurf == 1 || wurf == 14)
                {

                    anzahl++;

                    patronAnrufenPakt = true;
                    zt = ZauberTemplate.Get(Zaubertyp.Zauberkundigenzauber, wurf == 1 ? 14 : 1);
                    var patron = new Zauber(zt)
                    {
                        LaunenDerMagie = CreateLaunenDerMagie(glueck),
                        Manifestation = GetManifestation(zt)
                    };
                    zauberBuch.Add(patron);
                }
                
                zt = ZauberTemplate.Get(Zaubertyp.Zauberkundigenzauber, wurf);
                var zauber = new Zauber(zt)
                {
                    LaunenDerMagie = CreateLaunenDerMagie(glueck),
                    Manifestation = GetManifestation(zt)
                };
                zauberBuch.Add(zauber);

                if (zauberBuch.Count == anzahl && patronzauber && !patronAnrufenPakt)
                {
                    zauberBuch = zauberBuch.Where(x => x.Wurf != 27).ToList();
                }
            }
            return zauberBuch;
        }

        public IList<Zauber> ElfenZauberErstellen(int anzahl, int glueck)
        {
            if (anzahl < 1 || anzahl > 26)
            {
                throw new ArgumentOutOfRangeException(nameof(anzahl), "Muss zwischen 1 und 26 liegen.");
            }
            // Anrufung und Pakt zählen als ein Zauber
            anzahl++;

            var zauberBuch = new List<Zauber>(anzahl);
            var zt = ZauberTemplate.Get(Zaubertyp.Zauberkundigenzauber, 1);
            var anrufungPatron = new Zauber(zt)
            {
                LaunenDerMagie = CreateLaunenDerMagie(glueck),
                Manifestation = GetManifestation(zt)
            };
            zauberBuch.Add(anrufungPatron);
            zt = ZauberTemplate.Get(Zaubertyp.Zauberkundigenzauber, 14);
            var paktPatron = new Zauber(zt)
            {
                LaunenDerMagie = CreateLaunenDerMagie(glueck),
                Manifestation = GetManifestation(zt)
            };
            zauberBuch.Add(paktPatron);

            while (zauberBuch.Count < anzahl)
            {
                var wurf = w27.Würfeln();
                while (zauberBuch.Any(x => x.Wurf == wurf))
                {
                    wurf = w27.Würfeln();
                }

                zt = ZauberTemplate.Get(Zaubertyp.Zauberkundigenzauber, wurf);
                var zauber = new Zauber(zt)
                {
                    LaunenDerMagie = CreateLaunenDerMagie(glueck),
                    Manifestation = GetManifestation(zt)
                };
                zauberBuch.Add(zauber);
            }

            return zauberBuch;
        }

        public IList<Zauber> KlerikerZauberErstellen(int anzahlZauber, int glueck)
        {
            throw new NotImplementedException();
        }

        private IList<LauneDerMagie> CreateLaunenDerMagie(int glueck)
        {
            var wurf = w100.Würfeln() + glueck * 10;
            wurf = Math.Clamp(wurf, 1, 100);
            var launen = new List<LauneDerMagie>(3)
            {
                LauneDerMagie.Get(wurf)
            };
            if (wurf == 99)
            {
                launen.AddRange(CreateLaunenDerMagie(glueck));
                launen.AddRange(CreateLaunenDerMagie(glueck));
            }
            else if (wurf == 100)
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
            var launen = new List<LauneDerMagie>(3)
            {
                LauneDerMagie.Get(wurf)
            };
            if (wurf == 99)
            {
                launen.AddRange(CreateLaunenDerMagie(glueck));
                launen.AddRange(CreateLaunenDerMagie(glueck));
            }
            else if (wurf == 100)
            {
                launen.AddRange(CreateLaunenDerMagie100(glueck));
                launen.AddRange(CreateLaunenDerMagie100(glueck));
            }
            return launen;
        }

        private Manifestation GetManifestation(ZauberTemplate zauberTemplate)
        {
            var manifestationen = zauberTemplate.Manifestationen;

            var index = manifestationen.Count switch
            {
                1 => 1,
                4 => w4.Würfeln(),
                6 => w6.Würfeln(),
                8 => w8.Würfeln(),
                10 => w10.Würfeln(),
                _ => throw new ArgumentOutOfRangeException(nameof(manifestationen.Count))
            };
            return manifestationen[index - 1];
        }
    }
}
