using System.Diagnostics;
using System.Windows.Forms;
using XMLTabulka1;
using XMLTabulka1.API;
using XMLTabulka1.Trida;

namespace Instal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //textBox1.Text = Path.GetDirectoryName(Cesty.AdresarSpusteni);
            //textBox1.Text = Path.GetDirectoryName(Cesty.AppDataInstal);
            textBox1.Text = Cesty.AppDataTezak;
        }

        private async void Instalace_ClickAsync(object sender, EventArgs e)
        {
            var Akt = MenuInstal.Aktualizuj();
            //provedení instalace na zadanou cestu
            var zip = await Install.GetSearchAsync("zip.zip");
            if(zip.Count < 1)
            {
                MessageBox.Show($"Chyba hledání souboru v RestApi\nSoubor pravdìpodobnì nìní nahrán");
                Akt.Close();
                Close();
                return;
            }
            string RandomFilename = zip.Last().StoredFileName ?? "";
            string Cesta = textBox1.Text;

            //mìlo by vždy existovat
            if (!Directory.Exists(Cesta))
                Directory.CreateDirectory(Cesta);

            if (!await Install.Download(RandomFilename, Cesta))
            {
                MessageBox.Show($"Chyba pøi extrakci souboru: {RandomFilename}");
                Akt.Close();
                Close();
                return;
            }

            //naètení manifestu z restApi
            var Nova = await API.APIDownloadFile<ProgramInfo>($"api/file/manifest");
            Nova.SaveJson(Cesty.ManifestInstal);
            Akt.Close();

            //Spuštení programu TeZak
            //Cesta = @"D:\OneDrive\Databaze\Tezak\XMLTablulka1\WFForm\bin\Debug\net8.0-windows\WFForm.exe";
            //Cesta = Path.Combine( Cesty.AppData, "TeZak","WFForm.exe");
            //Cesta = Cesty.AppDataTezakWFForm;
            //Cesty.PodporaDataJson

            string soubor = "WFForm.exe";
            Cesta = Path.Combine(Cesty.AdresarSpusteni, soubor);

            if (File.Exists(Cesty.AppDataTezakWFForm))
                Soubor.StartAplikace(Cesty.AppDataTezakWFForm, "");

            //KOnec programu instalace
            Environment.Exit(0);
        }
    }
}
