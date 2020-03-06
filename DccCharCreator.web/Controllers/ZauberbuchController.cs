using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Generate(int anzahlZauber, int glueck, Klasse klasse)
        {
            if(glueck < -3 || glueck > 3)
            {
                return RedirectToAction(nameof(Index));
            }

            var wf = new WürfelFactory(new Random());
            var zauberFactory = new ZauberFactory(wf.W100, wf.W27, wf._4W20);

            var zauberbuchVM = new ZauberbuchViewModel
            {
                Glueck = glueck,
                AnzahlZauber = anzahlZauber,
                
            };

            zauberbuchVM.Zauberbuch = klasse switch
            {
                Klasse.Zauberer => zauberFactory.ZauberkundigenZauberErstellen(anzahlZauber, glueck),
                Klasse.Kleriker => zauberFactory.KlerikerZauberErstellen(anzahlZauber, glueck),
                Klasse.Elf => zauberFactory.ElfenZauberErstellen(anzahlZauber, glueck),
                _ => throw new ArgumentOutOfRangeException(nameof(klasse))
            };

            return View(zauberbuchVM);
        }
    }
}