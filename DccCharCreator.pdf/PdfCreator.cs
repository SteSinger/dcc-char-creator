using DccCharCreator.core.CharacterData;
using DccCharCreator.core.CharacterData.Klasse;
using DccCharCreator.core.Zauberbuch;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.AcroForms;
using PdfSharpCore.Pdf.Advanced;
using PdfSharpCore.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace DccCharCreator.pdf
{
    public static class PdfCreator
    {
        public static MemoryStream ErzeugeZwergBogen(Zwerg zwerg)
        {
            var assembly = typeof(PdfCreator).Assembly;
            using var zwergBogen = assembly.GetManifestResourceStream("DccCharCreator.pdf.Resources.DCC_Zwerg_ausfüllbar.pdf");
            using var pdf = PdfReader.Open(zwergBogen);
            SetNeedAppearances(pdf);

            FillZwerg(zwerg, pdf.AcroForm);

            var stream = new MemoryStream();
            pdf.Save(stream, false);
            pdf.Dispose();
            return stream;
        }

        private static void FillZwerg(Zwerg zwerg, PdfAcroForm form)
        {
            var fields = form.Fields;
            FillBase(fields, zwerg);

            fields["Klasse"].Value = new PdfString("Zwerg");
            //Sternzeichen muss noch gesetzt werden.
        }

        public static MemoryStream ErzeugeHalblingBogen(Halbling halbling)
        {
            var assembly = typeof(PdfCreator).Assembly;
            using var halblingBogen = assembly.GetManifestResourceStream("DccCharCreator.pdf.Resources.DCC_Halbling_ausfüllbar.pdf");
            using var pdf = PdfReader.Open(halblingBogen);
            SetNeedAppearances(pdf);

            FillHalbling(halbling, pdf.AcroForm);

            var stream = new MemoryStream();
            pdf.Save(stream, false);
            pdf.Dispose();
            return stream;
        }

        private static void FillHalbling(Halbling halbling, PdfAcroForm form)
        {
            var fields = form.Fields;
            FillBase(fields, halbling);

            fields["Klasse"].Value = new PdfString("Halbling");
            fields["Schleichen"].Value = new PdfString(halbling.schleichenVerstecken.ToString());
            
        }

        public static MemoryStream ErzeugeElfBogen(Elf elf)
        {
            var assembly = typeof(PdfCreator).Assembly;
            using var elfBogen = assembly.GetManifestResourceStream("DccCharCreator.pdf.Resources.DCC_Elf_ausfüllbar.pdf");
            using var pdf = PdfReader.Open(elfBogen);
            SetNeedAppearances(pdf);

            FillElf(elf, pdf.AcroForm);

            var stream = new MemoryStream();
            pdf.Save(stream, false);
            pdf.Dispose();
            return stream;
        }

        private static void FillElf(Elf elf, PdfAcroForm form)
        {
            var fields = form.Fields;
            FillBase(fields, elf);

            fields["Klasse"].Value = new PdfString("Elf");
            fields["BasisZauberwurf"].Value = new PdfString(elf.Zauberstufe.ToString());
            //Sternzeichen muss noch gesetzt werden.

            var i = 0;
            foreach (var zauber in elf.Zauberbuch)
            {
                i++;
                fields[$"Zauber{i}"].Value = new PdfString($"{zauber.Name} S.{zauber.Seite}\n{zauber.Manifestation.Beschreibung}");
                if (i == 8) break;
            }
        }

        public static MemoryStream ErzeugeZauberkundigerBogen(Zauberkundiger zauberkundiger)
        {
            var assembly = typeof(PdfCreator).Assembly;
            using var zauberkundigerBogen = assembly.GetManifestResourceStream("DccCharCreator.pdf.Resources.DCC_Zauberer_ausfüllbar.pdf");
            using var pdf = PdfReader.Open(zauberkundigerBogen);
            SetNeedAppearances(pdf);

            FillZauberkundiger(zauberkundiger, pdf.AcroForm);
            
            var stream = new MemoryStream();
            pdf.Save(stream, false);
            pdf.Dispose();
            return stream;
        }

        private static void FillZauberkundiger(Zauberkundiger zauberkundiger, PdfAcroForm form)
        {
            var fields = form.Fields;
            FillBase(fields, zauberkundiger);

            fields["Klasse"].Value = new PdfString("Zauberkundiger");
            fields["BasisZauberwurf"].Value = new PdfString(zauberkundiger.Zauberstufe.ToString());
            //Sternzeichen muss noch gesetzt werden.

            var i = 0;
            foreach (var zauber in zauberkundiger.Zauberbuch)
            {
                
                if (i < 8)
                {
                    i++;
                    fields[$"Zauber{i}"].Value = new PdfString($"{zauber.Name}"); 
                }

                fields[$"Name{i}"].Value = new PdfString($"{zauber.Name}");
                fields[$"Seite{i}"].Value = new PdfString($"{zauber.Seite}");
                fields[$"LaunenDerMagie{i}"].Value = new PdfString($"{string.Join(", ", zauber.LaunenDerMagie)}");
                fields[$"Manifestation{i}"].Value = new PdfString($"{zauber.Manifestation.Beschreibung}");
                fields[$"Beschreibung{i}"].Value = new PdfString($"{zauber.Beschreibung}");
            }
        }

        public static MemoryStream ErzeugeKlerikerBogen(Kleriker kleriker)
        {
            var assembly = typeof(PdfCreator).Assembly;
            using var klerikerBogen = assembly.GetManifestResourceStream("DccCharCreator.pdf.Resources.DCC_Kleriker_ausfüllbar.pdf");
            using var pdf = PdfReader.Open(klerikerBogen);
            SetNeedAppearances(pdf);

            FillKleriker(kleriker, pdf.AcroForm);

            var stream = new MemoryStream();
            pdf.Save(stream, false);
            pdf.Dispose();
            return stream;
        }

        private static void FillKleriker(Kleriker kleriker, PdfAcroForm form)
        {
            var fields = form.Fields;
            FillBase(fields, kleriker);

            fields["Klasse"].Value = new PdfString("Kleriker");
            fields["Zauberwurf"].Value = new PdfString(kleriker.Zauberstufe.ToString());
            //Sternzeichen muss noch gesetzt werden.

            var i = 0;
            foreach (var zauber in kleriker.Zauberbuch)
            {
                i++;
                fields[$"Zauber{i}"].Value = new PdfString($"{zauber.Name} S.{zauber.Seite}\n{zauber.Manifestation.Beschreibung}");
            }
        }

        public static MemoryStream ErzeugeDiebBogen(Dieb dieb)
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

        private static void FillDieb(Dieb dieb, PdfAcroForm form)
        {
            var fields = form.Fields;
            FillBase(fields, dieb);

            fields["Klasse"].Value = new PdfString("Dieb");

            fields["DiebGlück"].Value = new PdfString(dieb.Glückswürfel);
            fields["DiebHinterhalt"].Value = new PdfString(dieb.HinterhältigerAngriff.ToString());
            fields["DiebSchleichen"].Value = new PdfString(dieb.LautlosSchleichen.ToString());
            fields["DiebSchatten"].Value = new PdfString(dieb.ImSchattenVerstecken.ToString());
            fields["DiebKnacken"].Value = new PdfString(dieb.SchlösserKnacken.ToString());
            fields["DiebKlettern"].Value = new PdfString(dieb.Fassadenklettern.ToString());
            fields["DiebDiebstahl"].Value = new PdfString(dieb.Taschendiebstahl.ToString());
            fields["DiebEntdecken"].Value = new PdfString(dieb.FallenEntdecken.ToString());
            fields["DiebEntschärfen"].Value = new PdfString(dieb.FallenEntschärfen.ToString());
            fields["DiebFälschen"].Value = new PdfString(dieb.UrkundeFälschen.ToString());
            fields["DiebVerkleiden"].Value = new PdfString(dieb.Verkleiden.ToString());
            fields["DiebSprachen"].Value = new PdfString(dieb.SprachenLesen.ToString());
            fields["DiebGift"].Value = new PdfString(dieb.GiftMischen.ToString());
            fields["DiebZaubern"].Value = new PdfString(dieb.ZauberVonEinerSchriftrolleWirken.ToString());


        }

        public static MemoryStream ErzeugeKriegerBogen(Krieger krieger)
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

        private static void FillKrieger(Krieger krieger, PdfAcroForm form)
        {
            var fields = form.Fields;
            FillBase(fields, krieger);

            fields["Klasse"].Value = new PdfString("Krieger");

            fields["KritBereichFäh"].Value = new PdfString(krieger.KritBereich);
            fields["KritWürfelFäh"].Value = new PdfString(krieger.KritWürfel);
            fields["PersWaffe"].Value = new PdfString();
        }

        private static void FillBase(PdfAcroField.PdfAcroFieldCollection fields, KlasseBase k)
        {
            fields["Titel"].Value = new PdfString(k.Titel);
            fields["Beruf"].Value = new PdfString(k.Beruf.Name);
            fields["Bewegung"].Value = new PdfString(k.Bewegung);
            fields["Stufe"].Value = new PdfString(k.Stufe.ToString());
            fields["Erfahrung"].Value = new PdfString(k.Erfahrungspunkte);
            fields["KritWürfel"].Value = new PdfString(k.KritWürfel);
            fields["NahSchaden"].Value = new PdfString(k.Attribute.Stärke.Modifikator.ToString());
            fields["RK"].Value = new PdfString(k.Rüstungsklasse.ToString());
            fields["TP"].Value = new PdfString(k.Trefferpunkte.ToString());
            fields["Max"].Value = new PdfString(k.Trefferpunkte.ToString());
            fields["Ini"].Value = new PdfString(k.Initiative.ToString());
            fields["Aktionswürfel"].Value = new PdfString(k.Aktionswürfel);
            fields["Angriff"].Value = new PdfString(k.Angriffsmodifikator);
            fields["KritTabelle"].Value = new PdfString(k.KritTabelle);
            fields["ModStä"].Value = new PdfString(k.Attribute.Stärke.Modifikator.ToString());
            fields["Stä"].Value = new PdfString(k.Attribute.Stärke.Value.ToString());
            fields["ModGes"].Value = new PdfString(k.Attribute.Geschicklichkeit.Modifikator.ToString());
            fields["Ges"].Value = new PdfString(k.Attribute.Geschicklichkeit.Value.ToString());
            fields["Aus"].Value = new PdfString(k.Attribute.Ausdauer.Value.ToString());
            fields["ModAus"].Value = new PdfString(k.Attribute.Ausdauer.Modifikator.ToString());
            fields["Per"].Value = new PdfString(k.Attribute.Persönlichkeit.Value.ToString());
            fields["ModPer"].Value = new PdfString(k.Attribute.Persönlichkeit.Modifikator.ToString());
            fields["Glü"].Value = new PdfString(k.Attribute.Glück.Value.ToString());
            fields["ModGlü"].Value = new PdfString(k.Attribute.Glück.Modifikator.ToString());
            fields["Int"].Value = new PdfString(k.Attribute.Intelligenz.Value.ToString());
            fields["ModInt"].Value = new PdfString(k.Attribute.Intelligenz.Modifikator.ToString());
            fields["NahAngriff"].Value = new PdfString(k.Attribute.Stärke.Modifikator.ToString());
            fields["FernSchaden"].Value = new PdfString("");
            fields["FernAngriff"].Value = new PdfString(k.Attribute.Geschicklichkeit.Modifikator.ToString());
            fields["RefRW"].Value = new PdfString(k.Reflexe.ToString());
            fields["ZähRW"].Value = new PdfString(k.Zähigkeit.ToString());
            fields["WillRW"].Value = new PdfString(k.Willenskraft.ToString());
            fields["Glückswurf"].Value = new PdfString(k.Attribute.Glück.Modifikator.ToString());
            fields["Sprachen"].Value = new PdfString(string.Join('\n', k.Sprachen));
            fields["Gesinnung"].Value = new PdfString(k.Gesinnung.ToString());
            fields["Waffen"].Value = new PdfString($"{k.Beruf.Startwaffe} ({k.Beruf.Schaden})");
            fields["Ausrüstung"].Value = new PdfString($"{k.Beruf.Handelsware}\n{k.Ausrüstung.Gegenstand} ({k.Ausrüstung.Preis})");
            fields["Schätze"].Value = new PdfString(k.Startkapital);
            if (fields["Notizen"] != null)
            {
                fields["Notizen"].Value = new PdfString($"{k.Geburtszeichen.Name}: {k.Geburtszeichen.Schicksalswurf} ({k.Geburtszeichen.Bonus})");
            }
        }

        public static MemoryStream ErzeugeNullerBogen(Character[] c)
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

        private static void DrawNullCharacter(Character c, PdfAcroForm form, int character)
        {
            var fields = form.Fields;
            fields[$"Beruf{character}"].Value = new PdfString(c.Beruf.Name);
            fields[$"Stufe{character}"].Value = new PdfString("0");
            fields[$"Ep{character}"].Value = new PdfString("0");
            fields[$"St{character}"].Value = new PdfString(c.Attribute.Stärke.Value.ToString());
            fields[$"StMod{character}"].Value = new PdfString(c.Attribute.Stärke.Modifikator.ToString());
            fields[$"Ge{character}"].Value = new PdfString(c.Attribute.Geschicklichkeit.Value.ToString());
            fields[$"GeMod{character}"].Value = new PdfString(c.Attribute.Geschicklichkeit.Modifikator.ToString());
            fields[$"Au{character}"].Value = new PdfString(c.Attribute.Ausdauer.Value.ToString());
            fields[$"AuMod{character}"].Value = new PdfString(c.Attribute.Ausdauer.Modifikator.ToString());
            fields[$"Pe{character}"].Value = new PdfString(c.Attribute.Persönlichkeit.Value.ToString());
            fields[$"PeMod{character}"].Value = new PdfString(c.Attribute.Persönlichkeit.Modifikator.ToString());
            fields[$"In{character}"].Value = new PdfString(c.Attribute.Intelligenz.Value.ToString());
            fields[$"InMod{character}"].Value = new PdfString(c.Attribute.Intelligenz.Modifikator.ToString());
            fields[$"Gl{character}"].Value = new PdfString(c.Attribute.Glück.Value.ToString());
            fields[$"GlMod{character}"].Value = new PdfString(c.Attribute.Glück.Modifikator.ToString());
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
