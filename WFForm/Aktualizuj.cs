using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using XMLTabulka1;
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
            string cesta = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog
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


        public static Form Text()
        {
            System.Windows.Forms.ProgressBar bar = new()
            {
                Location = new Point(13, 32),
                Name = "Progress1",
                Size = new Size(300, 25),
                Style = ProgressBarStyle.Marquee,
            };

            System.Windows.Forms.Label label = new()
            {
                Location = new Point(97, 9),
                Name = "Label1",
                Size = new Size(123, 20),
                Text = "Aktualizuje se....",
            };

            Form form = new Form()
            {
                AutoScaleDimensions = new System.Drawing.SizeF(6, 13),
                AutoScaleMode = AutoScaleMode.Font,
                BackgroundImageLayout = ImageLayout.Center,
                ClientSize = new System.Drawing.Size(350, 70),
                FormBorderStyle = FormBorderStyle.FixedSingle,
                StartPosition = FormStartPosition.CenterScreen,
            };
            form.Controls.Add(bar);
            form.Controls.Add(label);
            //form.ShowDialog();

            bar.Value = bar.Maximum;
            return form;
        }

        //podpora form opakuje se
        /// <summary>
        /// Do listView Vypíše seznam moje zakazky.
        /// </summary>
        public static void VypisMojeZakazky(this ListView listView1)
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
