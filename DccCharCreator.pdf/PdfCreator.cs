using DccCharCreator.core.CharacterData;
using DccCharCreator.core.CharacterData.Klasse;
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

        public MemoryStream ErzeugeDiebBogen(Dieb dieb)
        {
            var assembly = typeof(PdfCreator).Assembly;
            using var diebBogen = assembly.GetManifestResourceStream("DccCharCreator.pdf.Resources.DCC_Dieb_ausfüllbar.pdf");
            using var pdf = PdfReader.Open(diebBogen);
            SetNeedAppearances(pdf);

            FillDieb(dieb, pdf.AcroForm);

            var stream = new MemoryStream();
            pdf.Save(stream, false);
            pdf.Dispose();
            return stream;
        }

        private void FillDieb(Dieb dieb, PdfAcroForm form)
        {
            var fields = form.Fields;
            FillBase(fields, dieb);

            fields["Klasse"].Value = new PdfString("Dieb");

        }

        public MemoryStream ErzeugeKriegerBogen(Krieger krieger)
        {
            var assembly = typeof(PdfCreator).Assembly;
            using var kriegerBogen = assembly.GetManifestResourceStream("DccCharCreator.pdf.Resources.DCC_Krieger_ausfüllbar.pdf");
            using var pdf = PdfReader.Open(kriegerBogen);
            SetNeedAppearances(pdf);

            FillKrieger(krieger, pdf.AcroForm);

            var stream = new MemoryStream();
            pdf.Save(stream, false);
            pdf.Dispose();
            return stream;
        }

        private static void SetNeedAppearances(PdfDocument pdf)
        {
            if (pdf.AcroForm.Elements.ContainsKey("/NeedAppearances") == false)
            {
                pdf.AcroForm.Elements.Add("/NeedAppearances", new PdfBoolean(true));
            }
            else
            {
                pdf.AcroForm.Elements["/NeedAppearances"] = new PdfBoolean(true);
            }
        }

        private void FillKrieger(Krieger krieger, PdfAcroForm form)
        {
            var fields = form.Fields;
            FillBase(fields, krieger);
            
            fields["Klasse"].Value = new PdfString("Krieger");
            
            fields["NahSchaden"].Value = new PdfString(krieger.Attribute.Stärke.Bonus.ToString());
            fields["KritWürfel"].Value = new PdfString(krieger.KritWürfel);
            fields["KritBereichFäh"].Value = new PdfString(krieger.KritBereich);
            fields["KritWürfelFäh"].Value = new PdfString(krieger.KritWürfel);
            fields["PersWaffe"].Value = new PdfString();
            fields["Notizen"].Value = new PdfString($"{krieger.Geburtszeichen.Name}: {krieger.Geburtszeichen.Schicksalswurf} ({krieger.Geburtszeichen.Bonus})");
        }

        private void FillBase(PdfAcroField.PdfAcroFieldCollection fields, KlasseBase k)
        {
            fields["Titel"].Value = new PdfString(k.Titel);
            fields["Beruf"].Value = new PdfString(k.Beruf.Name);
            fields["Bewegung"].Value = new PdfString(k.Bewegung);
            fields["Stufe"].Value = new PdfString(k.Stufe.ToString());
            fields["Erfahrung"].Value = new PdfString(k.Erfahrungspunkte);
            fields["RK"].Value = new PdfString(k.Rüstungsklasse.ToString());
            fields["TP"].Value = new PdfString(k.Trefferpunkte.ToString());
            fields["Max"].Value = new PdfString(k.Trefferpunkte.ToString());
            fields["Ini"].Value = new PdfString(k.Initiative.ToString());
            fields["Aktionswürfel"].Value = new PdfString(k.Aktionswürfel);
            fields["Angriff"].Value = new PdfString(k.Angriffsmodifikator);
            fields["KritTabelle"].Value = new PdfString(k.KritTabelle);
            fields["ModStä"].Value = new PdfString(k.Attribute.Stärke.Bonus.ToString());
            fields["Stä"].Value = new PdfString(k.Attribute.Stärke.Value.ToString());
            fields["ModGes"].Value = new PdfString(k.Attribute.Geschicklichkeit.Bonus.ToString());
            fields["Ges"].Value = new PdfString(k.Attribute.Geschicklichkeit.Value.ToString());
            fields["Aus"].Value = new PdfString(k.Attribute.Ausdauer.Value.ToString());
            fields["ModAus"].Value = new PdfString(k.Attribute.Ausdauer.Bonus.ToString());
            fields["Per"].Value = new PdfString(k.Attribute.Persönlichkeit.Value.ToString());
            fields["ModPer"].Value = new PdfString(k.Attribute.Persönlichkeit.Bonus.ToString());
            fields["Glü"].Value = new PdfString(k.Attribute.Glück.Value.ToString());
            fields["ModGlü"].Value = new PdfString(k.Attribute.Glück.Bonus.ToString());
            fields["Int"].Value = new PdfString(k.Attribute.Intelligenz.Value.ToString());
            fields["ModInt"].Value = new PdfString(k.Attribute.Intelligenz.Bonus.ToString());
            fields["NahAngriff"].Value = new PdfString(k.Attribute.Stärke.Bonus.ToString());
            fields["FernSchaden"].Value = new PdfString("");
            fields["FernAngriff"].Value = new PdfString(k.Attribute.Geschicklichkeit.Bonus.ToString());
            fields["RefRW"].Value = new PdfString(k.Reflexe.ToString());
            fields["ZähRW"].Value = new PdfString(k.Zähigkeit.ToString());
            fields["WillRW"].Value = new PdfString(k.Willenskraft.ToString());
            fields["Glückswurf"].Value = new PdfString(k.Attribute.Glück.Bonus.ToString());
            fields["Sprachen"].Value = new PdfString(string.Join('\n', k.Sprachen));
            fields["Gesinnung"].Value = new PdfString(k.Gesinnung.ToString());
            fields["Waffen"].Value = new PdfString($"{k.Beruf.Startwaffe} ({k.Beruf.Schaden})");
            fields["Ausrüstung"].Value = new PdfString($"{k.Beruf.Handelsware}\n{k.Ausrüstung.Gegenstand} ({k.Ausrüstung.Preis})");
            fields["Schätze"].Value = new PdfString(k.Startkapital);
        }

        public MemoryStream ErzeugeNullerBogen(Character[] c)
        {
            var assembly = typeof(PdfCreator).Assembly;
            using var nullerBogen = assembly.GetManifestResourceStream("DccCharCreator.pdf.Resources.Null_4.pdf");
            using var pdf = PdfReader.Open(nullerBogen);
            SetNeedAppearances(pdf);

            DrawNullCharacter(c[0], pdf.AcroForm, 1);
            DrawNullCharacter(c[1], pdf.AcroForm, 2);
            DrawNullCharacter(c[2], pdf.AcroForm, 3);
            DrawNullCharacter(c[3], pdf.AcroForm, 4);

            var stream = new MemoryStream();
            pdf.Save(stream, false);
            return stream;
        }

        private void DrawNullCharacter(Character c, PdfAcroForm form, int character)
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
