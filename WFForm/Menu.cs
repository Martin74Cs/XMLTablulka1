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
    public partial class Menu
    {
        public DialogResult Dialog { get; set; }
        public Form Okno { get; set; }
        public string Tlacitko { get; set; } = string.Empty;
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
            bar.Value = bar.Maximum;
            form.Visible = true;
            form.Show();
            form.TopMost = true;
            return form;
        }
    }

    public partial class Menu
    {
        public Form NováVerze(ProgramInfo info)
        {
            System.Windows.Forms.Label Label = new()
            {
                Location = new Point(13, 60),
                Name = "Aktualizace",
                Font = new Font("Segoe UI", 12F),
                Text = "Byla vydána nová verze " + info.Version.ToString() + "\n" +
                "Ze dne " + info.ReleaseDate.ToString("dd.MM.yyyy") + "\n" +
                "v " + info.ReleaseDate.ToString("HH:mm:ss"),
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

            System.Windows.Forms.Button Přeskočit = new()
            {
                Location = new Point(150, 15),
                Font = new Font("Segoe UI", 12F),
                Name = "Přeskočit",
                Size = new Size(100, 30),
                Text = "Přeskočit",
            };

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

            Aktualizuj.Click += Aktualizuj_klik;
            Přeskočit.Click += Přeskočit_klik;

            //form.Visible = true;
            //form.TopMost = true;
            Okno = form;
            form.ShowDialog();
            return form;
        }

        public void Aktualizuj_klik(object sender, EventArgs e)
        {
            Console.WriteLine("Tlačítko bylo stisknuto!");
            Dialog = DialogResult.OK;
            Okno.Close();
        }

        public void Přeskočit_klik(object sender, EventArgs e)
        {
            Console.WriteLine("Tlačítko bylo stisknuto!");
            Dialog = DialogResult.Cancel;
            Okno.Close();
        }

    }

    public partial class Menu
    {
        public Form Instalace(ProgramInfo info)
        {
            Form form = new()
            {
                AutoScaleDimensions = new SizeF(7F, 15F),
                AutoScaleMode = AutoScaleMode.Font,
                ClientSize = new Size(614, 178),
                BackgroundImageLayout = ImageLayout.Center,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                StartPosition = FormStartPosition.CenterScreen,
            };

            System.Windows.Forms.Label LCesta = new()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238),
                Location = new Point(12, 97),
                Name = "Cesta",
                Size = new Size(122, 21),
                Text = "Cesta programu",
            };


            System.Windows.Forms.TextBox TCesta = new()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238),
                Location = new Point(138, 94),
                Name = "Text",
                Size = new Size(427, 29),
                Text = "Cesta není nastavena",
            };

            System.Windows.Forms.Label LVerze = new()
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238),
                Location = new Point(12, 65),
                Name = "label3",
                Size = new Size(84, 21),
                Text = "Verze",
            };

            System.Windows.Forms.Button BInstalovat = new()
            {
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238),
                Location = new Point(509, 135),
                Name = "Instalovat",
                Size = new Size(102, 31),
                Text = "Instalovat",
                UseVisualStyleBackColor = true,
            };

            System.Windows.Forms.Button BProchazet = new()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238),
                Location = new Point(577, 93),
                Name = "Prochazet",
                Size = new Size(34, 31),
                Text = "...",
                UseVisualStyleBackColor = true,
            };

            BInstalovat.Click += BInstalovat_klik;
            BProchazet.Click += BProchazet_klik;

            form.Controls.Add(LVerze);
            form.Controls.Add(LCesta);
            form.Controls.Add(TCesta);
            form.Controls.Add(BInstalovat);
            form.Controls.Add(BProchazet);

            Okno = form;
            form.ShowDialog();
            return form;
        }

        public void BInstalovat_klik(object sender, EventArgs e)
        {
            //Console.WriteLine("Tlačítko bylo stisknuto!");
            Dialog = DialogResult.OK;
            Okno.Close();
        }

        public void BProchazet_klik(object sender, EventArgs e)
        {
            //Console.WriteLine("Tlačítko bylo stisknuto!");
            Dialog = DialogResult.OK;
            Okno.Close();
        }
    }
}
