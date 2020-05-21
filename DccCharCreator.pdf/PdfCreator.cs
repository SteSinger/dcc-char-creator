using DccCharCreator.core.CharacterData;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System;

namespace DccCharCreator.pdf
{
    public class PdfCreator
    {
        public PdfCreator()
        {

        }


        public void ErzeugeNullerBogen(Character[] c)
        {
            var assembly = typeof(PdfCreator).Assembly;
            var nullerBogen = assembly.GetManifestResourceStream("DccCharCreator.pdf.Resources.Null_4.pdf");
            var pdf = PdfReader.Open(nullerBogen);
            var page = pdf.Pages[0];
            
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("OpenSans", 10);
            DrawCharacter(c[0], gfx, font, 0, 0);
            DrawCharacter(c[1], gfx, font, 420, 0);
            DrawCharacter(c[2], gfx, font, 0, 296);
            DrawCharacter(c[3], gfx, font, 420, 296);
            pdf.Save("test.pdf");
        }

        private void DrawCharacter(Character c, XGraphics gfx, XFont font, int xOffset, int yOffset)
        {
            var am = new XStringFormat { Alignment = XStringAlignment.Center };
            var ar = new XStringFormat { Alignment = XStringAlignment.Far };

            var attribut = 108 + xOffset;
            var attributBonus = 135 + xOffset;
            var rettungswuerfe = 190 + xOffset;

            //Attribute
            gfx.DrawString(c.Attribute.Stärke.Value.ToString(), font, XBrushes.Black, attribut, 103 + yOffset, am);
            gfx.DrawString(c.Attribute.Stärke.BonusFormatted.ToString(), font, XBrushes.Black, attributBonus, 103 + yOffset, ar);
            gfx.DrawString(c.Refelexe.ToString(), font, XBrushes.Black, rettungswuerfe + 5, 95 + yOffset, am);

            gfx.DrawString(c.Attribute.Geschicklichkeit.Value.ToString(), font, XBrushes.Black, attribut, 120 + yOffset, am);
            gfx.DrawString(c.Attribute.Geschicklichkeit.BonusFormatted.ToString(), font, XBrushes.Black, attributBonus, 120 + yOffset, ar);

            gfx.DrawString(c.Attribute.Ausdauer.Value.ToString(), font, XBrushes.Black, attribut, 137 + yOffset, am);
            gfx.DrawString(c.Attribute.Ausdauer.BonusFormatted.ToString(), font, XBrushes.Black, attributBonus, 137 + yOffset, ar);
            gfx.DrawString(c.Zähigkeit.ToString(), font, XBrushes.Black, rettungswuerfe + 3, 139 + yOffset, am);

            gfx.DrawString(c.Attribute.Persönlichkeit.Value.ToString(), font, XBrushes.Black, attribut, 154 + yOffset, am);
            gfx.DrawString(c.Attribute.Persönlichkeit.BonusFormatted.ToString(), font, XBrushes.Black, attributBonus, 154 + yOffset, ar);

            gfx.DrawString(c.Attribute.Intelligenz.Value.ToString(), font, XBrushes.Black, attribut, 172 + yOffset, am);
            gfx.DrawString(c.Attribute.Intelligenz.BonusFormatted.ToString(), font, XBrushes.Black, attributBonus, 172 + yOffset, ar);
            gfx.DrawString(c.Willenskraft.ToString(), font, XBrushes.Black, rettungswuerfe, 166 + yOffset, am);

            gfx.DrawString(c.Attribute.Glück.Value.ToString(), font, XBrushes.Black, attribut, 191 + yOffset, am);
            gfx.DrawString(c.Attribute.Glück.BonusFormatted.ToString(), font, XBrushes.Black, attributBonus, 191 + yOffset, ar);

            //Beruf
            gfx.DrawString(c.Beruf.Name.ToString(), font, XBrushes.Black, 250 + xOffset, 65 + yOffset);

            //TP
            gfx.DrawString(c.Trefferpunkte.ToString(), font, XBrushes.Black, 237 + xOffset, 90 + yOffset, am);

            //RK
            gfx.DrawString(c.Rüstungsklasse.ToString(), font, XBrushes.Black, 250 + xOffset, 152 + yOffset, am);

            //Ausrüstung
            gfx.DrawString(c.Ausrüstung.Gegenstand.ToString(), font, XBrushes.Black, 308 + xOffset, 195 + yOffset);
            gfx.DrawString(c.Beruf.Handelsware.ToString(), font, XBrushes.Black, 308 + xOffset, 205 + yOffset);

            //Waffe
            gfx.DrawString($"{c.Beruf.Startwaffe} ({c.Beruf.Schaden})", font, XBrushes.Black, 305 + xOffset, 115 + yOffset);

            gfx.DrawString($"{c.Geburtszeichen.Name}: {c.Geburtszeichen.Schicksalswurf} ({c.Geburtszeichen.Bonus})", font, XBrushes.Black, 60 + xOffset, 270 + yOffset);
            gfx.DrawString($"{c.Beruf.Rassenvorteile()}", font, XBrushes.Black, 60 + xOffset, 280 + yOffset);

        }
    }
}
