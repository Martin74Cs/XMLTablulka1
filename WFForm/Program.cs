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
        static async void Main()
        {
            //Kontrola nov� verze programu p�es RestAPI
            if (await Aktualizuj.Nov�Verze())
            {
                Soubor.StartAplikace("explorer.exe");     
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            //Application.Exit();
        }
    }
}