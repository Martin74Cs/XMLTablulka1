namespace Instal
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Instalace = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // Instalace
            // 
            Instalace.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Instalace.Location = new Point(669, 54);
            Instalace.Name = "Instalace";
            Instalace.Size = new Size(101, 32);
            Instalace.TabIndex = 0;
            Instalace.Text = "Instalovat";
            Instalace.UseVisualStyleBackColor = true;
            Instalace.Click += Instalace_ClickAsync;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(7, 19);
            label1.Name = "label1";
            label1.Size = new Size(112, 21);
            label1.TabIndex = 1;
            label1.Text = "Cesta instalace";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F);
            textBox1.Location = new Point(138, 16);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(632, 29);
            textBox1.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(777, 95);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(Instalace);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Instal";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Instalace;
        private Label label1;
        private TextBox textBox1;
    }
}
