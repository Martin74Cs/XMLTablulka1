using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using XMLTabulka1;
using XMLTabulka1.API;
using XMLTabulka1.Trida;

namespace WFForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Kontrola nov� verze programu p�es RestAPI
            Kontrola().Wait();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            //Application.Exit();
        }

        public static async Task Kontrola()
        {
            var aktualizuj = new Aktualizuj();
            //Kontrola nov� verze programu p�es RestAPI
            if (await aktualizuj.KontrolaVerze())
            {
                var menu = new Menu();
                var Vyber =  menu.Nov�Verze(aktualizuj.Aktulni);
                Vyber.Close();
                if (menu.Dialog == DialogResult.OK)
                { 
                    // P��klad �ten� hodnot
                    var configuration = LoadKonfigurace(".appsettings.json");
                    string CestaProInstal = configuration["CestaProInstal"];
                    if(File.Exists(CestaProInstal))
                        Soubor.StartAplikace(CestaProInstal);

                    //Soubor.StartAplikace("explorer.exe");
                    Environment.Exit(0);
                }
                //pokra�uje bez Aktualizace
            }
        }

        public static IConfigurationRoot LoadKonfigurace(string Cesta)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(Cesta, optional: true, reloadOnChange: true)
                .Build();

            return configuration;
        }
    }
}