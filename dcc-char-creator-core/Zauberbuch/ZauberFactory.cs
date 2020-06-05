using DccCharCreator.core.Würfel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DccCharCreator.core.Zauberbuch
{
    public class ZauberFactory
    {
        private readonly IW100 w100;
        private readonly I4W20 _4W20;
        private readonly IW3 w3;
        private readonly IW4 w4;
        private readonly IW6 w6;
        private readonly IW8 w8;
        private readonly IW10 w10;
        private readonly IW11 w11;

        public ZauberFactory(IW100 w100, I4W20 _4w20, IW4 w4, IW6 w6, IW8 w8, IW10 w10, IW3 w3, IW11 w11)
        {
            this.w100 = w100;
            this._4W20 = _4w20;
            this.w4 = w4;
            this.w6 = w6;
            this.w8 = w8;
            this.w10 = w10;
            this.w3 = w3;
            this.w11 = w11;
        }

        public IList<Zauber> ZauberkundigenZauberErstellen(int stufe, int glueckMod, int intelligenz, Random random)
        {
            if (stufe < 1 || stufe > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(stufe), "Muss zwischen 1 und 10 liegen.");
            }

            Zauber zauberErstellen(ZauberTemplate zt) => new Zauber(zt)
            {
                LaunenDerMagie = CreateLaunenDerMagie(glueckMod),
                Manifestation = GetManifestation(zt)
            };

            var anzahlZauber = AnzahlZauberkundigenZauber(stufe, intelligenz);
            var zg1 = ZauberTemplate.Get(Zaubertyp.Zauberkundigenzauber, 1);
            var zauberGrad1 = zg1.Select(zauberErstellen).Take(anzahlZauber.Stufe1).ToList();

            // Anrufung und Pakt zählen als ein Zauber
            var patron = zauberGrad1.Where(x => x.Wurf == 1 || x.Wurf == 14).Sum(x => x.Wurf);
            if (patron == 1)
            {
                zauberGrad1.Add(zg1.Where(x => x.Wurf == 14).Select(zauberErstellen).First());
            }
            else if (patron == 14)
            {
                zauberGrad1.Add(zg1.Where(x => x.Wurf == 1).Select(zauberErstellen).First());
            }
            else if (patron == 15)
            {
                zauberGrad1.Add(zg1.Skip(zauberGrad1.Count).Select(zauberErstellen).First());
            }
            else if (zauberGrad1.Any(x => x.Wurf == 27)) // Patronzauber ohne Patron. Ein neuer Zauber wird gewürfelt
            {
                var neuerZauber = zg1.Skip(zauberGrad1.Count).Select(zauberErstellen).First();
                if (neuerZauber.Wurf == 1)
                {
                    zauberGrad1.Add(zg1.Where(x => x.Wurf == 14).Select(zauberErstellen).First());
                }
                else if (neuerZauber.Wurf == 14)
                {
                    zauberGrad1.Add(zg1.Where(x => x.Wurf == 1).Select(zauberErstellen).First());
                }
                zauberGrad1.Add(neuerZauber);

                zauberGrad1 = zauberGrad1.Where(x => x.Wurf != 27).ToList();
            }


            var grad2bis5 = Enumerable.Range(2, 4).SelectMany(x => Shuffle(ZauberTemplate.Get(Zaubertyp.Zauberkundigenzauber, x), random).Select(zauberErstellen).Take(anzahlZauber[x])).ToList();

            return zauberGrad1.Concat(grad2bis5).ToList();
        }

        public IList<Zauber> ElfenZauberErstellen(int stufe, int intelligenz, Random random)
        {
            Zauber zauberErstellen(ZauberTemplate zt) =>
                new Zauber(zt)
                {
                    LaunenDerMagie = CreateLaunenDerMagie(0), // Glück wird erstmal nicht angerechnet bei Elfen. Das Regelwerk ist da nicht ganz eindeutig.
                    Manifestation = GetManifestation(zt)
                };


            if (stufe < 1 || stufe > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(stufe), "Muss zwischen 1 und 10 liegen.");
            }

            var anzahlZauber = AnzahlElfZauber(stufe, intelligenz);
            var zg1 = ZauberTemplate.Get(Zaubertyp.Zauberkundigenzauber, 1);
            var patronZauber = zg1.Where(x => x.Wurf == 1 || x.Wurf == 14).Select(zauberErstellen);
            var zauberGrad1 = zg1.Where(x => x.Wurf != 1 && x.Wurf != 14).Select(zauberErstellen).Take(anzahlZauber.Stufe1);
            var grad2bis5 = Enumerable.Range(2, 4).SelectMany(x => Shuffle(ZauberTemplate.Get(Zaubertyp.Zauberkundigenzauber, x), random).Select(zauberErstellen).Take(anzahlZauber[x])).ToList();

            return patronZauber.Concat(zauberGrad1).Concat(grad2bis5).ToList();
        }

        public IList<Zauber> KlerikerZauberErstellen(int stufe, int glueck, bool launenDerMagie, Random random)
        {
            if (stufe < 1 || stufe > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(stufe), "Muss zwischen 1 und 10 liegen.");
            }

            Func<ZauberTemplate, Zauber> zauberErstellen = x =>
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
            };

            var anzahlZauber = AnzahlKlerikerZauber(stufe);
            return Enumerable.Range(1, 5).SelectMany(x => Shuffle(ZauberTemplate.Get(Zaubertyp.Klerikerzauber, x), random).Select(zauberErstellen).Take(anzahlZauber[x])).ToList();
        }

        private ZauberProStufe AnzahlElfZauber(int stufe, int intelligenz)
        {
            var (zauberProStufe, maxZaubergrad) = stufe switch
            {
                1 => (new ZauberProStufe(3, 0, 0, 0, 0), 1),
                2 => (new ZauberProStufe(4, 0, 0, 0, 0), 1),
                3 => (new ZauberProStufe(4, 1, 0, 0, 0), 2),
                4 => (new ZauberProStufe(4, 2, 0, 0, 0), 2),
                5 => (new ZauberProStufe(4, 2, 1, 0, 0), 3),
                6 => (new ZauberProStufe(4, 2, 2, 0, 0), 3),
                7 => (new ZauberProStufe(4, 2, 2, 1, 0), 4),
                8 => (new ZauberProStufe(4, 2, 2, 2, 0), 4),
                9 => (new ZauberProStufe(4, 2, 2, 2, 2), 5),
                10 => (new ZauberProStufe(4, 2, 2, 2, 4), 5),
                _ => (new ZauberProStufe(0, 0, 0, 0, 0), 0)
            };
            return ZauberIntelligenz(intelligenz, zauberProStufe, maxZaubergrad);
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

        private ZauberProStufe AnzahlZauberkundigenZauber(int stufe, int intelligenz)
        {
            var (zauberProStufe, maxZaubergrad) = stufe switch
            {
                1 => (new ZauberProStufe(4, 0, 0, 0, 0), 1),
                2 => (new ZauberProStufe(5, 0, 0, 0, 0), 1),
                3 => (new ZauberProStufe(5, 1, 0, 0, 0), 2),
                4 => (new ZauberProStufe(5, 2, 0, 0, 0), 2),
                5 => (new ZauberProStufe(5, 2, 1, 0, 0), 3),
                6 => (new ZauberProStufe(5, 2, 2, 0, 0), 3),
                7 => (new ZauberProStufe(5, 2, 2, 1, 0), 4),
                8 => (new ZauberProStufe(5, 2, 2, 3, 0), 4),
                9 => (new ZauberProStufe(5, 2, 2, 3, 2), 5),
                10 => (new ZauberProStufe(5, 2, 2, 3, 4), 5),
                _ => (new ZauberProStufe(0, 0, 0, 0, 0), 0)
            };
            return ZauberIntelligenz(intelligenz, zauberProStufe, maxZaubergrad);
        }

        private ZauberProStufe ZauberIntelligenz(int intelligenz, ZauberProStufe zauberProStufe, int maxZaubergrad)
        {
            switch (intelligenz)
            {
                case 4:
                case 5:
                    return CalculateZauberProStufe(zauberProStufe, -2, 1);
                case 6:
                case 7:
                    return CalculateZauberProStufe(zauberProStufe, -1, 1);
                case 8:
                case 9:
                    return CalculateZauberProStufe(zauberProStufe, 0, Math.Min(2, maxZaubergrad));
                case 10:
                case 11:
                    return CalculateZauberProStufe(zauberProStufe, 0, Math.Min(3, maxZaubergrad));
                case 12:
                case 13:
                    return CalculateZauberProStufe(zauberProStufe, 0, Math.Min(4, maxZaubergrad));
                case 14:
                    return CalculateZauberProStufe(zauberProStufe, 1, Math.Min(4, maxZaubergrad));
                case 15:
                case 16:
                    return CalculateZauberProStufe(zauberProStufe, 1, Math.Min(5, maxZaubergrad));
                case 17:
                case 18:
                    return CalculateZauberProStufe(zauberProStufe, 2, Math.Min(5, maxZaubergrad));
                default:
                    return new ZauberProStufe(0, 0, 0, 0, 0);
            }
        }

        private ZauberProStufe CalculateZauberProStufe(ZauberProStufe zps, int modZauberMenge, int maxZauberGrad)
        {
            zps.Stufe1 += modZauberMenge;

            if (maxZauberGrad < 5)
            {
                zps.Stufe4 += zps.Stufe5;
                zps.Stufe5 = 0;
                // Nur Relevant für Zauberer auf Stufe 10. Es gibt nur 6 Grad 4 Zauber.
                if (zps.Stufe4 > 6)
                {
                    zps.Stufe3 += zps.Stufe4 - 6;
                    zps.Stufe4 = 6;
                }
            }
            if (maxZauberGrad < 4)
            {
                zps.Stufe3 += zps.Stufe4;
                zps.Stufe4 = 0;
            }
            if (maxZauberGrad < 3)
            {
                zps.Stufe2 += zps.Stufe3;
                zps.Stufe3 = 0;
            }
            if (maxZauberGrad < 2)
            {
                zps.Stufe1 += zps.Stufe2;
                zps.Stufe2 = 0;
            }

            return zps;
        }

        private IList<LauneDerMagie> CreateLaunenDerMagie(int glueckMod)
        {
            var wurf = w100.Würfeln() + glueckMod * 10;
            wurf = Math.Clamp(wurf, 1, 100);
            var launen = new List<LauneDerMagie>(3)
            {
                LauneDerMagie.Get(wurf)
            };
            if (wurf == 99)
            {
                launen.AddRange(CreateLaunenDerMagie(glueckMod));
                launen.AddRange(CreateLaunenDerMagie(glueckMod));
            }
            else if (wurf == 100)
            {
                launen.AddRange(CreateLaunenDerMagie100(glueckMod));
                launen.AddRange(CreateLaunenDerMagie100(glueckMod));
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

        public static List<T> Shuffle<T>(List<T> l, Random random)
        {
            var list = new List<T>(l);
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

            public int this[int stufe]
            {
                get => stufe switch { 1 => Stufe1, 2 => Stufe2, 3 => Stufe3, 4 => Stufe4, 5 => Stufe5, _ => throw new IndexOutOfRangeException() };
            }
        }
    }
}
