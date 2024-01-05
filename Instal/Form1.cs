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
            var zip = await Install.Install.GetSearchAsync("TeZak.zip");
            //var zip = await Install.GetSearchAsync("instal.zip");
            if(zip.Count < 1)
            {
                MessageBox.Show($"Chyba hled�n� souboru v RestApi\nSoubor pravdepodobne nen� nahr�n");
                Akt.Close();
                Close();
                return;
            }

            string RandomFilename = zip.Last().StoredFileName ?? "";
            string Cesta = textBox1.Text;

            //m�lo by v�dy existovat
            if (!Directory.Exists(Cesta))
            { 
                Directory.CreateDirectory(Cesta);
                //Directory.CreateDirectory(Cesty.Podpora);
                //Directory.CreateDirectory(Cesty.Acad);
                //Directory.CreateDirectory(Cesty.Word);
            }

            if (!await Install.Install.Download(RandomFilename, Cesta))
            {
                MessageBox.Show($"Chyba p�i extrakci souboru: {zip.Last().FileName}");
                Akt.Close();
                Close();
                return;
            }

            //na�ten� manifestu z restApi
            var Nova = await API.APIDownloadFile<ProgramInfo>($"api/file/manifest");
            //ulo�en� manifestu
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
