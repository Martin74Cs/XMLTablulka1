using AutoCAD;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using XMLTabulka1;
using XMLTabulka1.Trida;

namespace LibraryAplikace.Acad
{
    public static class Acad
    {
        /// <summary>
        /// všechna prace s autocadem. Pokud soubor na cesta nexistuje bude vytvořen nový dokument dle šablony z dat teZak
        /// </summary>
        public static void Prace(TeZak teZak, string Cesta)
        {
            AcadDocument document = null;
            if (string.IsNullOrEmpty(Cesta))
            {
                document = VytvoritAcad(teZak.PATH);
                //vyplnění razítka
            }
            else
            {
                //soubor existuje a může být otevřen
                document = Program(Cesta);
            }
            //možná další práce se souborem dwg
            Razitko.Prenos(teZak);
            List<DataRazítka> datas = [];
            Razitko.VyberRazitkaAcad(document, datas);
        }

        /// <summary>
        /// Kontrola otevřeného souboru. Pokud soubor neexistuje bude vytvořen ze šablony.
        /// </summary>
        public static AcadDocument Program(string Cesta)
        {
            if (!Path.GetExtension(Cesta).Equals(".DWG", StringComparison.CurrentCultureIgnoreCase)) return null;
            //Otevři Acad = PripojAcad();
            var acad = OpenAcad();
            if (acad != null)
            {
                acad.Visible = true;
                //Hledaní otevřeno dokumentu
                AcadDocument dokument = acad.KontrolaOpenDokument(Cesta);
                if (dokument != null)
                    foreach (AcadDocument item in acad.Documents)
                    {
                        if (item.Name == Path.GetFileName(Cesta))
                        {
                            dokument = acad.Documents.Item(item.Name);
                            dokument.Activate();
                            break;
                        }
                    }
                else
                    dokument = acad.Documents.Open(Cesta);

                return dokument;

            }
            return null;
        }

        //public static AcadDocument Program(TeZak teZak)
        //{
        //    //Otevři Acad = PripojAcad();
        //    var acad = OpenAcad();
        //    if (acad != null)
        //    {
        //        acad.Visible = true;
        //        //hledaní otevřeno dokumentu
        //        AcadDocument dokument = acad.KontrolaOpenDokument(teZak.PATH);
        //        if (dokument != null)
        //        {
        //            //dokument.Open(teZak.PATH);
        //            dokument.Open(teZak.PATH);
        //            return dokument;
        //        }

        //        //ověření existence disků
        //        DirectoryInfo disk = new DirectoryInfo(teZak.PATH).Root;
        //        if (!Directory.Exists(disk.Name))
        //            teZak.PATH = teZak.PATH.Replace(disk.Name, "C:\\");

        //        //hledání všech souborů které odpovídají názvu výkresu dle poslední 6 znaků.
        //        List<string> Soubor = new Soubor().HledejZdaExistujeSoubor(teZak.PATH);
        //        if (Soubor.Count > 0)
        //            //muže být nalezeno více souborů
        //            // v seznamu jsou všechny typy souboru dwg, pdf, atd.
        //            dokument = acad.Documents.Open(Soubor.First());
        //        else
        //        {
        //            dokument = VytvoritAcad(teZak.PATH);
        //        }
        //        return dokument;
        //    }
        //    return null;
        //}

        /// <summary>
        /// Vytvoří adresař a soubor dwg dle šablony.
        /// </summary>
        public static AcadDocument VytvoritAcad(string Cesta)
        {
            if (!Directory.Exists(Path.GetDirectoryName(Cesta)))
                Directory.CreateDirectory(Path.GetDirectoryName(Cesta) ?? "");
            if (!File.Exists(Cesta))
                File.Copy(Cesty.SablonaDwg, Cesta);
            var acad = OpenAcad();
            return acad.Documents.Open(Cesta);
        }

        /// <summary>
        /// Hledej soubor dwg pokud existuje otevři ho.
        /// </summary>
        public static List<string> Program(DataRow CelyRadek)
        {
            //var acad =  PripojAcad();
            var acad = OpenAcad();
            if (acad != null)
            {
                acad.Visible = true;
                List<string> Soubor = new SouborApp().HledejZdaExistujeSoubor(CelyRadek[Sloupec.PATH].ToString() ?? "");
                if (Soubor.Count > 0)
                    acad.Documents.Open(Soubor.First() ?? "");
                return Soubor;
            }
            return null;
        }

        ///<summary>
        /// Otevří aplikaci Autocad
        /// </summary>
        public static AcadApplication OpenAcad()
        {
            try
            {
                return (AcadApplication)Marshal2.GetActiveObject("Autocad.Application");
            }
            catch (Exception)
            {
                return new AcadApplication();
                //throw;
            }
        }

        /// <summary>
        /// Kontrola otevřeného dokumentu.
        /// </summary>
        public static AcadDocument KontrolaOpenDokument(this AcadApplication Acapp, string Cesta)
        {
            AcadDocument doc = null;
            //AcadDocument doc = new(); otevíra novy dokuemnt
            try
            {
                foreach (AcadDocument item in Acapp.Documents)
                {
                    if (Path.GetFileName(item.Name) == Path.GetFileName(Cesta))
                    {
                        //Acapp.Documents.Open(Cesta);
                        doc = item;
                        break;
                    }
                }
            }
            catch (Exception)
            { }
            return doc;
        }

        /// <summary>
        /// Pripojení Aplikace Autocad
        /// </summary>
        public static AcadApplication PripojAcad()
        {
            //AutoCAD.AcadApplication app = null;
            AcadApplication app = new();
            try
            {
                Process[] processlist = Process.GetProcesses();
                foreach (var item in processlist)
                {
                    //Console.WriteLine("item.ProcessName " + item.ProcessName);
                    Console.WriteLine("item.MainWindowTitle \t\t" + item.MainWindowTitle.ToString());
                    if (item.MainWindowTitle.Length < 20) continue;
                    Console.WriteLine("item.MainWindowTitle \t\t" + item.MainWindowTitle[..16].ToString());
                    if (item.MainWindowTitle[..16] == "Autodesk AutoCAD")
                    {
                        Console.WriteLine("Funguje bylo autodesk autocad 2019");

                        //https://stackoverflow.com/questions/58010510/no-definition-found-for-getactiveobject-from-system-runtime-interopservices-mars
                        string filename = @"C:\Users\Martin\Documents\rr.dwg";
                        var ttt = Marshal.BindToMoniker(filename);

                        AcadDocument ddd = (AcadDocument)ttt;

                        var sss = ddd.Name;
                        break;
                    }
                }
                if (app == null)
                {
                    return new AcadApplication();
                }
                return app;
            }
            catch (Exception)
            {
                app = new AcadApplication();
                return app;
            }

        }

        /// <summary>
        /// Otevre výkres acadu
        /// </summary>
        public static AcadDocument OtevriAcad(string cesta)
        {
            if (File.Exists(cesta))
                return PripojAcad().Documents.Open(cesta);
            return new();
        }


    }
}