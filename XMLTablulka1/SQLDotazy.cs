﻿using System.Data;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace XMLTabulka1
{
    public class SQLDotazy
    {

        /// <summary>
        /// Vratí tabulku dle dotazu
        /// </summary>
        public static DataTable Hledej(string Dotaz)
        {
            DataSet Data = Dbf.Pripoj(Dotaz);
            if (Data == null) return new DataTable();// throw new ArgumentNullException($"Podle krytérii nebyly nenalezeny záznamy");
            //return Data.Tables[0];
            return new Cestina().Tabulka(Data.Tables[0]);
        }

        /// <summary>
        /// Vratí tabulku celé databaze
        /// </summary>
        public DataTable HledejVse()
        {
            string Dotaz = "SELECT * FROM TEZAK";
            //DbfXml dbf = new();
            DataSet Data = Dbf.Pripoj(Dotaz);
            if (Data == null) throw new ArgumentNullException($"Podle {Data} krytérii nebyly nenalezeny záznamy");
            //return Data.Tables[0];
            return new Cestina().Tabulka(Data.Tables[0]);
        }

        /// <summary>
        /// Vratí Pole stringů názvy sloupců Databaze Tezek
        /// </summary>
        public string[] SloupceTezak()
        {
            string Dotaz = "SELECT TOP 1 * FROM TEZAK";
            DataSet Data = Dbf.Pripoj(Dotaz);
            if (Data == null) throw new ArgumentNullException($"Podle {Data} krytérii nebyly nenalezeny záznamy");
            List<string> Pole = new();
            foreach (DataColumn item in Data.Tables[0].Columns)
            {
                Pole.Add(item.ColumnName);
            }
            return Pole.ToArray();
        }

        /// <summary>
        /// Vrati jeden radek dle podmínky SELECT TOP 1 *
        /// </summary>
        public DataTable JedenTezak(string Hledej, string Polozka)
        {
            string Dotaz = $"SELECT TOP 1 * FROM TEZAK WHERE {Hledej} = '{Polozka}'";
            DataSet Data = Dbf.Pripoj(Dotaz);
            if (Data == null) throw new ArgumentNullException($"Podle {Data} krytérii nebyly nenalezeny záznamy");
            return new Cestina().Tabulka(Data.Tables[0]);
        }

        /// <summary>
        /// Vrátí tabulku kde vyhovuje Podmínka 1.nazev sloupce, 2.kriterium
        /// </summary>
        public DataTable HledejPrvek(VyberSloupec Prvek, string CisloProjektu)
        {
            if (CisloProjektu == "") throw new Exception($"Číslo {CisloProjektu} nexistuje"); // return null;
            //string Dotaz = "SELECT * FROM TEZAK WHERE " + Prvek + " ='" + CisloProjektu + "' ORDER BY PCDOC";
            //string Dotaz = "SELECT * FROM TEZAK WHERE " + Prvek + " ='" + CisloProjektu + "' ORDER BY C_UKOL,[DIL],[CAST],PROFESE,PORADI,OR_CISLO";
            string Dotaz = "SELECT * FROM TEZAK WHERE " + Prvek + " ='" + CisloProjektu + "' ORDER BY [DIL],[CAST],PROFESE,PORADI,OR_CISLO";
            DataSet Data = Dbf.Pripoj(Dotaz);
            if (Data == null) throw new Exception($"Podle {Data} krytérii nebyl v databázi nenalezen žádný záznam");
            return new Cestina().Tabulka(Data.Tables[0]);
        }

        /// <summary>
        /// Vrátí tabulku kde vyhovuje Podmínka 1.nazev sloupce, 2.kriterium, 3.nazev sloupce, 4.kriterium
        /// </summary>
        public DataTable HledejPrvek(VyberSloupec Prvek1, string PrvekText1, VyberSloupec Prvek2, string PrvekText2)
        {
            if (PrvekText1 == "") throw new ArgumentNullException($"Číslo {PrvekText1} nexistuje");
            if (PrvekText2 == "") throw new ArgumentNullException($"Číslo {PrvekText2} nexistuje");

            string Dotaz = "SELECT * FROM TEZAK WHERE " + Prvek1 + "='" + PrvekText1 + "' AND " + Prvek2 + "='" + PrvekText2 + "' AND (NOT (DIL IS NULL))";
            DataSet data = Dbf.Pripoj(Dotaz);
            if (data == null) throw new ArgumentNullException($"Podle {data} krytérii nebyl v databázi nenalezen žádný záznam");
            DataSet Data = Dbf.Pripoj(Dotaz);
            return new Cestina().Tabulka(Data.Tables[0]);

        }

        /// <summary>
        /// Vrátí neopakující se obsah zadaného nazvu sloupce. Řazení sestupně DESC.
        /// </summary>
        public string[] SeznamJeden(VyberSloupec Prvek)
        {
            string Dotaz = $"SELECT DISTINCT {Prvek} FROM tezak  ORDER BY {Prvek} DESC";
            DataSet data = Dbf.Pripoj(Dotaz);
            if (data == null) throw new ArgumentNullException($"Podle {data} krytérii nebyl v databázi nenalezen žádný záznam");
            DataTable Table = new Cestina().Tabulka(data.Tables[0]);

            Stack<string> Nazev = new Stack<string>();
            foreach (DataRow item in Table.Rows)
            {
                Nazev.Push(item[Prvek.ToString()].ToString().Trim());
            }
            return Nazev.ToArray();
            //new XmlTabulka.Cestina().Tabulka(data);
        }



        /// <summary>
        /// Hledej číslo dokumentu ve formatu T1234, N9876 
        /// </summary>
        public DataTable HledejCislo(string HledejCislo)
        {
            if (HledejCislo == "") throw new ArgumentNullException($"Číslo {HledejCislo} nexistuje", HledejCislo); // return null;
            string TD = HledejCislo.First().ToString().ToUpper(); //první znak retezce
            string PCDOC = HledejCislo.Replace(TD, "");
            if (PCDOC.Length != 4) throw new ArgumentException($"Chyba parsovaní Čísla {HledejCislo}"); // return null;
            string Dotaz = "SELECT * FROM TEZAK WHERE TD='" + TD + "' AND PCDOC='" + PCDOC + "' ORDER BY DIL,[CAST],PROFESE,PORADI,OR_CISLO";
            DataSet Data = Dbf.Pripoj(Dotaz);
            if (Data == null) return null;
            return new Cestina().Tabulka(Data.Tables[0]);
            //return new Dbf(KopieDbf).Pripoj(Dotaz);
        }
    }
}
