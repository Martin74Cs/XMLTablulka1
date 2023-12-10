using AutoCAD;
using System.Data;
using System.Reflection;
using XMLTabulka1;
using XMLTabulka1.Trida;

namespace LibraryAplikace.Acad
{
    public class Razitko
    {
        public static List<DataRazítka> Prenos(TeZak teZak)
        { 
            JedenRadek Razitko = new();
            var Hlas = Soubor.CSVtoDataTable(Cesty.PodporaSpolecneCsv);
            if (Hlas == null) return null;
            int i = 1;
            string[] Deleni = new string[70];
            //string TeZAkValue = string.Empty;
            //Dictionary<string, string> Pole = new Dictionary<string, string>();
            List<DataRazítka> Pole = [];
            //procházení vazeb souboru Podpora a hledání vazeb na razítko
            foreach (DataRow item in Hlas.Rows)
            {
                //jedná se o sloupec s podpůrného souboru jde o nazvy tagů v Databázi
                string SouborDatabaze = item[1].ToString().Trim();
                string RazitkoTag = item[0].ToString().Trim();
                if (SouborDatabaze == "") continue;
                string TeZAkValue = teZak.GetType().GetProperty(SouborDatabaze).GetValue(teZak).ToString();
                //VyberSloupec test;
                //if (!Enum.TryParse(SouborDatabaze, out test)) // continue; //prevod chyba
                switch (SouborDatabaze)
                {
                    case "C_PROJ":
                        string[] dfg = teZak.C_PROJ.Split('.');
                        TeZAkValue = dfg.Last();
                        //Deleni[i] = dfg[0];
                        //Deleni[60] = dfg[^1];

                        break;
                    case "AUT_REV":
                    case "KONTROL":
                    case "SCHVALIL":
                    case "HIP":
                        //string[] dfg1 = row[SouborDatabaze].ToString().Split(' ');
                        //Deleni[i] = dfg1[dfg1.Length - 1].ToUpper().Trim();
                        string[] dfg1 = TeZAkValue.Split(' ');
                        TeZAkValue = dfg1.Last();
                        break;

                    case "DAT_REV":
                        string[] dfg2 = new string[2];
                        //dfg2 = Sloupec.CelyRadek[SouborDatabaze].ToString().Split('.');
                        dfg2 = TeZAkValue.Split('.');
                        DateTime dateTime = DateTime.Now;
                        if (dfg2.Length > -1)
                            if(double.TryParse(dfg2[0], out double result))
                                dateTime.AddDays(result);
                        if (dfg2.Length > 0)
                            if (int.TryParse(dfg2[1], out int result))
                                dateTime.AddMonths(result);
                        if (dfg2.Length > 1)
                            if (int.TryParse(dfg2[1], out int result))
                                dateTime.AddYears(result);
                        TeZAkValue = (dateTime.Day + " " + dateTime.Month + " " + dateTime.Year).ToUpper();
                        break;

                    case "PRIDANO":
                        //Deleni[62] = Sloupec.CelyRadek["ID_DOCR"].ToString();
                        break;

                    case "APSSO":
                        string[] s = ["DIL", "CAST", "PROFESE", "PORADI", "OR_CISLO"];
                        string[] o = ["", ".", "-", ".", "-"];
                        //string pom = "";
                        for (int rs = 0; rs < s.Length; rs++)
                        {
                            string test =  (string)teZak.GetType().GetProperty(s[rs]).GetValue(teZak);
                            if (!string.IsNullOrEmpty(test))
                                TeZAkValue = TeZAkValue + o[rs] + test ;
                            //if (!string.IsNullOrEmpty(Sloupec.CelyRadek[s[rs]].ToString()))
                            //{
                            //    pom += (o[rs] + Sloupec.CelyRadek[s[rs]].ToString()).Trim();
                            //}
                        }
                        //Deleni[i] = pom;
                        break;

                    case "PROF_CX":
                        //Deleni[i] = Sloupec.CelyRadek[SouborDatabaze].ToString().Trim() + Sloupec.CelyRadek["OR_CIT"].ToString();
                        TeZAkValue = teZak.PROF_CX + teZak.OR_CISLO;
                        break;
                    case "NAZ_UKOL":
                        //Deleni[i] = Sloupec.CelyRadek[SouborDatabaze].ToString().Trim();
                        break;
                    case "OR_CIT":

                        break;

                    default:
                        //var JmenoTeZak = teZak.GetType().GetProperty(SouborDatabaze).GetType();
                        //TeZAkValue = (string)teZak.GetType().GetProperty(SouborDatabaze).GetValue(teZak);
                        if (!string.IsNullOrEmpty(RazitkoTag))
                        {
                            //type = typeof(string);
                            //string[] xkjhdfk = Razitko.GetType().GetProperties().Select(x => x.Name).ToArray();
                            //Razitko.GetType().GetProperty(RazitkoTag).SetValue(Razitko, TeZAkValue);
                        }
                        //if (!string.IsNullOrEmpty(Sloupec.CelyRadek[SouborDatabaze].ToString()))
                        //Deleni[i] = Sloupec.CelyRadek[SouborDatabaze].ToString().Trim();
                        break;
                }
                Pole.Add(new DataRazítka() { TagDatabaze = SouborDatabaze, TagString = RazitkoTag, TextString = TeZAkValue });
                i++;
            }
            //Deleni[63] = Sloupec.CelyRadek[VyberSloupec.GLOBALID.ToString()].ToString();
            //Pole.Add(new DataRazítka() { TagString = "GLOBALID", TextString = Deleni[63] });
            return Pole;
        }

        public static Dictionary<string, string> Prenos(DataRow row)
        {
            DataTable Hlas = Soubor.CSVtoDataTable(Cesty.PodporaSpolecneCsv);
            if (Hlas == null) return null;
            int i = 1;
            string[] Deleni = new string[70];
            Dictionary<string, string> Pole = [];
            foreach (DataRow item in Hlas.Rows)
            {
                string Pomoc = item[1].ToString().Trim();
                if (Pomoc == "") continue;
                //VyberSloupec test;
                //if (!Enum.TryParse(SouborDatabaze, out test)) // continue; //prevod chyba
                switch (Pomoc)
                {
                    case "C_PROJ":
                        string[] dfg = row[Pomoc].ToString().Split('.');
                        Deleni[i] = dfg[0];
                        Deleni[60] = dfg[^1];

                        break;
                    case "AUT_REV":
                    case "KONTROL":
                    case "SCHVALIL":
                        string[] dfg1 = row[Pomoc].ToString().Split(' ');
                        Deleni[i] = dfg1[^1].ToUpper().Trim();
                        break;

                    case "DAT_REV":
                        string[] dfg2 = new string[2];
                        dfg2 = Sloupec.CelyRadek[Pomoc].ToString().Split('.');
                        string Den = dfg2[0];
                        string Mesic = dfg2[1];
                        string Rok = dfg2[2];
                        DateTime dateTime = DateTime.Now;
                        Rok = dateTime.Year.ToString();
                        Deleni[i] = (Den + " " + Mesic + " " + Rok).ToUpper();
                        break;

                    case "PRIDANO":
                        Deleni[62] = Sloupec.CelyRadek["ID_DOCR"].ToString();
                        break;

                    case "APSSO":
                        string[] s = ["DIL", "CAST", "PROFESE", "PORADI", "OR_CISLO"];
                        string[] o = ["", ".", "-", ".", "-"];
                        string pom = "";
                        for (int rs = 0; rs < s.Length; rs++)
                        {
                            if (!string.IsNullOrEmpty(Sloupec.CelyRadek[s[rs]].ToString()))
                            {
                                pom += (o[rs] + Sloupec.CelyRadek[s[rs]].ToString()).Trim();
                            }
                        }
                        Deleni[i] = pom;
                        break;

                    case "PROF_CX":
                        Deleni[i] = Sloupec.CelyRadek[Pomoc].ToString().Trim() + Sloupec.CelyRadek["OR_CIT"].ToString();
                        break;
                    case "NAZ_UKOL":
                        Deleni[i] = Sloupec.CelyRadek[Pomoc].ToString().Trim();
                        break;
                    case "OR_CIT":

                        break;

                    default:
                        if (!string.IsNullOrEmpty(Sloupec.CelyRadek[Pomoc].ToString()))
                            Deleni[i] = Sloupec.CelyRadek[Pomoc].ToString().Trim();
                        break;
                }
                Pole.Add(item[0].ToString().Trim(), Deleni[i]);
                i++;
            }
            Deleni[63] = Sloupec.CelyRadek[VyberSloupec.GLOBALID.ToString()].ToString();
            Pole.Add("GLOBALID", Deleni[63]);
            return Pole;
        }

        public static bool VyberRazitkaAcad(AcadDocument app, List<DataRazítka> razítkas)
        {
            // Add tag info to the lists
            if (razítkas == null) return false;

            //AcadSelectionSet mySel;
            //AcSelect mode;
            //object myFilterType;
            //object myFilterData;
            //short[] myFiltertypeArray = new short[2];
            //object[] myfilterdataArray = new object[2];

            try { app.SelectionSets.Item("SS1").Delete();  }
            catch { }

            // Commands for ACAD

            if (app == null) return false;

            var myFilterType = new short[] { 0, 2 };
            var myFilterData = new object[] { "INSERT", "*v03_CZ" };
            Thread.Sleep(2000);
            var mySel = app.SelectionSets.Add("SS1");
            var mode = AcSelect.acSelectionSetAll;
            //myFiltertypeArray[0] = 0;
            //myfilterdataArray[0] = "INSERT";
            //myFiltertypeArray[1] = 2;
            //myfilterdataArray[1] = "*v03_CZ";
            //myFilterType = myFiltertypeArray;
            //myFilterData = myfilterdataArray;
            mySel.Select(mode, null, null, myFilterType, myFilterData);
            if (mySel == null) return false;

            // mySel contains the selection of all objects in the stamp
            foreach (AcadBlockReference sel in mySel)
            {
                Thread.Sleep(100);
                string name1 = sel.EffectiveName;
                if (name1.EndsWith("_v03_CZ"))
                {
                    VyplnBlokyAcad(sel, razítkas);
                }
            }

            //zkracení ověření hodnoty null
            app?.SelectionSets.Item("SS1").Delete();
            return true;
        }

        public static void VyplnBlokyAcad(AcadBlockReference returnobj, List<DataRazítka> razítkas)
        {
            //razítkas = [];
            //foreach (var item in typeof(JedenRadek).GetProperties())
            //{
            //    razítkas.Add(new DataRazítka { TagString = item.Name, TagDatabaze = item.Name, TextString = "100" });
            //}

            if (returnobj == null) return;

            //Thread.Sleep(2000);

            //seznam atributu vybraného bloku
            object[] pole = (object[])returnobj.GetAttributes();
            for (int j = 0; j <= pole.GetUpperBound(0); j++)
            {
                //nazvy atributů vybraného bloku
                string hledej = ((AcadAttributeReference)pole[j]).TagString;

                //procházení listu razitkas
                for (int i = 0; i <= razítkas.Count - 1; i++)
                {
                    //hledání shody atributu bloku a razitkas
                    if (hledej.Equals(razítkas[i].TagString, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //vyplnění hodnoty razíkta
                        if (razítkas[i].TextString == "")
                        {
                            ((AcadAttributeReference)pole[j]).TextString = " ";
                        }
                        else
                        {
                            ((AcadAttributeReference)pole[j]).TextString = razítkas[i].TextString;
                            //int adf = razítkas[i].TextString.Length;
                            int delka = razítkas[i].TextString.Length;
                            double A = 1;

                            switch (hledej)
                            {
                                case "SUBJECT1":
                                    if (delka > 35)
                                    {
                                        A = 65 / (delka * 2);
                                    }
                                    ((AcadAttributeReference)pole[j]).ScaleFactor = A;
                                    break;
                                case "OCD":
                                    if (delka > 7)
                                    {
                                        A = 18 / (delka * 2);
                                    }
                                    ((AcadAttributeReference)pole[j]).ScaleFactor = A;
                                    break;
                            }
                        }

                        break;
                    }
                }
            }
        }

    }

    public class DataRazítka
    {
        public string TagString { get; set; }
        public string TextString { get; set; }
        public string TagDatabaze { get; set; }
    }
}
