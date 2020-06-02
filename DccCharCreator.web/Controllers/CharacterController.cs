using System;
using System.IO;
using DccCharCreator.core;
using DccCharCreator.core.CharacterData.Klasse;
using DccCharCreator.pdf;
using DccCharCreator.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DccCharCreator.web
{
    public class CharacterController : Controller
    {
        public IActionResult Index()
        {
            var seed = Environment.TickCount;

            var random = new Random(seed);
            var factory = new CharacterFactory(random);
            var c = new CharacterViewModel
            {
                Characters = new[] { factory.Default(), factory.Default(), factory.Default(), factory.Default() },
                Seed = seed
            };

            return View(c);
        }

        public IActionResult Gen()
        {
            var seed = Environment.TickCount;

            var random = new Random(seed);
            var factory = new CharacterFactory(random);
            var c = new CharacterViewModel
            {
                Characters = new[] { factory.Default(), factory.Default(), factory.Default(), factory.Default() },
                Seed = seed
            };

            return View("Gen", c);
        }

        public IActionResult Pdf(int seed, int character, Klasse klasse, int stufe, Gesinnung gesinnung)
        {
            MemoryStream ms = null;
            switch (klasse)
            {
                case Klasse.Dieb:
                    var dieb = new Dieb(seed, character, stufe, gesinnung);
                    ms = PdfCreator.ErzeugeDiebBogen(dieb);
                    break;
                case Klasse.Elf:
                    var elf = new Elf(seed, character, stufe, gesinnung);
                    ms = PdfCreator.ErzeugeElfBogen(elf);
                    break;
                case Klasse.Halbling:
                    var halbling = new Halbling(seed, character, stufe, gesinnung);
                    ms = PdfCreator.ErzeugeHalblingBogen(halbling);
                    break;
                case Klasse.Kleriker:
                    var kleriker = new Kleriker(seed, character, stufe, gesinnung);
                    ms = PdfCreator.ErzeugeKlerikerBogen(kleriker);
                    break;
                case Klasse.Krieger:
                    var krieger = new Krieger(seed, character, stufe, gesinnung);
                    ms = PdfCreator.ErzeugeKriegerBogen(krieger);
                    break;
                case Klasse.Zauberkundiger:
                    var zauberkundiger = new Zauberkundiger(seed, character, stufe, gesinnung);
                    ms = PdfCreator.ErzeugeZauberkundigerBogen(zauberkundiger);
                    break;
                case Klasse.Zwerg:
                    var zwerg = new Zwerg(seed, character, stufe, gesinnung);
                    ms = PdfCreator.ErzeugeZwergBogen(zwerg);
                    break;
            };

            return File(ms, "application/pdf", $"Charakterbogen_{klasse}_{seed}_{character}.pdf");
        }

        public IActionResult Print(int? seed)
        {
            if (!seed.HasValue)
            {
                seed = Environment.TickCount;
            }

            var random = new Random(seed.Value);

            var factory = new CharacterFactory(random);
            var characters = new[] { factory.Default(), factory.Default(), factory.Default(), factory.Default() };
            var stream = PdfCreator.ErzeugeNullerBogen(characters);

            return File(stream, "application/pdf", $"Charakterbogen_Nullen_{seed}.pdf");
        }

    }
}