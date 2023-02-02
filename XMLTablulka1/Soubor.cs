using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLTabulka1
{

    public static class Soubor
    {
        /// <summary>        /// KopieDbf (Cesta, zakaz kopirovani)     /// </summary>
        public static bool KopieDbf(bool VytvorKopii = false)
        {
            if (VytvorKopii == true)
            {
                if (File.Exists(Cesty.SouborDbf))
                {
                    if (Stari() > new TimeSpan(1, 0, 0))  //rozdíl staří databazí mezi sebou více než 1 hodina
                    {
                        //Console.WriteLine("Mazání staré Databáze");
                        File.Delete(Cesty.SouborDbf);
                        //Console.WriteLine("Kopírování nové verze Databáze");
                        File.Copy(Cesty.ZdrojDbf, Cesty.SouborDbf);
                    }
                }
                //Databáze zůstane stávající - NEBUDE kopírována
                else
                {
                    //Console.WriteLine("Kopírování Databáze");
                    File.Copy(Cesty.ZdrojDbf, Cesty.SouborDbf);
                }
            }
            return true;
        }

        /// <summary>        /// Stáří kopírované databáze        /// </summary>
        public static TimeSpan Stari()
        {
            if (!File.Exists(Cesty.ZdrojDbf)) { throw new FileNotFoundException($"Soubor {Cesty.ZdrojDbf} nebyl nalezen "); }
            if (!File.Exists(Cesty.SouborDbf)) { throw new FileNotFoundException($"Soubor {Cesty.SouborDbf} nebyl nalezen "); }

            //čas posledního zápisu
            DateTime ZdrojDatum = File.GetLastWriteTime(Cesty.ZdrojDbf);
            DateTime KopieDatum = File.GetLastWriteTime(Cesty.SouborDbf);
            //Rozdíl času
            return ZdrojDatum.Subtract(KopieDatum);
        }

        /// <summary>        /// Uloži table do txt        /// </summary>
        public static void SaveTXT(this DataTable data, string Cesta)
        {
            using var zak = new System.IO.StreamWriter(Cesta);
            string pom = "";
            foreach (DataColumn item in data.Columns)
            {
                pom += item.ColumnName + ";";
            }
            zak.WriteLine(pom.Substring(0, pom.Length - 1));

            foreach (DataRow item in data.Rows)
            { 
                pom ="";
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    pom += item[i].ToString() + ";";
                }
                zak.WriteLine(pom.Substring(0,pom.Length-1));
            }
            zak.Close();
        }

        /// <summary>        /// Uloži string[] do txt        /// </summary>
        public static void SaveTXT(this string[] data, string Cesta)
        {
            using var zak = new System.IO.StreamWriter(Cesta);
            foreach (string item in data)
                zak.WriteLine(item.ToString());
            zak.Close();
        }

        /// <summary>        /// Nacti string[] z txt podle radku        /// </summary>
        public static string[] LoadTXT(string Cesta)
        {
            string Pole = "";
            using FileStream fs = new FileStream(Cesta, FileMode.Open, FileAccess.Read);
            using StreamReader sw = new(fs);
            while (!sw.EndOfStream)
            {
                Pole += sw.ReadLine().ToString() + ";";
            }
            sw.Close();
            //return Pole.Split('\u002C');
            return Pole.Split(';',StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>Uloži DataTable do XML </summary>
        public static bool SaveXML(DataTable Table, string CestaXML)
        {
            if (Table == null) return false;
            try
            {
                FileStream fs = new(CestaXML, FileMode.Create);
                Table.WriteXml(fs);
                fs.Close();

                fs = new FileStream(Path.ChangeExtension(CestaXML, ".schema"), FileMode.Create);
                Table.WriteXmlSchema(fs);
                fs.Close();

                return true;
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Chyba uládání DataXML Soubor ");
            }
            catch (DataException)
            {
                throw new Exception("Chyba uládání DataXML Soubor ");
            }
        }

        /// <summary> Nacte XML do DataTable </summary>
        public static DataTable LoadXML(string CestaXML)
        {
            if (!File.Exists(CestaXML)) return null;
            DataTable Table = new();
            try
            {
                //TextReader tr = new TextReader(fs);
                //XmlTextReader xtr = new XmlTextReader(fs);
                //XmlReader xr = new XmlReader(fs);
                FileStream fs = new(Path.ChangeExtension(CestaXML, ".schema"), FileMode.Open);
                Table.ReadXmlSchema(fs);
                fs.Close();
                fs = new(CestaXML, FileMode.Open);
                Table.ReadXml(fs);
                fs.Close();
                return Table;    
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Chyba cteni DataXML Soubor ");
            }
            catch (DataException)
            {
                throw new Exception("Chyba cteni DataXML Soubor ");
            }
        }

        public static bool SaveJson(this DataTable Table)

        {
            string temp = JsonConvert.SerializeObject(Table);
            Soubor.SaveTXT(Table, Cesty.Pomoc + @"\Json.txt");
            DataTable Nove = (DataTable)JsonConvert.DeserializeObject(temp, typeof(DataTable) );
            Soubor.SaveTXT(Nove, Cesty.Pomoc + @"\JsonnNew.txt");

              return true;
        }


        /// <summary>Ze souboru CSV nacte data do datatable </summary>
        public static DataTable CSVtoDataTable(string Soubor)
        {
            DataTable Table = new();
            if (!File.Exists(Soubor)) return Table;
            Table.TableName = "Cestina";
            using FileStream fs = new(Soubor, FileMode.Open);
            using StreamReader sr = new(fs);
            string[] Radek = sr.ReadLine().Split(';');
            foreach (string item in Radek)
            {
                Table.Columns.Add(item,typeof(string));
            }
            while (!sr.EndOfStream)
            {
                string? cti = sr.ReadLine();
                DataRow row = Table.NewRow();
                string[] data = cti.Split(';');
                for (int i = 0; i < Table.Columns.Count; i++)
                {
                    row[i] = data[i];
                }
                //Table.Rows.Add(data);
                Table.Rows.Add(row);
            }
            return Table;
        }
       
        public static void DataTabletoCSV(this DataTable Table, string Soubor)
        {
            //DataTable Table = new() { TableName = "cestina", } ; 
            Table.TableName = "Cestina";
            using FileStream fs = new(Soubor, FileMode.Create);
            using StreamWriter sw = new(fs);
            string Pole = "";
            foreach (DataColumn item in Table.Columns)
            {
                Pole += item.ColumnName + ";";
            }
            sw.WriteLine(Pole.Substring(0,Pole.Length-1));

            foreach (DataRow item in Table.Rows)
            {
                Pole = "";
                foreach (DataColumn col in Table.Columns)
                {
                    string? hl = item[col].ToString();
                    if ((hl == @"N=A4 tech_z(TP-N-xxxx); Text") ||
                        (hl == @"G= Výkres (C1-G-xxxx); Vykr") ||
                        (hl == @"S= A4 Static.vyp(TP-S-xxxx); Text"))
                    {
                        string[] pom = hl.Split(';');
                        Pole += pom[0] + " " + pom[1] + ";";
                    }
                    else
                        Pole += item[col].ToString() + ";";
                }
                sw.WriteLine(Pole.Substring(0, Pole.Length-1));
            }
        }
    }
}
