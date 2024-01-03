using Instal;
using XMLTabulka1.API;
using XMLTabulka1.Trida;
using XMLTabulka1;

namespace InstalPrvni
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var Akt = MenuInstal.Aktualizuj();
            //provedení instalace na zadanou cestu
            var zip = await Install.GetSearchAsync("Instal.zip");
            if (zip.Count < 1)
            {
                MessageBox.Show($"Chyba hledání souboru v RestApi\nSoubor pravděpodobně není nahrán");
                Akt.Close();
                Close();
                return;
            }

            string RandomFilename = zip.Last().StoredFileName ?? "";
            string Cesta = Cesty.AppDataTezak;
            //mìlo by vždy existovat
            if (!Directory.Exists(Cesta))
            {
                Directory.CreateDirectory(Cesta);
                //Directory.CreateDirectory(Cesty.Podpora);
                //Directory.CreateDirectory(Cesty.Acad);
                //Directory.CreateDirectory(Cesty.Word);
            }

            if (!await Install.Download(RandomFilename, Cesta))
            {
                MessageBox.Show($"Chyba pøi extrakci souboru: {zip.Last().FileName}");
                Akt.Close();
                Close();
                return;
            }

            //naètení manifestu z restApi
            var Nova = await API.APIDownloadFile<ProgramInfo>($"api/file/manifest");
            //uložení manifestu
            Nova.SaveJson(Cesty.ManifestInstal);
            Akt.Close();

            //Spuštení programu TeZak
            //Cesta = @"D:\OneDrive\Databaze\Tezak\XMLTablulka1\WFForm\bin\Debug\net8.0-windows\WFForm.exe";
            //Cesta = Path.Combine( Cesty.AppData, "TeZak","WFForm.exe");
            //Cesta = Cesty.AppDataTezakWFForm;
            //Cesty.PodporaDataJson

            string soubor = "WFForm.exe";
            Cesta = Path.Combine(Cesta, soubor);

            if (File.Exists(Cesta))
                Soubor.StartAplikace(Cesta, "");

            //KOnec programu instalace
            Environment.Exit(0);
        }
    }
}
