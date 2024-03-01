using System;
using System.Data;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace XMLTabulka1
{
    public class Dbf
    {
        private readonly string cesty;

        /// <summary>
        /// Připojení databaze DBF, Data jsou uloženy na cestě Cesty.SouborDbf
        /// </summary>
        /// <param name="Querry">Dotaz</param>
        /// <returns>DataSet</returns>

        public Dbf(string Cesty)
        {
            cesty = Cesty;
        }

        //public string Cesty { get; set; }

        public DataSet Pripoj(string Querry)
        {
            //if (File.Exists(Cesty.SouborDbf) == false) return null;
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) throw new Exception($"System nepoužívá platformu Windows");
            //FileInfo InformaceOSouboru = new FileInfo(Cesty.SouborDbf);
            //if (!InformaceOSouboru.Exists) throw new FileNotFoundException($"Soubor {Cesty.SouborDbf} nenalezen");
            //string AdresarDbf = InformaceOSouboru.DirectoryName;

            if (!System.IO.Directory.Exists(cesty)) throw new DirectoryNotFoundException($"Adresar {cesty} nenalezen");
            //if (!System.IO.Directory.Exists(Cesty.SouborDbf)) throw new DirectoryNotFoundException($"Adresar {Cesty.SouborDbf} nenalezen");
            if (Querry == null || Querry == "") throw new Exception("Chyba dotaz je prázdný"); ;

            try
            {
                //using OleDbConnection Con = new() { ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Cesty.SouborDbf + ";Extended Properties=dBase IV" };
                using OleDbConnection Con = new() { ConnectionString = "Provider=Microsoft.ACE.Oledb.12.0; Data Source=" + Cesty.SouborDbf + ";Extended Properties=dBase IV" };
                using OleDbCommand dbfcmd = new(Querry, Con);
                using OleDbDataAdapter dbfda = new(dbfcmd);
                using DataSet dbase = new();
                //pokud nefunguje dopln do cproj
                //<BuiltInComInteropSupport>true</BuiltInComInteropSupport>
                dbfda.Fill(dbase);
                if (dbase == null) return null;                     //throw new DataException("Nebyl vracen žádny záznam z databaze");
                if (dbase.Tables[0].Rows.Count == 0) return null;   //throw new DataException("Chybí zaznam");
                return dbase;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new Exception("Chyba připojení do databaze: ");
            }
        }

    }
}
