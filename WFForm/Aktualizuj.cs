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
    public static class Aktualizuj
    {
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


        //podpora form opakuje se
        /// <summary>
        /// Do listView Vypíše seznam moje zakazky.
        /// </summary>
        public static async void VypisMojeZakazky(this ListView listView1, List<MojeZakazky> VlastniZakazky)
        {
            await Task.Delay(1);
            //List<MojeZakazky> moje = LibraryAplikace.Zakazky.MojeZakazkyList();
            listView1.Clear();
            listView1.View = System.Windows.Forms.View.Details;
            listView1.Columns.Add(Sloupec.C_PROJ);
            listView1.Columns.Add(Sloupec.NAZEV);
            listView1.Columns[1].Width = 250;

            foreach (var item in VlastniZakazky)
            {
                listView1.Items.Add(new ListViewItem(new string[] { item.CisloProjektu, item.ProjektNazev }));
            }
        }

        /// <summary>
        /// Kontrola nove verze true - nova verze, false - nejaká chyba
        /// </summary>
        public static async Task<bool> NováVerze()
        {
            //načtení manifestu z restApi
            ProgramInfo Nova = await API.APIDownloadFile<ProgramInfo>($"api/file/manifest");
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
