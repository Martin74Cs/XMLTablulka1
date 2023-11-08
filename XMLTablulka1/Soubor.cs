using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XMLTabulka1.Trida;

namespace XMLTabulka1
{

    public static class Soubor
    {
        public static void Pruzkumnik(string cesta = @"C:\") 
        {
            Process process = new Process();
            process.StartInfo.FileName = "explorer.exe";
            process.StartInfo.Arguments = cesta;
            process.Start();
        }


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

        /// <summary>        /// Uloži string[] do txt do jednotlivých řádků      /// </summary>
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
            string[] Res = Pole.Split(';', StringSplitOptions.RemoveEmptyEntries);
            return Res;
        }

        /// <summary>        /// Nacti XmlDocument ze souboru  /// </summary>
        public static XDocument LoadXML(string Cesta)
        {
            if(!File.Exists(Cesta)) return null;
            XDocument Pole = XDocument.Load(Cesta);
            //string xmlString = File.ReadAllText(Cesty.PodporaDataXml);
            //Pole.LoadXml(xmlString);
            return Pole;
        }

        /// <summary>
        /// Uložení xml dokumentu do souboru Cesta
        /// </summary>       
        public static void SaveXML(this XDocument doc, string Cesta)
        {
            if (File.Exists(Cesta)) return;
            doc.Save(Cesta);           
        }

        public static void SaveXML<T>(this List<T> moje, string Cesta)
        {
            //File.Delete(Cesta);
            FileStream writer = new(Cesta, FileMode.Create);
            XmlSerializer ser = new XmlSerializer(typeof(List<T>),
                new XmlRootAttribute("SEZNAM"));
            ser.Serialize(writer, moje);
            writer.Close();
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
        public static DataTable LoadXMLToDataTable(string CestaXML)
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

        public static bool LoadSaveJson(this DataTable Table)
        {
            string temp = JsonConvert.SerializeObject(Table);
            Soubor.SaveTXT(Table, Cesty.Pomoc + @"\Json.txt");
            DataTable Nove = (DataTable)JsonConvert.DeserializeObject(temp, typeof(DataTable) );
            Soubor.SaveTXT(Nove, Cesty.Pomoc + @"\JsonnNew.txt");
             return true;
        }

        public static void LoadJson(this List<MojeZakazky> moje, string cesta)
        {
            if (File.Exists(cesta))
            {
                string jsonString = File.ReadAllText(cesta);
                moje = System.Text.Json.JsonSerializer.Deserialize<List<MojeZakazky>>(jsonString)!;
            }
        }

        public static T LoadJson<T>(string cesta) where T : class
        {
            if (File.Exists(cesta))
            {
                string jsonString = File.ReadAllText(cesta);
                T moje = System.Text.Json.JsonSerializer.Deserialize<T>(jsonString)!;
                return moje;
            }
            return null;
        }

        public static List<T> LoadJsonList<T>(string cesta) where T : class
        {
            if (File.Exists(cesta))
            {
                string jsonString = File.ReadAllText(cesta);
                List<T> moje = System.Text.Json.JsonSerializer.Deserialize<List<T>>(jsonString)!;
                return moje;
            }
            return new();
        }

        public static List<T> LoadXML<T>(string cesta) where T : class
        {
            if (File.Exists(cesta))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>),
                new XmlRootAttribute("SEZNAM"));
                var reader = new StreamReader(cesta);
                List<T> Pole = (List<T>)xmlSerializer.Deserialize(reader);
                reader.Close();
                return Pole;

                //XDocument xml = XDocument.Load(cesta);
                //string json = XMLTabulka1.Soubor.XmltoJson(xml);
                //string jsonString = File.ReadAllText(cesta);
                //XmlSerializer xmlSerializer = new XmlSerializer(typeof(Moje));
                //xmlSerializer.Deserialize(reader);  

            }
            return new();
        }

        public static void SaveJson(this List<MojeZakazky> moje, string cesta)
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(moje);
            File.WriteAllText(cesta, jsonString);
        }

        public static void SaveJson<T>(this T moje, string cesta)
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(moje);
            File.WriteAllText(cesta, jsonString);
        }

        public static void SaveJson<T>(this List<T> moje, string cesta)
        {
            //string jsonString = System.Text.Json.JsonSerializer.Serialize(moje);
            string jsonString = JsonConvert.SerializeObject(moje, Newtonsoft.Json.Formatting.Indented);
            
            File.WriteAllText(cesta, jsonString);
        }

        /// <summary>
        /// Převod DataTable na Json definovany <T>
        /// </summary>
        public static List<T> DataTabletoJson<T>(this DataTable table)
        {
            List<T> teZaks = new List<T>();

            foreach (DataRow row in table.Rows)
            {
                Type temp = typeof(T);
                T obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in table.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == column.ColumnName)
                            pro.SetValue(obj, row[column.ColumnName].ToString(), null);
                        else
                            continue;
                    }
                }
                teZaks.Add(obj);
            }
            return teZaks;
        }

        /// <summary>
        /// Převod XmlDocument na Json string
        /// </summary>
        public static string XmltoJson(XDocument doc)
        {
            return JsonConvert.SerializeXNode(doc, Newtonsoft.Json.Formatting.Indented);
        }

        public static string XmlToJsonWithoutRoot(XDocument xml)
        {
            return JsonConvert.SerializeXNode(xml, Newtonsoft.Json.Formatting.None, omitRootObject: true);
        }

        private static readonly XDeclaration _defaultDeclaration = new("1.0", null, null);
        public static string JsonToXml(string json)
        {
            var doc = JsonConvert.DeserializeXNode(json);
            var declaration = doc.Declaration ?? _defaultDeclaration;
            return $"{declaration}{Environment.NewLine}{doc}";
        }
        public static string JsonToXmlWithExplicitRoot(string json, string rootName)
        {
            var doc = JsonConvert.DeserializeXNode(json, rootName);
            var declaration = doc.Declaration ?? _defaultDeclaration;
            return $"{declaration}{Environment.NewLine}{doc}";
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
                string cti = sr.ReadLine();
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
                    string hl = item[col].ToString();
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
