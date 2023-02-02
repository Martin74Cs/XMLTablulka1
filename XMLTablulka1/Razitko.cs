using System.Data;

namespace XMLTabulka1
{
    public class Razitko
    {
        public Dictionary<string, string> Prenos(DataRow row)
        {
            DataTable Hlas = Soubor.CSVtoDataTable(Cesty.PodporaSpolecneCsv);
            if (Hlas == null) return null;
            int i = 1;
            string[] Deleni = new string[70];
            Dictionary<string, string> Pole = new Dictionary<string, string>();
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
                        Deleni[60] = dfg[dfg.Length - 1];

                        break;
                    case "AUT_REV":
                    case "KONTROL":
                    case "SCHVALIL":
                        string[] dfg1 = row[Pomoc].ToString().Split(' ');
                        Deleni[i] = dfg1[dfg1.Length - 1].ToUpper().Trim();
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
                        string[] s = { "DIL", "CAST", "PROFESE", "PORADI", "OR_CISLO" };
                        string[] o = { "", ".", "-", ".", "-" };
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
                        if (!String.IsNullOrEmpty(Sloupec.CelyRadek[Pomoc].ToString()))
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

    }
}
