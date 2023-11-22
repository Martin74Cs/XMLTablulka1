using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using XMLTabulka1.Trida;

namespace WFForm
{
    public class Menu
    {
        public DialogResult Dialog { get; set; }
        public Form Okno { get; set; }
        public static Form Aktualizuj()
        {
            System.Windows.Forms.ProgressBar bar = new()
            {
                Location = new Point(13, 32),
                Name = "Progress1",
                Font = new Font("Segoe UI", 12F),
                Size = new Size(300, 25),
                Style = ProgressBarStyle.Marquee,
            };

            System.Windows.Forms.Label label = new()
            {
                Location = new Point(97, 9),
                Name = "Label1",
                Font = new Font("Segoe UI", 12F),
                Size = new Size(123, 20),
                Text = "Aktualizuje se....",
            };

            Form form = new()
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

            form.Visible = true;
            form.Show();
            form.TopMost = true;

            return form;
        }
        public Form NováVerze(ProgramInfo info)
        {
            System.Windows.Forms.Label Label = new()
            {
                Location = new Point(13, 60),
                Name = "Aktualizace",
                Font = new Font("Segoe UI", 12F),
                Text = "Byla vydána nová verze " + info.Version.ToString() + "\n" + "Ze dne " + info.ReleaseDate.ToString(),
                Size = new Size(300, 100),
            };

            System.Windows.Forms.Button Aktualizuj = new()
            {
                Location = new Point(50, 15),
                Font = new Font("Segoe UI", 12F),
                Name = "Aktualizuj",
                Size = new Size(100, 30),
                Text = "Aktualizuj",
            };
            Aktualizuj.Click += Aktualizuj_klik;

            System.Windows.Forms.Button Přeskočit = new()
            {
                Location = new Point(150, 15),
                Font = new Font("Segoe UI", 12F),
                Name = "Přeskočit",
                Size = new Size(100, 30),
                Text = "Přeskočit",
            };
            Přeskočit.Click += Přeskočit_klik;

            Form form = new()
            {
                AutoScaleDimensions = new System.Drawing.SizeF(6, 13),
                AutoScaleMode = AutoScaleMode.Font,
                BackgroundImageLayout = ImageLayout.Center,
                ClientSize = new System.Drawing.Size(300, 150),
                FormBorderStyle = FormBorderStyle.FixedSingle,
                StartPosition = FormStartPosition.CenterScreen,
            };
            form.Controls.Add(Label);
            form.Controls.Add(Aktualizuj);
            form.Controls.Add(Přeskočit);

            //form.ShowDialog();

            //form.Visible = true;
            //form.TopMost = true;
            form.ShowDialog();
            return form;
        }

        public void Aktualizuj_klik(object sender, EventArgs e)
        {
            Console.WriteLine("Tlačítko bylo stisknuto!");
            Dialog = DialogResult.OK;
        }

        public void Přeskočit_klik(object sender, EventArgs e)
        {
            Console.WriteLine("Tlačítko bylo stisknuto!");
            Dialog = DialogResult.Cancel;
        }

    }
}
