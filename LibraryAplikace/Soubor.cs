
using XMLTabulka1;

namespace LibraryAplikace
{
    public class SouborApp
    {
        public string[] Existuje(string Cesta)
        {
            List<string> Soubor = [];
            if (File.Exists(Cesta))
            { 
                Soubor.Add(Cesta);
                //return Soubor.ToArray();
                return [.. Soubor];
            }
            List<string> Pole = HledejZdaExistujeSoubor(Cesta);
            Soubor.AddRange(Pole);
            //return Soubor.ToArray();
            return [.. Soubor];
        }

        /// <summary>
        /// Nový verze hledej soubor
        /// </summary>
        public List<string> HledejZdaExistujeSoubor(string JmenoSouboru)
        {
            List<string> listSouboru = [];

            var Soubor = Path.GetFileNameWithoutExtension(JmenoSouboru);
            int Delka = 6;
            if (Soubor.Length <= 6)
                Delka = Soubor.Length;
            var Soubor6 = Soubor[..Delka].ToUpper();

            string AktualniAdresar = Path.GetDirectoryName(JmenoSouboru);
            if (Directory.Exists(AktualniAdresar))
            {
                return Procházenísouboru(Soubor6, AktualniAdresar);
            }
            return [];
        }

        public List<string> Procházenísouboru(string Hledane, string JmenoSouboru)
        {
            List<string> listSouboru = [];
            //Procházení souboru

            foreach (var SouborJedna in Directory.GetFileSystemEntries(JmenoSouboru))
            {
                if (Directory.Exists(SouborJedna))
                {
                    List<string> listpomc = Procházenísouboru(Hledane, SouborJedna);
                    listSouboru.AddRange(listpomc);
                }
                else
                {
                    string test = Path.GetFileNameWithoutExtension(SouborJedna).ToUpper();
                    int Delka = 6;
                    if (test.Length > 6)
                        test = test[..Delka];
                    if (test == Hledane)
                        listSouboru.Add(SouborJedna);
                }
            }
            
            return listSouboru;
        }


            public List<string> HledejZdaExistujeSouborOld(string JmenoSouboru)
        {
            List<string> listSouboru = [];
            var AdresarJedna = new FileInfo(JmenoSouboru).DirectoryName;
            if (Directory.Exists(AdresarJedna) == false)
                return [];
                
            var Soubor = Path.GetFileNameWithoutExtension(JmenoSouboru);
            int Delka;
            if (Soubor.Length >= 6) 
                Delka = 6;
            else
                Delka = Soubor.Length;
            var Soubor6 = Soubor[..Delka].ToUpper();

            //Open AI Hledáme soubory v aktuálním adresáři a jeho podadresářích
            //string[] files = Directory.GetFiles(AdresarJedna, fileName, SearchOption.AllDirectories);

            //Open AI Hledáme soubory v aktuálním adresáři a jeho podadresářích s použitím regulárního výrazu
            //string[] files = Directory.GetFiles(AdresarJedna)
            //.Where(filePath => Regex.IsMatch(Path.GetFileNameWithoutExtension(filePath), Soubor6, RegexOptions.IgnoreCase))
            //.ToArray();

            //Procházení souboru
            foreach (var SouborJedna in Directory.GetFiles(AdresarJedna))
            {
                string test = Path.GetFileNameWithoutExtension(SouborJedna).ToUpper();
                //if(test.Length>6) test = test.Substring(0, Delka);
                if (test.Length > 6) test = test[..Delka];
                if (test == Soubor6)
                {
                    listSouboru.Add(SouborJedna);
                }
            }

            //Procházení adresářů
            var Adresar = Directory.GetDirectories(AdresarJedna);
            foreach ( var item in Adresar) 
            {
                List<string> Pom = ProjdiSoubory(item, Delka, Soubor6, Adresar, listSouboru);
                if (Pom.Count < 1)
                    break;
                else
                { 
                    listSouboru.AddRange(Pom);
                }
            }
            return listSouboru;
        }

        public static List<string> ProjdiSoubory(string Adresar, int Delka, string Soubor6, string[] Pole, List<string> PoleSouboru)
        {
            foreach (var item in Pole)
            {
                var pom = ProjdiSoubory(item, Delka, Soubor6, Directory.GetDirectories(item), PoleSouboru);
                if (pom.Count <= 0)
                    break;
            }

            //projdi soubory v daném adresáři a hldej zadaný soubor a přidej ho do seznamu
            foreach (var Soubor in Directory.GetFiles(Adresar))
            {
                string pom = Path.GetFileNameWithoutExtension(Soubor).ToUpper();
                //if (pom.Length > 6) pom = pom.Substring(0, Delka);
                if (pom.Length > 6) pom = pom[..Delka];
                if (pom == Soubor6)
                    PoleSouboru.Add(Soubor);
            }
            return PoleSouboru;
        }

        //public static void PriladyPraceSeSouboryAresari()
        //{
        //    string Cesta = "";
            
        //    //Ve Adresár je aktuální adresář z cesty pokus
        //    //string? Adresar = new FileInfo(Cesta).DirectoryName;
        //    //Adresar = Path.GetDirectoryName(Cesta);

        //    //V disk je název disku např G:\ z cesty pokus
        //    DirectoryInfo disk = new DirectoryInfo(Cesta).Root;

        //    //SEZNAM SÍTOVÝCH JEDNOTEK IO.DriveInfo.GetDrives()
        //    DriveInfo[] drive = DriveInfo.GetDrives();
        //    foreach (var driveItem in drive) 
        //    {
        //        Console.WriteLine(driveItem.Name);
        //    }

        //    //ZJIŠTĚNÍ DISKU G: KTERÝ NEEXISTUJE TRVÁ DLOUHO -PROHLÉM ASI WINDOWS
        //    if (Directory.Exists(drive[0].Name))
        //    {
        //        Console.WriteLine($"Přístup na disk {disk.Name} - Neexistuje \n Bude použit disk D:");
        //    }

        //    //Záměna části řetezce na jinou hodnotu 
        //    string Asus = Cesta.Replace("Měneno", "Změna");

        //    //Uprava pro "Notebook_ASUS"
        //    string JmenoPočítače = System.Net.Dns.GetHostName();

        //    //Název souboru bez přípony
        //    string JmenoSouboru = Path.GetFileNameWithoutExtension(Cesta);

        //    //Název přípony
        //    string pripona1 = Path.GetExtension(Cesta);
        //}


        /// <summary>
        /// Kopie šablony
        /// </summary>
        public static bool KopieSablonyDoc(string CestaCil)
        {
            try
            {
                //WordPodpora word = new();
                //string CestaZdroj = word.CestaAdresar() + "\\Sablony\\titlist1.doc";

                string CestaZdroj = Path.Combine(Cesty.Word, "VZOR.docx");

                //ověření existence šablony 
                if (!File.Exists(CestaZdroj)) return false;

                if (System.IO.File.Exists(CestaCil))
                {
                    //Console.WriteLine("\nCesta : " + CestaCil);
                    //Console.WriteLine("\nSoubor existuje: smazat [y/n]");
                    //ConsoleKey klavesa = Console.ReadKey(false).Key;

                    //if (klavesa == ConsoleKey.Y || klavesa == ConsoleKey.A)
                    //{
                        System.IO.File.Delete(CestaCil);
                        //Console.WriteLine("Soubor {0} byl smazan.", CestaCil);
                    //}
                    //else
                        return false;
                }
                else
                { 
                    //kontorla existence adrsáře
                    //Directory.CreateDirectory(Path.GetDirectoryName(CestaCil)??"");
                    Directory.CreateDirectory(Path.GetDirectoryName(CestaCil));

                    //kopirování souborů s přepsnáním;
                    System.IO.File.Copy(CestaZdroj, CestaCil, true);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

    }
}
