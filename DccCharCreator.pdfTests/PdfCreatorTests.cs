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

            using var nullPdf = PdfCreator.ErzeugeNullerBogen(characters);
            using var f = File.Create("null_test.pdf");
            nullPdf.CopyTo(f);
            f.Flush();
        }

        [TestMethod()]
        public void ErzeugeKriegerBogenTest()
        {
            var krieger = new Krieger(1, 1, 1, Gesinnung.Rechtschaffen);
            using var kriegerPdf = PdfCreator.ErzeugeKriegerBogen(krieger);

            using var f = File.Create("krieger_test.pdf");
            kriegerPdf.CopyTo(f);
            f.Flush();
            
        }


        [TestMethod()]
        public void ErzeugeDiebBogenTest()
        {
            var dieb = new Dieb(1, 1, 1, Gesinnung.Rechtschaffen);
            using var diebPdf = PdfCreator.ErzeugeDiebBogen(dieb);

            using var f = File.Create("dieb_test.pdf");
            diebPdf.CopyTo(f);
            f.Flush();
        }


        [TestMethod()]
        public void ErzeugeKlerikerBogenTest()
        {
            var kleriker = new Kleriker(1, 1, 1, Gesinnung.Rechtschaffen);
            using var klerikerPdf = PdfCreator.ErzeugeKlerikerBogen(kleriker);

            using var f = File.Create("kleriker_test.pdf");
            klerikerPdf.CopyTo(f);
            f.Flush();
        }

        [TestMethod()]
        public void ErzeugeZauberkundigerBogenTest()
        {
            var zauberkundiger = new Zauberkundiger(1, 1, 1, Gesinnung.Rechtschaffen);
            using var zauberkundigerPdf = PdfCreator.ErzeugeZauberkundigerBogen(zauberkundiger);

            using var f = File.Create("zauberer_test.pdf");
            zauberkundigerPdf.CopyTo(f);
            f.Flush();
        }

        [TestMethod()]
        public void ErzeugeElfBogenTest()
        {
            var elf = new Elf(1, 1, 1, Gesinnung.Rechtschaffen);
            using var elfPdf = PdfCreator.ErzeugeElfBogen(elf);

            using var f = File.Create("elf_test.pdf");
            elfPdf.CopyTo(f);
            f.Flush();
        }
        
        [TestMethod()]
        public void ErzeugeHalblingBogenTest()
        {
            var halbling = new Halbling(1, 3, 3, Gesinnung.Rechtschaffen);
            using var halblingPdf = PdfCreator.ErzeugeHalblingBogen(halbling);

            using var f = File.Create("halbling_test.pdf");
            halblingPdf.CopyTo(f);
            f.Flush();
        }

        [TestMethod()]
        public void ErzeugeZwergBogenTest()
        {
            var zwerg = new Zwerg(1, 1, 1, Gesinnung.Rechtschaffen);
            using var zwergPdf = PdfCreator.ErzeugeZwergBogen(zwerg);

            using var f = File.Create("zweg_test.pdf");
            zwergPdf.CopyTo(f);
            f.Flush();
        }
    }
}