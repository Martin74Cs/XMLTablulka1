
using System.Diagnostics;

namespace XMLTabulka1
{
    public static class Cesty
    {
        //Cesty k souborum Acad
        public static string Acad => Path.Combine(Podpora, "Acad"); // Podpora + @"\Acad";
        public static string Word => Path.Combine(Podpora, "Word");
        public static string SablonaDwg => Path.Combine(Acad, @"Sablona.dwg");

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

        public static string AdresarSpusteniNarazen => Path.GetFullPath(Path.Combine(AdresarSpusteni, ".."));

        //public static string ManifestInstal => Path.Combine(AdresarSpusteniNarazen, "Podpora", @"Manifest.json");
        public static string ManifestInstal => Path.Combine(Podpora, @"Manifest.json");
        public static string AktualniAdresar => Environment.CurrentDirectory;

        public static string AppData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string AppDataTezak => Path.Combine(AppData, "TeZak");

        public static string AppDataInstal { get {
                string AppDataInstal = Path.Combine(AppData, "Instal");
                if (!Directory.Exists(AppDataInstal))
                    Directory.CreateDirectory(AppDataInstal);
                return AppDataInstal; } }

        public static string AppDataTezakWFForm => Path.Combine(AppDataTezak, "WFForm.exe");

        //D:\OneDrive\Databaze\Tezak\XMLTablulka1\Database\bin\Debug\net6.0\Database.exe
        public static string SouborExe => Environment.ProcessPath.ToString();

        //D:\OneDrive\Databaze\Tezak\XMLTablulka1\Database\bin\Debug\net6.0\XMLTabulka1.dll
        //public static string SouborExe1 => System.Reflection.Assembly.GetExecutingAssembly().Location;
        //adresar spušteni
        public static string AdresarSpusteni => Path.GetDirectoryName(SouborExe); 

        //public static string SouborDbf => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\..\..\"));
        public static string SouborDbf => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\"));
        public static string SouborTezakDbf => Path.GetFullPath(Path.Combine(SouborDbf, @"Tezak.dbf"));

        public static string AdresarTezakDbfSitováCesta = @"U:\Elektro\mcsato\Databaze";

        public static string Upload => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\..\"));

        public static string Priprava
        {
            get
            {
                var Cesta = Path.Combine(Upload, "Priprava");
                //string Priprava = @"c:\Users\Martin\OneDriveKopie\Databaze\Tezak\";
                //var Cesta = Path.Combine(Priprava, "Priprava");
                if (!Directory.Exists(Cesta))
                    Directory.CreateDirectory(Cesta);
                //Directory.CreateDirectory(Path.GetDirectoryName(Cesta));
                return Cesta;
            }
        }

        public static string PripravaSetup
        {
            get
            {
                string Cesta = Path.Combine(Priprava, "Setup");
                if (!Directory.Exists(Cesta))
                    Directory.CreateDirectory(Cesta);
                //Directory.CreateDirectory(Path.GetDirectoryName(Cesta));
                return Cesta;
            }
        }

        public static string PripravaTeZak
        {
            get
            {
                string Cesta = Path.Combine(Priprava, "TeZak");
                if (!Directory.Exists(Cesta))
                    Directory.CreateDirectory(Cesta);
                    //Directory.CreateDirectory(Path.GetDirectoryName(Cesta));
                return Cesta;
            }
        }
        public static string PripravaTeZakAll
        {
            get
            {
                string Cesta = Path.Combine(Priprava, "TeZakAll");
                if (!Directory.Exists(Cesta))
                    Directory.CreateDirectory(Cesta);
                //Directory.CreateDirectory(Path.GetDirectoryName(Cesta));
                return Cesta;
            }
        }

        public static string PripravaTeZakInstal
        {
            get
            {
                //string Priprava = @"c:\Users\Martin\OneDriveKopie\Databaze\Tezak\";
                var Cesta = Path.Combine(PripravaTeZakAll, "Instal");
                //if (!Directory.Exists(Path.GetDirectoryName(Cesta)))
                if (!Directory.Exists(Cesta))
                    Directory.CreateDirectory(Cesta);
                return Cesta;
            }
        }

        //#if DEBUG
        //public static string Pomoc => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\Pomoc"));

        //public static string Podpora => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\Podpora"));
        //public static string Podpora => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\Podpora"));
        //#else
        public static string AdresarInstalPrvni
        {
            get
            {
                string cesta = Path.GetFullPath(Path.Combine(AdresarSpusteni, "..", "..", "..", "..", "InstalPrvni", "bin", "Debug", "net8.0-windows8.0"));
                return cesta;
            }
        }

        public static string AdresarDebugInstal
        {
            get
            {
                string cesta = Path.GetFullPath(Path.Combine(AdresarSpusteni, "..", "..", "..", "..", "Instal", "bin", "Debug", "net8.0-windows8.0"));
                return cesta;

                //if (Environment.MachineName == "KANCELAR")
                //    return @"c:\Users\Martin\OneDrive\Databaze\Tezak\XMLTablulka1\WFForm\bin\Debug\net8.0-windows8.0\";
                //else
                //   return @"d:\OneDrive\Databaze\Tezak\XMLTablulka1\WFForm\bin\Debug\net8.0-windows8.0\";
            }
        }

        public static string AdresarDebugWFForm
        {
            get
                {//d:\OneDrive\Databaze\Tezak\XMLTablulka1\Upload\bin\x64\Debug\net8.0-windows8.0\
                //string cesta = Path.GetFullPath(Path.Combine(AdresarSpusteni, "..", "..", "..", "..", "..", "WFForm","bin", "x64", "Debug","net8.0-windows8.0"));
                string cesta = Path.GetFullPath(Path.Combine(AdresarSpusteni, "..", "..", "..", "..", "WFForm", "bin", "Debug", "net8.0-windows8.0"));
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
                var Cesta = Path.Combine(Priprava, "ZIP");
                if (!Directory.Exists(Cesta))
                    Directory.CreateDirectory(Cesta);
                return Cesta;
            }
        }

        public static string InstalZIP => Path.Combine(ZIP, "Instal.zip");

        public static string InstalTeZakAll => Path.Combine(ZIP, "TeZakAll.zip");
        public static string InstalTeZak => Path.Combine(ZIP, "TeZak.zip");

        public static string InstalExe => Path.Combine(ZIP, "Instal.exe");

        public static string ZIPProgram => Path.Combine(ZIP, "Program.zip");

        public static string SevenZIP => Path.Combine(ZIP, "Zip.7z");

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
                string Cesta = Path.Combine(AppDataTezak, @"Podpora");
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
