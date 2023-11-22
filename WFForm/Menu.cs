using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WFForm
{
    public class Menu
    {
        public static Form Aktualizuj()
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
        public static Form NovaVerze()
        {
            System.Windows.Forms.Label Label = new()
            {
                Location = new Point(13, 32),
                Name = "Aktualizace",
                Text = "Byla vydána nová verze",
                Size = new Size(300, 25),
            };

            System.Windows.Forms.Button Tlacitko = new()
            {
                Location = new Point(97, 9),
                Name = "Label1",
                Size = new Size(123, 20),
                Text = "Aktualizuj",
            };
            Tlacitko.Click += Akce;

            Form form = new()
            {
                AutoScaleDimensions = new System.Drawing.SizeF(6, 13),
                AutoScaleMode = AutoScaleMode.Font,
                BackgroundImageLayout = ImageLayout.Center,
                ClientSize = new System.Drawing.Size(350, 70),
                FormBorderStyle = FormBorderStyle.FixedSingle,
                StartPosition = FormStartPosition.CenterScreen,
            };
            form.Controls.Add(Label);
            form.Controls.Add(Tlacitko);
            //form.ShowDialog();

            form.Visible = true;
            form.TopMost = true;
            form.ShowDialog();
            return form;
        }

        private void Akce(object sender, EventArgs e)
        {

        }

    }
}
