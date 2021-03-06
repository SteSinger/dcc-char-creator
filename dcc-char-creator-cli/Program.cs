﻿using DccCharCreator.core.CharacterData;
using DccCharCreator.core.Würfel;
using DccCharCreator.core.Zauberbuch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace dcc_char_creator_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var zauberListe = ZauberTemplate.ZauberDict.Value.SelectMany(x => x.Value.SelectMany(y => y.Value)).ToList();
            foreach (var z in zauberListe)
            {
                z.Beschreibung = string.Join(" ", z.Beschreibung.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));

                foreach (var m in z.Manifestationen)
                {
                    m.Beschreibung = string.Join(" ", m.Beschreibung.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));
                }
            }

            ZauberTemplate.Save(zauberListe);
            //var zt = new ZauberTemplate
            //{
            //    Beschreibung = "Beschreibung",
            //    Manifestationen = new List<Manifestation> { new Manifestation { Beschreibung = "Manifestation" } },
            //    Name = "Zauber",
            //    Seite = "13",
            //    Typ = Zaubertyp.Zauberkundigenzauber,
            //    Wurf = 12
            //};

            //ZauberTemplate.Save(new List<ZauberTemplate>() { zt });

            var wf = new WürfelFactory(new Random());
            var zauberFactory = new ZauberFactory(wf);

            zauberFactory.ZauberkundigenZauberErstellen(6, 1, 12, new Random());

            var würfel = new WürfelFactory(new Random());
            var berufWürfel = würfel.W100;
            var zeichenWürfel = würfel.W30;
            var attributWürfel = würfel._3W6;
            var trefferWürfel = würfel.W4;
            var handelsWarenWürfel = würfel.W24;
            var geldWürfel = würfel._5W12;

            do
            {
                var c = new Character(attributWürfel, berufWürfel, zeichenWürfel, trefferWürfel, geldWürfel, handelsWarenWürfel);
                Console.WriteLine(c);
            } while (Console.ReadKey().Key != ConsoleKey.Q);
        }
    }
}
