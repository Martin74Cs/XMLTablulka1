namespace WFForm
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
            DataGridView1 = new DataGridView();
            TreeView1 = new TreeView();
            Button1 = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            GroupBox1 = new GroupBox();
            Button3 = new Button();
            BAdd = new Button();
            ListView1 = new ListView();
            GroupBox2 = new GroupBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ComboBox1 = new ComboBox();
            Button4 = new Button();
            Button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)DataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            GroupBox1.SuspendLayout();
            GroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // DataGridView1
            // 
            DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(DataGridView1, 2);
            DataGridView1.Dock = DockStyle.Fill;
            DataGridView1.Location = new Point(153, 193);
            DataGridView1.Name = "DataGridView1";
            DataGridView1.RowHeadersWidth = 51;
            tableLayoutPanel1.SetRowSpan(DataGridView1, 2);
            DataGridView1.Size = new Size(789, 298);
            DataGridView1.TabIndex = 0;
            DataGridView1.CellClick += DataGridView1_CellClick;
            DataGridView1.CellContentClick += DataGridView1_CellContentClick_1;
            // 
            // TreeView1
            // 
            TreeView1.Dock = DockStyle.Fill;
            TreeView1.Location = new Point(3, 193);
            TreeView1.Name = "TreeView1";
            tableLayoutPanel1.SetRowSpan(TreeView1, 2);
            TreeView1.Size = new Size(144, 298);
            TreeView1.TabIndex = 1;
            TreeView1.AfterSelect += TreeView1_AfterSelect;
            // 
            // Button1
            // 
            Button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button1.Font = new Font("Segoe UI", 12F);
            Button1.Location = new Point(446, 146);
            Button1.Name = "Button1";
            Button1.Size = new Size(75, 30);
            Button1.TabIndex = 2;
            Button1.Text = "Konec";
            Button1.UseVisualStyleBackColor = true;
            Button1.Click += Button1_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.30072F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.69928F));
            tableLayoutPanel1.Controls.Add(DataGridView1, 1, 1);
            tableLayoutPanel1.Controls.Add(TreeView1, 0, 1);
            tableLayoutPanel1.Controls.Add(GroupBox1, 2, 0);
            tableLayoutPanel1.Controls.Add(GroupBox2, 0, 0);
            tableLayoutPanel1.Location = new Point(1, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 190F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(945, 514);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // GroupBox1
            // 
            GroupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox1.Controls.Add(Button3);
            GroupBox1.Controls.Add(BAdd);
            GroupBox1.Controls.Add(ListView1);
            GroupBox1.Location = new Point(536, 3);
            GroupBox1.Name = "GroupBox1";
            GroupBox1.Size = new Size(406, 184);
            GroupBox1.TabIndex = 2;
            GroupBox1.TabStop = false;
            GroupBox1.Text = "Vlastní zakázky :";
            // 
            // Button3
            // 
            Button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button3.Font = new Font("Segoe UI", 12F);
            Button3.Location = new Point(243, -1);
            Button3.Name = "Button3";
            Button3.Size = new Size(80, 26);
            Button3.TabIndex = 6;
            Button3.Text = "Delete";
            Button3.UseVisualStyleBackColor = true;
            Button3.Click += Button3_Click;
            // 
            // BAdd
            // 
            BAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BAdd.Font = new Font("Segoe UI", 12F);
            BAdd.Location = new Point(329, -1);
            BAdd.Name = "BAdd";
            BAdd.Size = new Size(73, 26);
            BAdd.TabIndex = 4;
            BAdd.Text = "Add";
            BAdd.UseVisualStyleBackColor = true;
            BAdd.Click += Button2_Click;
            // 
            // ListView1
            // 
            ListView1.Dock = DockStyle.Fill;
            ListView1.Location = new Point(3, 19);
            ListView1.Name = "ListView1";
            ListView1.Size = new Size(400, 162);
            ListView1.TabIndex = 3;
            ListView1.UseCompatibleStateImageBehavior = false;
            ListView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;
            ListView1.KeyDown += ListView1_KeyDown;
            // 
            // GroupBox2
            // 
            GroupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(GroupBox2, 2);
            GroupBox2.Controls.Add(label3);
            GroupBox2.Controls.Add(label2);
            GroupBox2.Controls.Add(label1);
            GroupBox2.Controls.Add(ComboBox1);
            GroupBox2.Controls.Add(Button4);
            GroupBox2.Controls.Add(Button1);
            GroupBox2.Controls.Add(Button2);
            GroupBox2.Location = new Point(3, 3);
            GroupBox2.Name = "GroupBox2";
            GroupBox2.Size = new Size(527, 184);
            GroupBox2.TabIndex = 5;
            GroupBox2.TabStop = false;
            GroupBox2.Text = "Funkce";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label3.Location = new Point(10, 74);
            label3.Name = "label3";
            label3.Size = new Size(13, 21);
            label3.TabIndex = 7;
            label3.Text = ".";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label2.Location = new Point(10, 53);
            label2.Name = "label2";
            label2.Size = new Size(13, 21);
            label2.TabIndex = 6;
            label2.Text = ".";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label1.Location = new Point(10, 32);
            label1.Name = "label1";
            label1.Size = new Size(13, 21);
            label1.TabIndex = 5;
            label1.Text = ".";
            // 
            // ComboBox1
            // 
            ComboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ComboBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            ComboBox1.FormattingEnabled = true;
            ComboBox1.Location = new Point(0, 0);
            ComboBox1.Name = "ComboBox1";
            ComboBox1.Size = new Size(524, 29);
            ComboBox1.TabIndex = 0;
            ComboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            ComboBox1.TextChanged += ComboBox1_TextChanged;
            ComboBox1.KeyPress += ComboBox1_KeyPress;
            // 
            // Button4
            // 
            Button4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Button4.Font = new Font("Segoe UI", 12F);
            Button4.Location = new Point(7, 146);
            Button4.Name = "Button4";
            Button4.Size = new Size(75, 31);
            Button4.TabIndex = 4;
            Button4.Text = "Load Data";
            Button4.UseVisualStyleBackColor = true;
            Button4.Visible = false;
            Button4.Click += Button4_Click;
            // 
            // Button2
            // 
            Button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button2.Font = new Font("Segoe UI", 12F);
            Button2.Location = new Point(323, 146);
            Button2.Name = "Button2";
            Button2.Size = new Size(118, 30);
            Button2.TabIndex = 3;
            Button2.Text = "Průzkumník";
            Button2.UseVisualStyleBackColor = true;
            Button2.Click += Button2_Click_2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(947, 522);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Load += Form1_Load;
            Shown += Form1_Shown;
            ((System.ComponentModel.ISupportInitialize)DataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            GroupBox1.ResumeLayout(false);
            GroupBox2.ResumeLayout(false);
            GroupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView DataGridView1;
        private Button Button1;
        public TreeView TreeView1;
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox GroupBox1;
        private ListView ListView1;
        private Button BAdd;
        private Button Button3;
        private GroupBox GroupBox2;
        private Button Button4;
        private Button Button2;
        private ComboBox ComboBox1;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}