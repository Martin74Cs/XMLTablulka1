using LibraryAplikace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace XMLTabulka1.Word
{
    public class WordPodpora
    {
        public DataRow CelyRadek { set; get; }
        Microsoft.Office.Interop.Word.Document Doc { set; get; } = null;
        public string UTAJENI { set; get; } = string.Empty;
        public string STUPEN { set; get; } = string.Empty;
        public string STATUS { set; get; } = string.Empty;
        public static string SouborExe => System.Reflection.Assembly.GetExecutingAssembly().Location;
        public static string AdresarSpusteni => Path.GetDirectoryName(SouborExe);
        public static string AktualniAdresar => Environment.CurrentDirectory;
        public static string Word => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\Word"));
        public static string CestaAdresar()
        {
            //cesta k projektu
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return System.IO.Path.GetDirectoryName(path);
        }

        /// <summary>
        /// Nový dokumet word vyplnění razítka
        /// </summary>
        /// <param name="cesta">cesta k dokumentu word kam budou přeneseny data z XML</param>
        /// /// <param name="CestaXML">Cesta k XML</param>
        public async Task<bool> NovyDoc(string cesta, string CestaXML)
        {
            //try
            //{
            if (cesta == null) throw new Exception("Soubor nexistuje");
            CelyRadek = Soubor.NactiXML(CestaXML);
            if (CelyRadek == null) throw new Exception("Soubor XML neexistuje");

            if (File.Exists(cesta))
            {
                if (Path.GetExtension(cesta).ToLower() == ".doc")
                {
                    Console.WriteLine("Španá připona je vyžadováno Docx");
                    Soubor.Pruzkumnik(cesta);
                    return false;
                }
                else 
                {
                    //soubor existuje Otevřín Word a soubor
                    var WordApp = WordApp1();
                    WordApp.Documents.Open(cesta);
                }
            }
            else
            {
                string Adresar = Path.GetDirectoryName(cesta);
                string Pripona = CelyRadek["EXT"].ToString();
                string JmenoSouboru = CelyRadek["FPC"].ToString();
                //Soubor neexistuje
                if (Pripona.ToLower() == "doc")
                    //ZMĚNA PŘÍPONY
                    cesta = Path.Combine(Adresar, JmenoSouboru + ".docx");
                if (SouborApp.KopieDoc(cesta) == false)
                {
                    //throw new Exception("Chyba kopírování šablony");
                    Console.WriteLine("kopie šablony uspěšně vytvořena");
                }
                Prenos(cesta);
            }
            return true;
            //}

            //catch (Exception)
            //{
            //    //throw;
            //}
            return false;
        }

        /// <summary>
        /// Přenos dat do Wordu
        /// </summary>
        /// <param name="SouborWord">Cesta dokumentu DOC kam se mají data přenést</param>
        /// <returns></returns>
        public bool Prenos(string SouborWord)
        {
            if (!File.Exists(SouborWord)) return false;

            Microsoft.Office.Interop.Word.Application WordApp;
            try
            {
                DataSet Hlas = new DataSet();

                string Hodnota = "";
                //doc odkazuje na aktivní dokument
                //Microsoft.Office.Interop.Word.Document Doc = new Microsoft.Office.Interop.Word.Document();

                //Bude otevřena aplikace WORD
                Console.WriteLine("Bude otevřena aplikace WORD");
                WordApp = WordApp1();  //Bude otevřen WORD
                WordApp.Visible = true; //WORD bude zobrazen

                if (System.IO.File.Exists(SouborWord))
                {
                    Console.WriteLine("Bude otevřena soubor Wordu");
                    Doc = WordApp.Documents.Open(SouborWord);        //Otevření souboru
                }
                else
                {
                    Console.WriteLine("Soubor NE-EXISTUJE");
                    return false;
                }

                string csv = Word + @"\word_data1.csv";
                Hlas = Soubor.NactiCSV(csv);
                if (Hlas == null) return false;

                Console.WriteLine("Probíhá přenos dat");
                foreach (DataRow i in Hlas.Tables[0].Rows)
                {
                    //sloupec 0 v csv nazev WORD
                    string SWord = i[0].ToString();

                    Hodnota = "";  //vymazání hodnoty
                    //sloupec 1 v csv nazev database
                    string SData = i[1].ToString();
                    //pok hodnota z datatable

                    //if (lok != "" || lok != "x")
                    if (SData != "")
                    {
                        switch (SWord.ToUpper())
                        {
                            case "CDOK":
                                Hodnota = RadekParam("PCC") + "-" + RadekParam("TD") + "-" + RadekParam("PCDOC");
                                break;

                            case "PROJEKT":
                                //C_PROJ
                                Hodnota = RadekParam(SData) + "." + RadekParam("C_UKOL");
                                /*if (RadekExistuje(SData))
                                    Hodnota = CelyRadek[SData].ToString() + "." + CelyRadek["C_UKOL"].ToString();*/
                                break;

                            case "ZAKAZNIK":
                                Hodnota = RadekParam("INVESTOR");
                                break;

                            case "NAZEV":
                                Hodnota = RadekParam("NAZ_PROJ");
                                break;

                            case "ZPRAC1":
                                string[] dfg = RadekParam(SData).Split(' ');
                                Hodnota = dfg[dfg.Length - 1].ToUpper(); //asi přijmení
                                break;

                            case "KONTROL1":
                                string[] dfg1 = RadekParam(SData).Split(' ');
                                Hodnota = dfg1[dfg1.Length - 1].ToUpper(); //asi přijmení
                                break;

                            case "SCHVALIL1":
                                //titul jmeno prijmeni
                                string[] dfg2 = RadekParam(SData).Split(' ');
                                Hodnota = dfg2[dfg2.Length - 1].ToUpper(); //asi přijmení
                                break;

                            case "CA4":
                                if (RadekExistuje("DIL"))
                                    Hodnota = RadekParam("DIL");
                                if (RadekExistuje("CAST"))
                                    Hodnota += "." + RadekParam("CAST");
                                if (RadekExistuje("PROFESE"))
                                    Hodnota += "-" + RadekParam("PROFESE");
                                if (RadekExistuje("PORADI"))
                                    Hodnota += "." + RadekParam("PORADI");
                                if (RadekExistuje("OR_CISLO"))
                                    Hodnota += "-" + RadekParam("OR_CISLO");
                                break;

                            /*If IsNothing(CelyRadek("CAST").ToString()) Then Else CelyRadek("CAST").ToString()
                            'pod = DIL + "." _
                            '    + CAST + "-" _
                            '    + PROFESE + "." _
                            '    + PORADI + "-" _
                            '    + OR_CISLO
                            */
                            case "REV":
                                Hodnota = RadekParam(SData);
                                break;

                            case "DAT1": // "DAT2": "DAT3": "DAT4":
                                DateTime datum;
                                if (RadekParam(SData) == "")
                                {
                                    Hodnota = RadekParam(SData);

                                    datum = DateTime.Now;
                                    Hodnota = datum.ToString("yyyy'-'MM");
                                }

                                else
                                {
                                    datum = Convert.ToDateTime(RadekParam(SData));
                                    Hodnota = datum.ToString("yyyy'-'MM");
                                    //string[] pole = RadekParam(SData).Split(' ');
                                    //string[] Prvni = RadekParam(pole[0]).Split('.');
                                    //Hodnota = Prvni[1].Trim() + "/"+ Prvni[2].Trim();
                                    //datum = (DateTime)RadekParam(SData);

                                    DateTime pomoc = DateTime.Now;
                                    if (datum.Year < 2018)
                                    {
                                        datum = new DateTime(pomoc.Year, datum.Month, datum.Day, datum.Hour, datum.Minute, datum.Second, datum.Millisecond);
                                        Hodnota = datum.ToString("yyyy'-'MM'-'dd");
                                    }
                                }
                                break;

                            case "":
                                //Hodnota = "Prazdno";
                                Hodnota = RadekParam(SData);
                                break;

                            default:
                                Hodnota = RadekParam(SData);
                                /*if (RadekExistuje(SData) == true)
                                    Hodnota = CelyRadek[SData].ToString();
                                else
                                    Hodnota = "Default";*/
                                break;
                        }
                    }

                    //if (Hodnota == "") 
                    //    Hodnota = "Nenalezeno";

                    if (SWord != "")
                    {
                        try
                        { UpdateForm(SWord, Hodnota.ToUpper()); }

                        catch (Exception)
                        { UpdateBook(SWord, Hodnota.ToUpper()); }
                    }


                }

                if (UTAJENI != "")
                    try
                    { UpdateForm("UTAJENI", UTAJENI); }
                    catch (Exception)
                    { UpdateBook("UTAJENI", UTAJENI); }

                if (STUPEN != "")
                    try
                    { UpdateForm("UTAJENI", STUPEN); }
                    catch (Exception)
                    { UpdateBook("UTAJENI", STUPEN); }


                if (STATUS != "")
                    try
                    { UpdateForm("UTAJENI", STATUS); }
                    catch (Exception)
                    { UpdateBook("UTAJENI", STATUS); }

                //https://stackoverflow.com/questions/6777422/disposing-of-microsoft-office-interop-word-application
                // Document
                //object saveOptionsObject = saveDocument ? Word.WdSaveOptions.wdSaveChanges : Word.WdSaveOptions.wdDoNotSaveChanges;
                //this.WordDocument.Close(ref saveOptionsObject, ref Missing.Value, ref Missing.Value);

                //Application
                //object saveOptionsObject = Word.WdSaveOptions.wdDoNotSaveChanges;
                //this.WordApplication.Quit(ref saveOptionsObject, ref Missing.Value, ref Missing.Value);

                Console.WriteLine("Uložení dokumentu");
                Console.WriteLine("");
                Doc.Save();
                Doc.Close(false);

                //object saveOptionsObject = Doc.Saved ? Microsoft.Office.Interop.Word.WdSaveOptions.wdSaveChanges : Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                //Doc.Close(ref saveOptionsObject, ref Missing.Value, ref Missing.Value);
                //Console.WriteLine("Zavření dokuemtu");
                //Doc.Close(ref saveOptionsObject);
                //Console.WriteLine("Dokument: " + Doc.ToString());

                Console.WriteLine("Ukončení aplikace Word");
                WordApp.Quit(false);
                Console.WriteLine("WordApp: " + WordApp.ToString());
                WordApp = null;

                //if (Doc != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(Doc);
                //if (WordApp != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(WordApp);
                //Console.WriteLine("Marshal.ReleaseComObject");

                Doc = null;
                WordApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Console.WriteLine("GC.Collect();");
                return true;
            }
            catch (Exception)
            {
                //Doc.Close();
                //WordApp.Quit();
                return false;
                //throw;
            }

        }

        /// <summary>
        /// Přenos dat do Wordu
        /// </summary>
        /// <param name="SouborWord">Cesta dokumentu DOC kam se mají data přenést</param>
        /// <returns></returns>
        //public bool Prenos(TeZak SouborWord)
        public void Prenos()
        {

            Microsoft.Office.Interop.Word.Application WordApp = null;
            //try
            //{
            //    Soubor Tridasoubor = new Soubor();
            //    DataSet Hlas = new DataSet();

            //    string Hodnota = "";
            //    //doc odkazuje na aktivní dokument
            //    //Microsoft.Office.Interop.Word.Document Doc = new Microsoft.Office.Interop.Word.Document();

            //    //Bude otevřena aplikace WORD
            //    Console.WriteLine("Bude otevřena aplikace WORD");
            //    //WordApp = WordApp1();  //Bude otevřen WORD
            //    //WordApp.Visible = true; //WORD bude zobrazen

            //    //DOC jedna se o zdílenou třídu mezi metodami.
            //    //Doc = WordApp.Documents.Add();        //novy dokument
            //    Doc = WordApp.Documents.Add();        //novy dokument

            //    string csv = Word + @"\word_data1.csv";
            //    Hlas = Tridasoubor.NactiCSV(csv);
            //    if (Hlas == null) return false;

            //    Console.WriteLine("Probíhá přenos dat");
            //    foreach (DataRow i in Hlas.Tables[0].Rows)
            //    {
            //        //sloupec 0 v csv nazev WORD
            //        string SWord = i[0].ToString();

            //        Hodnota = "";  //vymazání hodnoty
            //        //sloupec 1 v csv nazev database
            //        string SData = i[1].ToString();
            //        //pok hodnota z datatable

            //        //if (lok != "" || lok != "x")
            //        if (SData != "")
            //        {
            //            switch (SWord.ToUpper())
            //            {
            //                case "CDOK":
            //                    Hodnota = RadekParam("PCC") + "-" + RadekParam("TD") + "-" + RadekParam("PCDOC");
            //                    break;

            //                case "PROJEKT":
            //                    //C_PROJ
            //                    Hodnota = RadekParam(SData) + "." + RadekParam("C_UKOL");
            //                    /*if (RadekExistuje(SData))
            //                        Hodnota = CelyRadek[SData].ToString() + "." + CelyRadek["C_UKOL"].ToString();*/
            //                    break;

            //                case "ZAKAZNIK":
            //                    Hodnota = RadekParam("INVESTOR");
            //                    break;

            //                case "NAZEV":
            //                    Hodnota = RadekParam("NAZ_PROJ");
            //                    break;

            //                case "ZPRAC1":
            //                    string[] dfg = RadekParam(SData).Split(' ');
            //                    Hodnota = dfg[dfg.Length - 1].ToUpper(); //asi přijmení
            //                    break;

            //                case "KONTROL1":
            //                    string[] dfg1 = RadekParam(SData).Split(' ');
            //                    Hodnota = dfg1[dfg1.Length - 1].ToUpper(); //asi přijmení
            //                    break;

            //                case "SCHVALIL1":
            //                    //titul jmeno prijmeni
            //                    string[] dfg2 = RadekParam(SData).Split(' ');
            //                    Hodnota = dfg2[dfg2.Length - 1].ToUpper(); //asi přijmení
            //                    break;

            //                case "CA4":
            //                    if (RadekExistuje("DIL"))
            //                        Hodnota = RadekParam("DIL");
            //                    if (RadekExistuje("CAST"))
            //                        Hodnota += "." + RadekParam("CAST");
            //                    if (RadekExistuje("PROFESE"))
            //                        Hodnota += "-" + RadekParam("PROFESE");
            //                    if (RadekExistuje("PORADI"))
            //                        Hodnota += "." + RadekParam("PORADI");
            //                    if (RadekExistuje("OR_CISLO"))
            //                        Hodnota += "-" + RadekParam("OR_CISLO");
            //                    break;

            //                /*If IsNothing(CelyRadek("CAST").ToString()) Then Else CelyRadek("CAST").ToString()
            //                'pod = DIL + "." _
            //                '    + CAST + "-" _
            //                '    + PROFESE + "." _
            //                '    + PORADI + "-" _
            //                '    + OR_CISLO
            //                */
            //                case "REV":
            //                    Hodnota = RadekParam(SData);
            //                    break;

            //                case "DAT1": // "DAT2": "DAT3": "DAT4":
            //                    DateTime datum;
            //                    if (RadekParam(SData) == "")
            //                    {
            //                        Hodnota = RadekParam(SData);

            //                        datum = DateTime.Now;
            //                        Hodnota = datum.ToString("yyyy'-'MM");
            //                    }

            //                    else
            //                    {
            //                        datum = Convert.ToDateTime(RadekParam(SData));
            //                        Hodnota = datum.ToString("yyyy'-'MM");
            //                        //string[] pole = RadekParam(SData).Split(' ');
            //                        //string[] Prvni = RadekParam(pole[0]).Split('.');
            //                        //Hodnota = Prvni[1].Trim() + "/"+ Prvni[2].Trim();
            //                        //datum = (DateTime)RadekParam(SData);

            //                        DateTime pomoc = DateTime.Now;
            //                        if (datum.Year < 2018)
            //                        {
            //                            datum = new DateTime(pomoc.Year, datum.Month, datum.Day, datum.Hour, datum.Minute, datum.Second, datum.Millisecond);
            //                            Hodnota = datum.ToString("yyyy'-'MM'-'dd");
            //                        }
            //                    }
            //                    break;

            //                case "":
            //                    //Hodnota = "Prazdno";
            //                    Hodnota = RadekParam(SData);
            //                    break;

            //                default:
            //                    Hodnota = RadekParam(SData);
            //                    /*if (RadekExistuje(SData) == true)
            //                        Hodnota = CelyRadek[SData].ToString();
            //                    else
            //                        Hodnota = "Default";*/
            //                    break;
            //            }
            //        }

            //        //if (Hodnota == "") 
            //        //    Hodnota = "Nenalezeno";

            //        if (SWord != "")
            //        {
            //            try
            //            { UpdateForm(SWord, Hodnota.ToUpper()); }

            //            catch (Exception)
            //            { UpdateBook(SWord, Hodnota.ToUpper()); }
            //        }


            //    }

            //    if (UTAJENI != "")
            //        try
            //        { UpdateForm("UTAJENI", UTAJENI); }
            //        catch (Exception)
            //        { UpdateBook("UTAJENI", UTAJENI); }

            //    if (STUPEN != "")
            //        try
            //        { UpdateForm("UTAJENI", STUPEN); }
            //        catch (Exception)
            //        { UpdateBook("UTAJENI", STUPEN); }


            //    if (STATUS != "")
            //        try
            //        { UpdateForm("UTAJENI", STATUS); }
            //        catch (Exception)
            //        { UpdateBook("UTAJENI", STATUS); }

            //    //https://stackoverflow.com/questions/6777422/disposing-of-microsoft-office-interop-word-application
            //    // Document
            //    //object saveOptionsObject = saveDocument ? Word.WdSaveOptions.wdSaveChanges : Word.WdSaveOptions.wdDoNotSaveChanges;
            //    //this.WordDocument.Close(ref saveOptionsObject, ref Missing.Value, ref Missing.Value);

            //    //Application
            //    //object saveOptionsObject = Word.WdSaveOptions.wdDoNotSaveChanges;
            //    //this.WordApplication.Quit(ref saveOptionsObject, ref Missing.Value, ref Missing.Value);

            //    Console.WriteLine("Uložení dokumentu");
            //    Console.WriteLine("");
            //    Doc.Save();
            //    Doc.Close(false);

            //    //object saveOptionsObject = Doc.Saved ? Microsoft.Office.Interop.Word.WdSaveOptions.wdSaveChanges : Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            //    //Doc.Close(ref saveOptionsObject, ref Missing.Value, ref Missing.Value);
            //    //Console.WriteLine("Zavření dokuemtu");
            //    //Doc.Close(ref saveOptionsObject);
            //    //Console.WriteLine("Dokument: " + Doc.ToString());

            //    Console.WriteLine("Ukončení aplikace Word");
            //    WordApp.Quit(false);
            //    Console.WriteLine("WordApp: " + WordApp.ToString());
            //    WordApp = null;

            //    //if (Doc != null)
            //    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(Doc);
            //    //if (WordApp != null)
            //    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(WordApp);
            //    //Console.WriteLine("Marshal.ReleaseComObject");

            //    Doc = null;
            //    WordApp = null;
            //    GC.Collect();
            //    GC.WaitForPendingFinalizers();
            //    Console.WriteLine("GC.Collect();");
            //    return true;
            //}
            //catch (Exception)
            //{
            //    //Doc.Close();
            //    //WordApp.Quit();
            //    return false;
            //    //throw;
            //}

        }

        /// <summary>
        /// Otevře aplikaci word
        /// </summary>
        /// <returns>Microsoft.Office.Interop.Word.Application</returns>
        public static Microsoft.Office.Interop.Word.Application WordApp1()
        {
            try
            {
                //word je oteřen bude využit
                //return (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
                //object word = Marshal2.GetActiveObject("Word.Application");

                //NEFUNGUJE
                //if (!OperatingSystem.IsWindows()) return null;
                //var typ = System.Type.GetTypeFromProgID("Word.Application");
                //Microsoft.Office.Interop.Word.Application pokus = (Microsoft.Office.Interop.Word.Application)typ;
                //return pokus;

                return (Microsoft.Office.Interop.Word.Application)Marshal2.GetActiveObject("Word.Application");
            }
            catch (Exception)
            {
                return new Microsoft.Office.Interop.Word.Application();
                //throw;
            }
        }

        /// <summary>
        /// Kontrola existence parametry
        /// </summary>
        /// <param name="parametr">vstupní parametr DataRow</param>
        /// <returns>pokud není vrani ""</returns>
        public string RadekParam(string parametr)
        {
            try
            {
                //return 
                return CelyRadek[parametr].ToString(); ;
            }
            catch (Exception)
            {
                return "";
                //throw;
            }
        }

        /// <summary>
        /// Oveření prázdné hodnoty
        /// </summary>
        /// <param name="parametr"></param>
        /// <returns>bool</returns>
        bool RadekExistuje(string parametr)
        {
            try
            {
                //return 
                CelyRadek[parametr].ToString();
                return true;
            }
            catch (Exception)
            {
                //return "";
                return false;
                //throw;
            }
        }

        /// <summary>
        /// vyplnění založky ve wordu 
        /// </summary>
        /// <param name="BookmarkToUpdate"></param>
        /// <param name="TextToUse"></param>
        /// <property>Doc musí být nastaven na dokument wordu</property>
        /// <returns></returns>
        bool UpdateBook(string BookmarkToUpdate, string TextToUse)
        {
            if (Doc is null)
                return false;

            try
            {
                //Microsoft.Office.Interop.Word.Bookmark adf;
                //adf = doc.Bookmarks[BookmarkToUpdate];
                Microsoft.Office.Interop.Word.Range BMRange = Doc.Bookmarks[BookmarkToUpdate].Range;
                BMRange.Text = TextToUse;
                Doc.Bookmarks.Add(BookmarkToUpdate, BMRange);
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }

        }

        /// <summary>
        /// Vyplnění Formu ve wordu
        /// </summary>
        /// <param name="BookmarkToUpdate"></param>
        /// <param name="TextToUse"></param>
        /// <returns></returns>
        bool UpdateForm(string BookmarkToUpdate, string TextToUse)
        {
            if (Doc is null)
                return false;
            try
            {
                //Microsoft.Office.Interop.Word.FormFields test = doc.FormFields;
                //Microsoft.Office.Interop.Word.FormField adf;
                //adf = doc.FormFields[BookmarkToUpdate];

                if (Doc.FormFields[BookmarkToUpdate] is null)
                    return false;
                Doc.FormFields[BookmarkToUpdate].Result = TextToUse;
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }


    }

}
