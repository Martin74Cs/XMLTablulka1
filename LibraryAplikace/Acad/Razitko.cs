using AutoCAD;
using System.Data;
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
            //Dictionary<string, string> Pole = new Dictionary<string, string>();
            List<DataRazítka> Pole = [];
            //procházení vazeb souboru Podpora a hledání vazeb na razítko
            foreach (DataRow item in Hlas.Rows)
            {
                //jedná se o sloupec s podpůrného souboru jde o nazvy tagů v Databázi
                string Pomoc = item[1].ToString().Trim();
                string RazitkoTag = item[0].ToString().Trim();
                if (Pomoc == "") continue;
                //VyberSloupec test;
                //if (!Enum.TryParse(Pomoc, out test)) // continue; //prevod chyba
                switch (Pomoc)
                {
                    case "C_PROJ":
                        string[] dfg = teZak.C_PROJ.Split('.');
                        Deleni[i] = dfg[0];
                        Deleni[60] = dfg[^1];

                        break;
                    case "AUT_REV":
                    case "KONTROL":
                    case "SCHVALIL":
                        //string[] dfg1 = row[Pomoc].ToString().Split(' ');
                        //Deleni[i] = dfg1[dfg1.Length - 1].ToUpper().Trim();
                        break;

                    case "DAT_REV":
                        string[] dfg2 = new string[2];
                        //dfg2 = Sloupec.CelyRadek[Pomoc].ToString().Split('.');
                        string Den = dfg2[0];
                        string Mesic = dfg2[1];
                        string Rok = dfg2[2];
                        DateTime dateTime = DateTime.Now;
                        Rok = dateTime.Year.ToString();
                        Deleni[i] = (Den + " " + Mesic + " " + Rok).ToUpper();
                        break;

                    case "PRIDANO":
                        //Deleni[62] = Sloupec.CelyRadek["ID_DOCR"].ToString();
                        break;

                    case "APSSO":
                        string[] s = ["DIL", "CAST", "PROFESE", "PORADI", "OR_CISLO"];
                        string[] o = ["", ".", "-", ".", "-"];
                        string pom = "";
                        for (int rs = 0; rs < s.Length; rs++)
                        {
                            //if (!string.IsNullOrEmpty(Sloupec.CelyRadek[s[rs]].ToString()))
                            //{
                            //    pom += (o[rs] + Sloupec.CelyRadek[s[rs]].ToString()).Trim();
                            //}
                        }
                        Deleni[i] = pom;
                        break;

                    case "PROF_CX":
                        //Deleni[i] = Sloupec.CelyRadek[Pomoc].ToString().Trim() + Sloupec.CelyRadek["OR_CIT"].ToString();
                        break;
                    case "NAZ_UKOL":
                        //Deleni[i] = Sloupec.CelyRadek[Pomoc].ToString().Trim();
                        break;
                    case "OR_CIT":

                        break;

                    default:
                        //var type = teZak.GetType().GetProperty(Pomoc).GetType();
                        string hodnota = (string)teZak.GetType().GetProperty(Pomoc).GetValue(teZak);
                        if (!string.IsNullOrEmpty(RazitkoTag))
                        {
                            //type = typeof(string);
                            //string[] xkjhdfk = Razitko.GetType().GetProperties().Select(x => x.Name).ToArray();
                            Razitko.GetType().GetProperty(RazitkoTag).SetValue(Razitko, hodnota);
                        }
                        //if (!string.IsNullOrEmpty(Sloupec.CelyRadek[Pomoc].ToString()))
                        //Deleni[i] = Sloupec.CelyRadek[Pomoc].ToString().Trim();
                        break;
                }
                Pole.Add(new DataRazítka() { TagDatabaze = Pomoc, TagString = item[0].ToString().Trim(), TextString = Deleni[i] });
                i++;
            }
            Deleni[63] = Sloupec.CelyRadek[VyberSloupec.GLOBALID.ToString()].ToString();
            Pole.Add(new DataRazítka() { TagString = "GLOBALID", TextString = Deleni[63] });
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
                //if (!Enum.TryParse(Pomoc, out test)) // continue; //prevod chyba
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

            try { app.SelectionSets.Item("SS1").Delete(); }
            catch { }

            // Commands for ACAD
            if (app == null) return false;

            var myFilterType = new short[] { 0, 2 };
            var myFilterData = new object[] { "INSERT", "*v03_CZ" };

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
            razítkas = [];
            foreach (var item in typeof(JedenRadek).GetProperties())
            {
                razítkas.Add(new DataRazítka { TagString = item.Name, TagDatabaze = item.Name, TextString = "100" });
            }

            if (returnobj == null) return;
            object[] pole = (object[])returnobj.GetAttributes();
            for (int j = 0; j <= pole.GetUpperBound(0); j++)
            {
                string hledej = ((AcadAttributeReference)pole[j]).TagString;
                //for (int i = 0; i <= tag.GetUpperBound(0); i++)
                for (int i = 0; i <= razítkas.Count - 1; i++)
                {
                    //if (tag == null) return;
                    if (hledej.Equals(razítkas[i].TagString, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //if (info == null) return;
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
