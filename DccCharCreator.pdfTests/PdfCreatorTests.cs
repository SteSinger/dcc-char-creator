using Microsoft.VisualStudio.TestTools.UnitTesting;
using DccCharCreator.pdf;
using System;
using System.Collections.Generic;
using System.Text;
using DccCharCreator.core;
using DccCharCreator.core.CharacterData.Klasse;
using System.IO;
using System.Diagnostics;

namespace DccCharCreator.pdf.Tests
{
    [TestClass()]
    public class PdfCreatorTests
    {
        [TestMethod()]
        public void ErzeugeNullerBogenTest()
        {
            var random = new Random();
            var factory = new CharacterFactory(random);
            var characters = new[] { factory.Default(), factory.Default(), factory.Default(), factory.Default() };
            
            var p = new PdfCreator();
            p.ErzeugeNullerBogen(characters);
            Assert.Fail();
        }

        [TestMethod()]
        public void ErzeugeKriegerBogenTest()
        {
            var krieger = new Krieger(1, 1, 1, Gesinnung.Rechtschaffen);
            var p = new PdfCreator();
            using var kriegerPdf = p.ErzeugeKriegerBogen(krieger);

            using var f = File.Create("krieger_test.pdf");
            kriegerPdf.CopyTo(f);
            f.Flush();
            
        }


        [TestMethod()]
        public void ErzeugeDiebBogenTest()
        {
            var dieb = new Dieb(1, 1, 1, Gesinnung.Rechtschaffen);
            var p = new PdfCreator();
            using var diebPdf = p.ErzeugeDiebBogen(dieb);

            using var f = File.Create("dieb_test.pdf");
            diebPdf.CopyTo(f);
            f.Flush();
        }

    }
}