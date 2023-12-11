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
            //proveden� instalace na zadanou cestu
            var zip = await Install.GetSearchAsync("zip.zip");
            if(zip.Count < 1)
            {
                MessageBox.Show($"Chyba hled�n� souboru v RestApi\nSoubor pravd�podobn� n�n� nahr�n");
                Akt.Close();
                Close();
                return;
            }
            string RandomFilename = zip.Last().StoredFileName ?? "";
            string Cesta = textBox1.Text;

            //m�lo by v�dy existovat
            if (!Directory.Exists(Cesta))
                Directory.CreateDirectory(Cesta);

            if (!await Install.Download(RandomFilename, Cesta))
            {
                MessageBox.Show($"Chyba p�i extrakci souboru: {RandomFilename}");
                Akt.Close();
                Close();
                return;
            }

            //na�ten� manifestu z restApi
            var Nova = await API.APIDownloadFile<ProgramInfo>($"api/file/manifest");
            Nova.SaveJson(Cesty.ManifestInstal);
            Akt.Close();

            //Spu�ten� programu TeZak
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
