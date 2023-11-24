using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using XMLTabulka1;
using XMLTabulka1.API;
using XMLTabulka1.Trida;

namespace WFForm
{
    public class Aktualizuj
    {
        public ProgramInfo Nova { get; set; }

        /// <summary>
        /// Otevri umístění databaze pro vytvoření kopie
        /// </summary>
        /// <returns></returns>
        public static string OpenDataze()
        {
            //string cesta = string.Empty;
            OpenFileDialog dialog = new()
            {
                Title = "Vyper databázi Dbf",
                InitialDirectory = @"G:\env",
                Filter = "DB Files|*.dbf",
                FileName = "Tezak.dbf"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Cesty.ZdrojDbf = dialog.FileName;
            }
            return dialog.FileName;
        }

        /// <summary>
        /// Kontrola nove verze true - nova verze, false - nejaká chyba
        /// </summary>
        public async Task<bool> KontrolaVerze()
        {
            //načtení manifestu z restApi
            Nova = await API.APIDownloadFile<ProgramInfo>($"api/file/manifest");
            if (Nova == null) return false;

            //načtení z manifestu ze souboru
            ProgramInfo Aktulni = Soubor.LoadJson<ProgramInfo>(Cesty.Manifest);
            if (Aktulni == null)
            {
                Nova.SaveJson(Cesty.Manifest);
                return false;
            }

            var n = new Version(Nova.Version);
            var a = new Version(Aktulni.Version);

            // Porovnání verzí
            int result = n.CompareTo(a);

            if (result < 0)
            {
                //starší
                return false;
            }
            else if (result > 0)
            {
                //novejší
                Nova.SaveJson(Cesty.Manifest);
                return true;
            }
            else
            {
                //stejné
                return false;
            }
               

        }

    }

}
