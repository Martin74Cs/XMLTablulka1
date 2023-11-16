using LibraryAplikace;
using System.Data;
using XMLTabulka1.API;
using XMLTabulka1.Trida;
using W = Microsoft.Office.Interop.Word;
namespace XMLTabulka1.Word
{
    public class Word
    {
        /// <summary>
        /// volani dynamické knihovny c:\Users\Martin\OneDrive\Word\NewWord1\Podpora\
        /// práce D:\OneDrive\Word\NewWord1
        /// </summary>
        /// <param name="cesta">cesta kde umístěn soubor</param>
        /// <param name="XML">cesta k XML záznam z databaze</param>
        public static async void Doc(string cesta, string XML)
        {
            WordPodpora app = new();
            bool Volba = await app.NovyDoc(cesta, XML);
            return;
        }

        public static void Doc(TeZak teZak)
        {
            List<string> SouboryList = new SouborApp().HledejZdaExistujeSoubor(teZak.PATH);
            //hledej souborz doc a docx.
            string[] cestas = SouboryList.Where(x => Path.GetExtension(x).ToLowerInvariant() == "doc" || Path.GetExtension(x).ToLowerInvariant() == "docx").ToArray();
            //kontrola navayných cesta a jiné umístění souboru v podsložkách
            if (File.Exists(cestas.First()))
            {
                //musí být provedena kontrola souborů
                var WordApp = WordPodpora.WordApp1();
                foreach (W.Document item in WordApp.Documents)
                {
                    if (item.FullName == teZak.PATH)
                    {
                        item.Activate();
                        return;
                    }
                }
                WordApp.Documents.Open(teZak.PATH);
            }
            else 
            {
                //soubor neexistuje
                if (teZak == null) return;
                WordPodpora app = new();

                string Adresar = Path.GetDirectoryName(teZak.PATH);
                string Pripona = teZak.EXT;
                string JmenoSouboru = teZak.EXT;
                string cesta = string.Empty;
                //Soubor neexistuje
                if (Pripona.ToLowerInvariant() == "doc")
                    //ZMĚNA PŘÍPONY
                    cesta = Path.Combine(Adresar, JmenoSouboru + ".docx");
                if (SouborApp.KopieDoc(cesta) == false)
                {
                    Console.WriteLine("kopie šablony uspěšně vytvořena");
                }

                app.CelyRadek  = teZak.ObjectToDataRow();
                app.Prenos(cesta);

            }

        }
    }
}
