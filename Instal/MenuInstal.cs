using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instal
{
    public  class MenuInstal
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
                Name = "Instalace",
                Font = new Font("Segoe UI", 12F),
                Size = new Size(123, 20),
                Text = "Probíhá instalce....",
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
}

