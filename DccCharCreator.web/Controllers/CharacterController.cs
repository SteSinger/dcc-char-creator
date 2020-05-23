using System;
using DccCharCreator.core;
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

        public IActionResult Print(int? seed)
        {
            if (!seed.HasValue)
            {
                seed = Environment.TickCount;
            }

            var random = new Random(seed.Value);

            var factory = new CharacterFactory(random);
            var characters = new[] { factory.Default(), factory.Default(), factory.Default(), factory.Default() };
            var pdf = new PdfCreator();
            var stream = pdf.ErzeugeNullerBogen(characters);

            return File(stream, "application/pdf", $"Charakterbogen_Nullen_{seed}.pdf");
        }

    }
}