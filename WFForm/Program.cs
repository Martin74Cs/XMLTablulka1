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
            //Kontrola nové verze programu pøes RestAPI
            Kontrola();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            //Application.Exit();
        }

        public async static void Kontrola()
        {
            //Kontrola nové verze programu pøes RestAPI
            if (await Aktualizuj.NováVerze())
            {
                Soubor.StartAplikace("explorer.exe");
                Environment.Exit(0);
            }
        }
    }
}