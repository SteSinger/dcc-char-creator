using System;
using DccCharCreator.core.Würfel;
using DccCharCreator.core.Zauberbuch;
using DccCharCreator.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DccCharCreator.web.Controllers
{
    public class ZauberbuchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Generate(int stufe, int intelligenz, int glueck, Klasse klasse)
        {
            if(glueck < -3 || glueck > 3)
            {
                return RedirectToAction(nameof(Index));
            }

            Random random = new Random();
            var würfelFactory = new WürfelFactory(random);
            var zauberFactory = new ZauberFactory(würfelFactory);

            var zauberbuchVM = new ZauberbuchViewModel
            {
                Glueck = glueck,
                Intelligenz = intelligenz,
                Stufe = stufe,
                Klasse = klasse,
            };

            zauberbuchVM.Zauberbuch = klasse switch
            {
                Klasse.Zauberer => zauberFactory.ZauberkundigenZauberErstellen(stufe, glueck, intelligenz, random),
                Klasse.Kleriker => zauberFactory.KlerikerZauberErstellen(stufe, glueck, false, random),
                Klasse.KlerikerLaunen => zauberFactory.KlerikerZauberErstellen(stufe, glueck, true, random),
                Klasse.Elf => zauberFactory.ElfenZauberErstellen(stufe, intelligenz, random),
                _ => throw new ArgumentOutOfRangeException(nameof(klasse))
            };

            return View(zauberbuchVM);
        }
    }
}