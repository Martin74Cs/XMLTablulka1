using System.Diagnostics;
using System.Windows.Forms;
using XMLTabulka1;
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
            textBox1.Text = Path.GetDirectoryName(Cesty.AdresarSpusteni);
        }

        private async void Instalace_ClickAsync(object sender, EventArgs e)
        {
            var Akt = MenuInstal.Aktualizuj();
            //proveden� instalace na zadanou cestu
            var zip = await Install.GetSearchAsync("zip.zip");
            string RandomFilename = zip.Last().StoredFileName ?? "";
            string Cesta = textBox1.Text;

            //m�lo by v�dy existovat
            if (!Directory.Exists(Cesta))
                Directory.CreateDirectory(Cesta);

            await Install.Download(RandomFilename, Cesta);
            Akt.Close();
           
#if DEBUG
            //Spu�ten� programu TeZak
            Cesta = @"D:\OneDrive\Databaze\Tezak\XMLTablulka1\WFForm\bin\Debug\net8.0-windows\WFForm.exe";
#else
            string soubor = "WFForm.exe";
            Cesta = Path.Combine(Cesty.AdresarSpusteni, soubor);
#endif
            if (File.Exists(Cesta))
                Soubor.StartAplikace(Cesta, "");

            //KOnec programu instalace
            Environment.Exit(0);
        }
    }
}
