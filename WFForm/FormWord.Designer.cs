namespace WFForm
{
    partial class FormWord
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listView1 = new ListView();
            button1 = new Button();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            listView1.Location = new Point(14, 20);
            listView1.Margin = new Padding(4);
            listView1.Name = "listView1";
            listView1.Size = new Size(750, 171);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(686, 208);
            button1.Name = "button1";
            button1.Size = new Size(75, 38);
            button1.TabIndex = 1;
            button1.Text = "Vytvořit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FormWord
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(777, 259);
            Controls.Add(button1);
            Controls.Add(listView1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Margin = new Padding(4);
            Name = "FormWord";
            Text = "Word";
            Load += FormWord_Load;
            ResumeLayout(false);
        }

        #endregion

        public ListView listView1;
        public Button button1;
    }
}