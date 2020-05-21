using Microsoft.VisualStudio.TestTools.UnitTesting;
using DccCharCreator.pdf;
using System;
using System.Collections.Generic;
using System.Text;
using DccCharCreator.core;

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
    }
}