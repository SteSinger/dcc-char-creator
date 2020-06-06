using DccCharCreator.core.CharacterData;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.AcroForms;
using PdfSharpCore.Pdf.IO;
using System;
using System.IO;

namespace DccCharCreator.pdf
{
    public class PdfCreator
    {
        public PdfCreator()
        {

        }


        public MemoryStream ErzeugeNullerBogen(Character[] c)
        {
            var assembly = typeof(PdfCreator).Assembly;
            using var nullerBogen = assembly.GetManifestResourceStream("DccCharCreator.pdf.Resources.Null_4.pdf");
            using var pdf = PdfReader.Open(nullerBogen);
            if (pdf.AcroForm.Elements.ContainsKey("/NeedAppearances") == false)
            {
                pdf.AcroForm.Elements.Add("/NeedAppearances", new PdfBoolean(true));
            }
            else
            {
                pdf.AcroForm.Elements["/NeedAppearances"] = new PdfBoolean(true);
            }

            DrawCharacter(c[0], pdf.AcroForm, 1);
            DrawCharacter(c[1], pdf.AcroForm, 2);
            DrawCharacter(c[2], pdf.AcroForm, 3);
            DrawCharacter(c[3], pdf.AcroForm, 4);

            var stream = new MemoryStream();
            pdf.Save(stream, false);
            
            return stream;
        }

        private void DrawCharacter(Character c, PdfAcroForm form, int character)
        {
            var fields = form.Fields;
            fields[$"Beruf{character}"].Value = new PdfString(c.Beruf.Name);
            fields[$"Stufe{character}"].Value = new PdfString("0");
            fields[$"Ep{character}"].Value = new PdfString("0");
            fields[$"St{character}"].Value = new PdfString(c.Attribute.Stärke.Value.ToString());
            fields[$"StMod{character}"].Value = new PdfString(c.Attribute.Stärke.Bonus.ToString());
            fields[$"Ge{character}"].Value = new PdfString(c.Attribute.Geschicklichkeit.Value.ToString());
            fields[$"GeMod{character}"].Value = new PdfString(c.Attribute.Geschicklichkeit.Bonus.ToString());
            fields[$"Au{character}"].Value = new PdfString(c.Attribute.Ausdauer.Value.ToString());
            fields[$"AuMod{character}"].Value = new PdfString(c.Attribute.Ausdauer.Bonus.ToString());
            fields[$"Pe{character}"].Value = new PdfString(c.Attribute.Persönlichkeit.Value.ToString());
            fields[$"PeMod{character}"].Value = new PdfString(c.Attribute.Persönlichkeit.Bonus.ToString());
            fields[$"In{character}"].Value = new PdfString(c.Attribute.Intelligenz.Value.ToString());
            fields[$"InMod{character}"].Value = new PdfString(c.Attribute.Intelligenz.Bonus.ToString());
            fields[$"Gl{character}"].Value = new PdfString(c.Attribute.Glück.Value.ToString());
            fields[$"GlMod{character}"].Value = new PdfString(c.Attribute.Glück.Bonus.ToString());
            fields[$"R{character}"].Value = new PdfString(c.Reflexe);
            fields[$"Z{character}"].Value = new PdfString(c.Zähigkeit);
            fields[$"W{character}"].Value = new PdfString(c.Willenskraft);
            fields[$"TpMax{character}"].Value = new PdfString(c.Trefferpunkte.ToString());
            fields[$"TpCur{character}"].Value = new PdfString(c.Trefferpunkte.ToString());
            fields[$"Rk{character}"].Value = new PdfString(c.Rüstungsklasse.ToString());
            fields[$"KampfZ1_{character}"].Value = new PdfString($"{c.Beruf.Startwaffe} ({c.Beruf.Schaden})");
            fields[$"Notizen{character}"].Value = new PdfString(c.Beruf.Rassenvorteile());
            fields[$"Ausruestung{character}"].Value = new PdfString($"{c.Beruf.Handelsware}\n{c.Ausrüstung.Gegenstand} {c.Ausrüstung.Preis}\n{c.Startkapital}");
            fields[$"BesFaehigk{character}"].Value = new PdfString($"{c.Geburtszeichen.Name}: {c.Geburtszeichen.Schicksalswurf} ({c.Geburtszeichen.Bonus})");
        }
    }
}
