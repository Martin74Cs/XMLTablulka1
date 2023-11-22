using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLTabulka1;
using XMLTabulka1.Trida;

namespace WFForm
{
    public static class Rozšíření
    {
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

    }
}
