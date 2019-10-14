using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DccCharCreator.core;
using Microsoft.AspNetCore.Mvc;

namespace DccCharCreator.web
{
    public class CharacterController : Controller
    {
        public IActionResult Index()
        {
            var c = new[] { CharacterFactory.Default(), CharacterFactory.Default(), CharacterFactory.Default(), CharacterFactory.Default() };

            return View(c);
        }
    }
}