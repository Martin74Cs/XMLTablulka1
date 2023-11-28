﻿
using System.Diagnostics;

namespace XMLTabulka1
{
    public static class Cesty
    {
        //Cesty k souborum Acad
        public static string Acad => Path.Combine(Podpora, "Acad"); // Podpora + @"\Acad";
        public static string Word => Path.Combine(Podpora, "Word");
        public static string SablonaDwg => Acad + @"\Sablona.dwg";

        //Cesty k podpůrným souborum
        public static string CislaProjektuTxt => Pomoc + @"\CislaProjektů.txt";
        public static string NazevProjektuTxt => Pomoc + @"\NazevProjektu.txt";
        public static string CislaDokumentuXML => Pomoc + @"\CislaDokumentu.xml";
        public static string CislaDokumentuJson => Pomoc + @"\CislaDokumentu.json";

        public static string JedenRadekXml => Pomoc + @"\JedenRadek.xml";
        public static string JedenRadekJson => Pomoc + @"\JedenRadek.json";

        public static string PodporaSpolecneCsv => Podpora + @"\SPOLECNE2.csv";
        public static string PodporaDataXml => Podpora + @"\data.xml";
        public static string PodporaDataJson => Podpora + @"\data.json";
        public static string Manifest => Podpora + @"\Manifest.json";       
        public static string AktualniAdresar => Environment.CurrentDirectory;

        //D:\OneDrive\Databaze\Tezak\XMLTablulka1\Database\bin\Debug\net6.0\Database.exe
        public static string SouborExe => Environment.ProcessPath.ToString();

        //D:\OneDrive\Databaze\Tezak\XMLTablulka1\Database\bin\Debug\net6.0\XMLTabulka1.dll
        //public static string SouborExe1 => System.Reflection.Assembly.GetExecutingAssembly().Location;
        //adresar spušteni
        public static string AdresarSpusteni => Path.GetDirectoryName(SouborExe); 

        public static string SouborDbf => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\..\"));
        public static string SouborTezakDbf => Path.GetFullPath(Path.Combine(SouborDbf, @"Tezak.dbf"));

        //#if DEBUG
        //public static string Pomoc => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\Pomoc"));

        //public static string Podpora => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\Podpora"));
        //public static string Podpora => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\Podpora"));
        //#else

        public static string AdresarDebugWFForm
        {
            get 
            {
                string cesta = Path.GetFullPath(Path.Combine(AdresarSpusteni, "..", "..", "..", "..", "WFForm","bin","Debug","net8.0-windows8.0"));
                return cesta;

                //if (Environment.MachineName == "KANCELAR")
                //    return @"c:\Users\Martin\OneDrive\Databaze\Tezak\XMLTablulka1\WFForm\bin\Debug\net8.0-windows8.0\";
                //else
                //   return @"d:\OneDrive\Databaze\Tezak\XMLTablulka1\WFForm\bin\Debug\net8.0-windows8.0\";
            }
        }

        public static string ZIP
        {
            get
            {
                var Cesta = Path.Combine(AdresarSpusteni, "ZIP", "Zip.zip");
                if (!Directory.Exists(Path.GetDirectoryName(Cesta)))
                    Directory.CreateDirectory(Path.GetDirectoryName(Cesta));
                return Cesta;
            }
        }


        public static string Pomoc  
        {
            get {
                string Cesta = Path.Combine(AdresarSpusteni, @"Pomoc");
                if (!Directory.Exists(Cesta))
                    Directory.CreateDirectory(Cesta);
                return Path.GetFullPath(Cesta);
            }
        }

        public static string Podpora
        {
            get {
                string Cesta = Path.Combine(AdresarSpusteni, @"Podpora");
                if (!Directory.Exists(Cesta))
                    Directory.CreateDirectory(Cesta);
                return Path.GetFullPath(Cesta);            }
        }


//#endif

        public static string SQL3DPlant => Pomoc + @"\SQL3DPlant.txt";


        //public static string SouborDbf { get => AdresarSpusteni + @"\Tezak.DBF"; }

        //public static string ZdrojDbf { get; set; } = @"\\encz04\am\env\Tezak.DBF";
        public static string ZdrojDbf { get; set; } = @"D:\OneDrive\Databaze\Tezak\Tezaknew2.DBF";

        //public static void Nastavit()
        //{
        //    var configuration = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json")
        //    .Build();
        //}
    }
}
