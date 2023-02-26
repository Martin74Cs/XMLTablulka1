using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLTabulka1;

namespace LibraryAplikace
{
    public class Soubor
    {
        public string[] Existuje(string Cesta)
        {
            List<string> Soubor = new();
            if (File.Exists(Cesta))
            { 
                Soubor.Add(Cesta);
                return Soubor.ToArray();
            }
            string[] Pole = HledejZdaExistujeSoubor(Cesta);
            Soubor.AddRange(Pole);
            return Soubor.ToArray();
        }

        public string[] HledejZdaExistujeSoubor(string JmenoSouboru)
        {
            List<string> list = new List<string>();
            var AdresarJedna = new FileInfo(JmenoSouboru).DirectoryName;
            if (Directory.Exists(AdresarJedna) == false)
            {
                return list.ToArray();
            }
                
            var Soubor = Path.GetFileNameWithoutExtension(JmenoSouboru);
            int Delka;
            if (Soubor.Length >= 6) 
                Delka = 6;
            else
                Delka = Soubor.Length;
            var Soubor6 = Soubor.Substring(0, Delka).ToUpper();

            //Procházení souboru
            foreach (var SouborJedna in Directory.GetFiles(AdresarJedna))
            {
                if (Path.GetFileNameWithoutExtension(SouborJedna).Substring(0, Delka).ToUpper() == Soubor6)
                {
                    list.Add(SouborJedna);
                    list.ToArray();
                }
            }

            //Procházení adresářů
            var Adresar = Directory.GetDirectories(AdresarJedna);
            foreach ( var item in Adresar) 
            {
                string[] Pom = ProjdiSoubory(item, Delka, Soubor6, Adresar);
                if (Pom.Count() < 1)
                    break;
                else
                    list.AddRange(Pom);
            }
            return list.ToArray();
        }

        public string[] ProjdiSoubory(string Adresar, int Delka, string Soubor6, string[] Pole)
        {
            foreach (var item in Pole)
            {
                var pom = ProjdiSoubory(item, Delka, Soubor6, Pole);
                if (pom.Count() <= 0)
                    break;
            }

            foreach (var Soubor in Directory.GetFiles(Adresar))
            {
                if (Path.GetFileNameWithoutExtension(Soubor).Substring(0, Delka).ToUpper() == Soubor6)
                { 
                    Pole.ToList().Add(Soubor);
                    Pole.ToArray();
                }
            }
            return Pole;
        }

        public void PriladyPraceSeSouboryAresari()
        {
            string Cesta = "";
            
            //Ve Adresár je aktuální adresář z cesty pokus
            string? Adresar = new FileInfo(Cesta).DirectoryName;
            Adresar = Path.GetDirectoryName(Cesta);

            //V disk je název disku např G:\ z cesty pokus
            DirectoryInfo disk = new DirectoryInfo(Cesta).Root;

            //SEZNAM SÍTOVÝCH JEDNOTEK IO.DriveInfo.GetDrives()
            DriveInfo[] drive = DriveInfo.GetDrives();
            foreach (var driveItem in drive) 
            {
                Console.WriteLine(driveItem.Name);
            }

            //ZJIŠTĚNÍ DISKU G: KTERÝ NEEXISTUJE TRVÁ DLOUHO -PROHLÉM ASI WINDOWS
            if (Directory.Exists(drive[0].Name))
            {
                Console.WriteLine($"Přístup na disk {disk.Name} - Neexistuje \n Bude použit disk D:");
            }

            //Záměna části řetezce na jinou hodnotu 
            string Asus = Cesta.Replace("Měneno", "Změna");

            //Uprava pro "Notebook_ASUS"
            string JmenoPočítače = System.Net.Dns.GetHostName();

            //Název souboru bez přípony
            string JmenoSouboru = Path.GetFileNameWithoutExtension(Cesta);

            //Název přípony
            string pripona1 = Path.GetExtension(Cesta);
        }

    }
}
