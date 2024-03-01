using LibraryAplikace;
using System.Data;
using System.Diagnostics;
using XMLTabulka1;
using XMLTabulka1.API;
using XMLTabulka1.Trida;
using W = Microsoft.Office.Interop.Word;

namespace LibraryAplikace.Word
{
    public static class Word
    {
        /// <summary>
        /// volani dynamické knihovny c:\Users\Martin\OneDrive\Word\NewWord1\Podpora\
        /// práce D:\OneDrive\Word\NewWord1
        /// </summary>
        public static async void Doc(string cesta, string XML)
        {
            WordPodpora app = new();
            //bool Volba = await app.NovyDoc(cesta, XML);
            await app.NovyDoc(cesta, XML);
            return;
        }

        public enum Dokument
        { 
            Word,
            Exel,
            Autocad,
            Pdf
        }

        public static List<string> ExistujeSouborPriponou(string Cesta, Dokument dokument)
        {
            //kontrola navayných cesta a jiné umístění souboru v podsložkách
            List<string> SouboryList = new SouborApp().HledejZdaExistujeSoubor(Cesta);
            if (SouboryList == null || SouboryList.Count < 1) { return null; }
            List<string> result = [];
            string[] Pripona = null;
            switch (dokument)
            {
                case Dokument.Word:
                    Pripona = [".doc", ".docx"];

                    break;
                case Dokument.Exel:
                    Pripona = [".xls", ".xlsx"];
                    break;
                case Dokument.Autocad:
                    Pripona = [".dwg"];
                    break;
                case Dokument.Pdf:
                    Pripona = [".dwg"];
                    break;
                default:
                    break;
            }
            //najde soubory se stanovenou příponou.
            result = SouboryList.Where(x => Pripona.Any(ext => x.EndsWith(ext, StringComparison.OrdinalIgnoreCase))).ToList();
            return result;
        }

        public static void Zobrazit(string NazevProcesu)
        {
            // Importované konstanty a funkce z User32.dll
            const int SW_RESTORE = 9;

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            [System.Runtime.InteropServices.DllImport("user32.dll")]
            static extern bool SetForegroundWindow(IntPtr hWnd);

            //Process[] proce = Process.GetProcesses();
            Process[] processes = Process.GetProcessesByName(NazevProcesu);
            if (processes.Length > 0)
            {
                // Aktivace a zobrazení prvního nalezeného procesu s daným názvem
                IntPtr mainWindowHandle = processes[0].MainWindowHandle;
                ShowWindow(mainWindowHandle, SW_RESTORE);
                SetForegroundWindow(mainWindowHandle);
            }

        }

        /// <summary>
        /// Dokumenet neexistuje
        /// </summary>
        public static bool VytvoritDokumentDoc(TeZak teZak)
        {
            //neexistují data pro vytvoření dokumetu
            if (teZak == null) return false;

            //Kotrola existence dokumnetu
            if (File.Exists(teZak.PATH)) return false;

            //Otevření aplikace Word
            //var WordApp = WordPodpora.WordApp1();
            //WordApp.Visible = true;
            
            //kontrola existence adresaře a jeho vytvoření
            string Adresar = Path.GetDirectoryName(teZak.PATH);
            if(!Directory.Exists(Adresar))
                Directory.CreateDirectory(Adresar);
            string JmenoSouboru = teZak.FPC;
            
            //ZMĚNA PŘÍPONY
            if (teZak.EXT.Equals("doc", StringComparison.InvariantCultureIgnoreCase))
                teZak.PATH = Path.Combine(Adresar, JmenoSouboru + ".docx");

            //možná ješte jednou kontrola existence pokud došlo ke změně písmena

            if (SouborApp.KopieSablonyDoc(teZak.PATH) == false)
            {
                Console.WriteLine("kopie šablony uspěšně vytvořena");
            }

            WordPodpora app = new() { CelyRadek = teZak.ObjectToDataRow() };
            app.Prenos(teZak.PATH);
            return true;
        }


        public static async Task<bool> OtevriDokument(TeZak teZak)
        {
            await Task.Delay(1);

            //ZMĚNA NE UŽ BYL VYBRÁN SOUBOR
            //POKUD NENÍ ZMĚNA PŘÍPONY
            //if (!Path.GetExtension(teZak.PATH).Equals(".docx", StringComparison.InvariantCultureIgnoreCase))
            //{ 
            //    string Adresar = Path.GetDirectoryName(teZak.PATH);
            //    string JmenoSouboru = teZak.FPC;
            //    if (teZak.EXT.Equals("doc", StringComparison.InvariantCultureIgnoreCase))
            //        teZak.PATH = Path.Combine(Adresar, JmenoSouboru + ".docx");
            //}

            if (File.Exists(teZak.PATH))
            {
                //musí být provedena kontrola souborů
                var WordApp = WordPodpora.WordApp1();
                WordApp.Visible = true;

                foreach (W.Document item in WordApp.Documents)
                {
                    if (item.FullName == teZak.PATH)
                    {
                        item.Activate();
                        Zobrazit("WINWORD");
                        return true;
                    }
                }
                WordApp.Documents.Open(teZak.PATH);
                return true;
            }
            

            //soubor neexistuje
            //if (teZak == null) return false;
            ////WordPodpora app = new();

            //string Adresar = Path.GetDirectoryName(teZak.PATH);
            ////string Pripona = teZak.EXT;
            //string JmenoSouboru = teZak.FPC;

            //string cesta = string.Empty;
            ////ZMĚNA PŘÍPONY
            //if (teZak.EXT.Equals("doc", StringComparison.InvariantCultureIgnoreCase))
            //    cesta = Path.Combine(Adresar, JmenoSouboru + ".docx");

            //možná ješte jednou kontrola existence pokud došlo ke změně písmena

            //if (SouborApp.KopieSablonyDoc(cesta) == false)
            //{
            //    Console.WriteLine("kopie šablony uspěšně vytvořena");
            //}

            //app.CelyRadek  = teZak.ObjectToDataRow();
            //app.Prenos(cesta);
            return false;
        }

        public static string ZmenaDisku(string cesta)
        {
            if (ExistDisk(cesta))
                return cesta;
            //zmena disku vždy na C
            // Získání kořenového adresáře
            string kořenovýAdresář = Path.GetPathRoot(cesta);
            
            // Získání zbytku cesty bez kořenového adresáře
            string zbytekCesty = cesta[kořenovýAdresář.Length..];

            // Vytvoření nové cesty s novým diskovým písmenem
            return Path.Combine("C:", zbytekCesty);

        }

        /// <summary>
        /// True disk existuje
        /// </summary>
        public static bool ExistDisk(string cesta)
        {
            // Získání informací o všech discích
            DriveInfo[] disky = DriveInfo.GetDrives();

            // Získání diskového písmena ze zadané cesty
            char diskovePismeno = Path.GetPathRoot(cesta)[0];

            // Ověření existence disku s daným písmenem
            return disky.Any(disk => disk.Name[0] == diskovePismeno);
        }

    }
}
