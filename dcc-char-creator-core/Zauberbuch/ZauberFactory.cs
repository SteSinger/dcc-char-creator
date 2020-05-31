using DccCharCreator.core.Würfel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;

namespace DccCharCreator.core.Zauberbuch
{
    public class ZauberFactory
    {
        private readonly IW100 w100;
        private readonly IW27 w27;
        private readonly I4W20 _4W20;
        private readonly IW3 w3;
        private readonly IW4 w4;
        private readonly IW6 w6;
        private readonly IW8 w8;
        private readonly IW10 w10;
        private readonly IW11 w11;

        public ZauberFactory(IW100 w100, IW27 w27, I4W20 _4w20, IW4 w4, IW6 w6, IW8 w8, IW10 w10, IW3 w3, IW11 w11)
        {
            this.w100 = w100;
            this.w27 = w27;
            this._4W20 = _4w20;
            this.w4 = w4;
            this.w6 = w6;
            this.w8 = w8;
            this.w10 = w10;
            this.w3 = w3;
            this.w11 = w11;
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

        public IList<Zauber> KlerikerZauberErstellen(int stufe, int glueck, bool launenDerMagie, Random random)
        {
            if (stufe < 1 || stufe > 11)
            {
                throw new ArgumentOutOfRangeException(nameof(stufe), "Muss zwischen 1 und 11 liegen.");
            }

            var anzahlZauber = AnzahlKlerikerZauber(stufe).Stufe1;
            
            var templates = ZauberTemplate.Get(Zaubertyp.Klerikerzauber).Values.ToList();
            return Shuffle(templates, random).Select(x =>
            {
                var zauber = new Zauber(x)
                {
                    Manifestation = GetManifestation(x)
                };
                if (launenDerMagie)
                {
                    zauber.LaunenDerMagie = CreateLaunenDerMagie(glueck);
                }
                else
                {
                    zauber.LaunenDerMagie = new List<LauneDerMagie>();
                }
                return zauber;
            }).Take(anzahlZauber).ToList();
        }

        private ZauberProStufe AnzahlKlerikerZauber(int stufe) => stufe switch
        {
            1 => new ZauberProStufe(4, 0, 0, 0, 0),
            2 => new ZauberProStufe(5, 0, 0, 0, 0),
            3 => new ZauberProStufe(5, 3, 0, 0, 0),
            4 => new ZauberProStufe(6, 4, 0, 0, 0),
            5 => new ZauberProStufe(6, 5, 2, 0, 0),
            6 => new ZauberProStufe(7, 5, 3, 0, 0),
            7 => new ZauberProStufe(7, 6, 4, 1, 0),
            8 => new ZauberProStufe(8, 6, 5, 2, 0),
            9 => new ZauberProStufe(8, 7, 5, 3, 1),
            10 => new ZauberProStufe(9, 7, 6, 4, 2),
            _ => new ZauberProStufe(0, 0, 0, 0, 0)
        };

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
                3 => w3.Würfeln(),
                4 => w4.Würfeln(),
                6 => w6.Würfeln(),
                8 => w8.Würfeln(),
                10 => w10.Würfeln(),
                11 => w11.Würfeln(),
                _ => throw new Exception(manifestationen.Count.ToString(CultureInfo.InvariantCulture))
            };
            return manifestationen[index - 1];
        }

    public List<T> Shuffle<T>( List<T> list, Random random)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

            return list;
    }

    private struct ZauberProStufe
        {
            public ZauberProStufe(int s1, int s2, int s3, int s4, int s5)
            {
                Stufe1 = s1;
                Stufe2 = s2;
                Stufe3 = s3;
                Stufe4 = s4;
                Stufe5 = s5;
            }
            public int Stufe1;
            public int Stufe2;
            public int Stufe3;
            public int Stufe4;
            public int Stufe5;
        }
    }
}
