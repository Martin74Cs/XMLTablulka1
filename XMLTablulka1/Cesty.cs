using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTabulka1
{
    public static class Cesty
    {
        //jedna se o cestu k XMLTablulka.dll

        public static string CislaProjektuTxt => Pomoc + @"\Cisla_projektů.txt";
        public static string NazevProjektuTxt => Pomoc + @"\Nazev_projektu.txt";
        public static string CislaDokumentu => Pomoc + @"\CislaDokumentu.txt";
        
        public static string JedenRadekXml => Pomoc + @"\JedenRadek.xml";
        public static string JedenRadekJson => Pomoc + @"\JedenRadek.json";

        public static string PodporaSpolecneCsv => Podpora + @"\SPOLECNE2.csv";
        public static string PodporaDataXml => Podpora + @"\data.xml";
        public static string PodporaDataJson => Podpora + @"\data.json";
        public static string AktualniAdresar => Environment.CurrentDirectory;
        
        public static string SouborExe => System.Reflection.Assembly.GetExecutingAssembly().Location;

        //adresar spušteni
        public static string AdresarSpusteni => Path.GetDirectoryName(SouborExe); 

        public static string SouborDbf => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\..\"));

        public static string Pomoc => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\Pomoc"));

        public static string Podpora => Path.GetFullPath(Path.Combine(AdresarSpusteni, @"..\..\..\..\Podpora"));


        //public static string SouborDbf { get => AdresarSpusteni + @"\Tezak.DBF"; }

        //public static string ZdrojDbf { get; set; } = @"\\encz04\am\env\Tezak.DBF";
        public static string ZdrojDbf { get; set; } = @"D:\OneDrive\Databaze\Tezak\Tezaknew2.DBF";
    }
}
