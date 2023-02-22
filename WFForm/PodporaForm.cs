using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLTabulka1;
using XMLTabulka1.Trida;

namespace WFForm
{
    public partial class Form1 : Form
    {
        public void VypisMojeZakazky()
        {
            List<MojeZakazky> moje = new LibraryAplikace.Zakazky().MojeZakazkyList();
            listView1.Clear();
            listView1.View = System.Windows.Forms.View.Details;
            listView1.Columns.Add(Sloupec.C_PROJ);
            listView1.Columns.Add(Sloupec.NAZEV);
            listView1.Columns[1].Width = 250;

            foreach (var item in moje)
            {
                listView1.Items.Add(new ListViewItem(new string[] { item.CisloProjektu, item.ProjektNazev }));
            }
        }
    }

}
