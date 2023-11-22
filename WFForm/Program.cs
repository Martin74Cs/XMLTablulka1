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
            Kontrola();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            //Application.Exit();
        }

        public async static void Kontrola()
        {
            //Kontrola nov� verze programu p�es RestAPI
            if (await Aktualizuj.Nov�Verze())
            {
                var nova = Menu.NovaVerze();
                if(nova = nova.bu)
                var configuration = LoadKonfigurace();
                // P��klad �ten� hodnot
                string CestaProInstal = configuration["CestaProInstal"];
                if(File.Exists(CestaProInstal))
                    Soubor.StartAplikace(CestaProInstal);
                //Soubor.StartAplikace("explorer.exe");
                Environment.Exit(0);
            }
        }



        public static IConfigurationRoot LoadKonfigurace()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(".appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            return configuration;
        }
    }
}