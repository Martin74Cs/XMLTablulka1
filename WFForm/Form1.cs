using System.Data;
using System.Runtime.InteropServices;
using XMLTabulka1;


namespace WFForm
{
    public partial class Form1 : Form
    {
        DataTable table = new();
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            SQLDotazy sql = new();
            string CisloProjektu = "P.018711";
            //string CisloProjektu = ""; //= "P.002112";
            
            string[] Arg = Environment.GetCommandLineArgs();
            if (Arg.Length > 1)
            {
                if (sql.HledejPrvek(VyberSloupec.C_PROJ, Arg[1]) != null)
                    CisloProjektu = Arg[1];
            }
            TreeNode Strom = new("Pokus");
            if (!File.Exists(Cesty.CislaProjektuTxt))
            {
                string[] Pole = sql.SeznamJeden(VyberSloupec.C_PROJ);
                CisloProjektu = Pole[0];
                Pole.SaveTXT(Cesty.CislaProjektuTxt);          
            }
            if (File.Exists(Cesty.CislaProjektuTxt))
            {
                foreach (string item in Soubor.LoadTXT(Cesty.CislaProjektuTxt))
                    TreeView1.Nodes.Add("C_PROJ", item);
            }

            table = await new SQLDotazy().HledejPrvek(VyberSloupec.C_PROJ, CisloProjektu);
            dataGridView1.DataSource = table;
            Barvy(dataGridView1);
            SloupecSirka(dataGridView1.Columns);
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Radek cislo ", dataGridView1.CurrentRow.Index.ToString());
            MessageBox.Show("Radek cislo ", dataGridView1.CurrentCell.Value.ToString());
            if (e.RowIndex < -1) return;

            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("Radek cislo ", e.RowIndex.ToString());
                //DataTable dataTable = new();
                //dataGridView1.SelectedRows[e.RowIndex];

                DataTable dt = (DataTable)dataGridView1.DataSource;
                //new Soubor().SaveXML(dt, Cesty.AdresarSpusteni + @"XML.xml");

            }
        }



        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (ListViewItem item in dataGridView1.SelectedRows)
                {
                    MessageBox.Show(item.SubItems[0].Text);

                }
            }
            //dataGridView1.SelectionChanged -= DataGridView1_SelectionChanged;
        }

        private async void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode N = TreeView1.SelectedNode;
            //char Separ = Convert.ToChar(TreeView1.PathSeparator);
            string[] Cesta = N.FullPath.Split(TreeView1.PathSeparator);
            if (N.Nodes.Count <= 0)
            { 
                switch (Cesta.Length)
                {
                    case 0:
                        break;
                    case 1:
                        string querry = "SELECT DISTINCT " + Sloupec.C_UKOL + "  FROM TEZAK WHERE [" + N.Name + "]='" + N.Text + "'";

                        foreach (DataRow item in SQLDotazy.Hledej(querry).Rows)
                            N.Nodes.Add(Sloupec.C_UKOL, item[Sloupec.C_UKOL].ToString());
                        N.Expand();
                        break;
                    case 2:
                        querry = "SELECT DISTINCT DIL FROM TEZAK WHERE " + N.Parent.Name + "='" + N.Parent.Text + "' AND " + N.Name + "='" + N.Text + "' AND (NOT (DIL IS NULL))";
                        foreach (DataRow item in SQLDotazy.Hledej(querry).Rows)
                            N.Nodes.Add("DIL", item["DIL"].ToString());
                        N.Expand();
                        break;
                    case 3:
                        querry = "SELECT DISTINCT [CAST] FROM TEZAK WHERE " + N.Parent.Parent.Name + "='" + N.Parent.Parent.Text + "'AND " + N.Parent.Name + "='" + N.Parent.Text + "'AND " + N.Name + "='" + N.Text + "' AND (NOT ([CAST] IS NULL))";
                        foreach (DataRow item in SQLDotazy.Hledej(querry).Rows)
                            N.Nodes.Add("CAST", item["CAST"].ToString());
                        N.Expand();
                        break;
                    default:
                        break;
                }
            }
            switch (Cesta.Length)
            {
                case 0:
                    break;
                case 1:
                    string querry = "SELECT DIL,[CAST],PROFESE,PORADI,OR_CISLO,NAZEV,PCC,TD,PCDOC,C_REV,ID_DOCR,EXT,GLOBALID,NAZ_PROJ,INVESTOR,M_STAVBY,HIP FROM TEZAK WHERE C_PROJ='" + Cesta[0] + "' ORDER BY DIL,[CAST],PROFESE,PORADI,OR_CISLO";
                    dataGridView1.DataSource = SQLDotazy.Hledej(querry);
                    break;
                case 2:
                    querry = "SELECT DIL,[CAST],PROFESE,PORADI,OR_CISLO,PROF_CX,OR_CIT,NAZEV,PCC,TD,PCDOC,C_REV,ID_DOCR,EXT,GLOBALID FROM TEZAK WHERE C_PROJ='" + Cesta[0] + "' AND C_UKOL = '" + Cesta[1] + "' ORDER BY DIL,[CAST],PROFESE,PORADI,OR_CISLO,PROF_CX,OR_CIT";
                    dataGridView1.DataSource = SQLDotazy.Hledej(querry);
                    break;
                case 3:
                    querry = "SELECT DIL,[CAST],PROFESE,PORADI,OR_CISLO,PROF_CX,OR_CIT,NAZEV,PCC,TD,PCDOC,C_REV,ID_DOCR,EXT,GLOBALID FROM TEZAK WHERE C_PROJ='" + Cesta[0] + "' AND C_UKOL = '" + Cesta[1] + "' AND DIL = '" + Cesta[2] + "' ORDER BY DIL,[CAST],PROFESE,PORADI,OR_CISLO,PROF_CX,OR_CIT";
                    dataGridView1.DataSource = SQLDotazy.Hledej(querry);
                    break;
                case 4:
                    querry = "SELECT * FROM TEZAK WHERE C_PROJ='" + Cesta[0] + "' AND C_UKOL = '" + Cesta[1] + "' AND [DIL] = '" + Cesta[2] + "' AND [CAST] = '" + Cesta[3] + "' ORDER BY [DIL],[CAST],PROFESE,PORADI,OR_CISLO,PROF_CX,OR_CIT";
                    dataGridView1.DataSource = SQLDotazy.Hledej(querry);
                    break;
                default:
                    break;
            }
            Barvy(dataGridView1);
            SloupecSirka(dataGridView1.Columns);
        }

        public static void Barvy(DataGridView data)
        {
            for (int Radek = 0; Radek < data.Rows.Count - 1; Radek++)
            {
                switch (data.Rows[Radek].Cells[VyberSloupec.EXT.ToString()].Value.ToString().ToUpper())
                {
                    case "DWG":
                        if (Radek % 2 == 0)
                            data.Rows[Radek].DefaultCellStyle.BackColor = Color.FromArgb(242, 224, 182);
                        else
                            data.Rows[Radek].DefaultCellStyle.BackColor = Color.FromArgb(237, 213, 156);
                        break;
                    case "DOC":
                        if (Radek % 2 == 0)
                            data.Rows[Radek].DefaultCellStyle.BackColor = Color.FromArgb(214, 244, 245);
                        else
                            data.Rows[Radek].DefaultCellStyle.BackColor = Color.FromArgb(186, 236, 237);
                        break;
                    case "XLS":
                        if (Radek % 2 == 0)
                            data.Rows[Radek].DefaultCellStyle.BackColor = Color.FromArgb(213, 236, 157);
                        else
                            data.Rows[Radek].DefaultCellStyle.BackColor = Color.FromArgb(199, 229, 123);
                        break;
                    default:
                        if (Radek % 2 == 0)
                            data.Rows[Radek].DefaultCellStyle.BackColor = Color.White;
                        else
                            data.Rows[Radek].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                        break;
                }
            }
        }

        public static void SloupecSirka(DataGridViewColumnCollection Columns)
        {
            string[] ABC = { "DIL", "CAST", "PROFESE", "PORADI", "OR_CISLO", "PROF_CX", "OR_CIT", "NAZEV", "PCC", "TD", "PCDOC", "C_REV", "ID_DOCR", "EXT", "GLOBALID" };
            int[] Cisla = { 50, 50, 50, 50, 50, 50, 50, 300, 40, 40, 60, 40, 40, 40, 100 };
            for (int i = 0; i < ABC.Length; i++)
            {
                if (Columns[ABC[i]] is null)
                    continue;
                Columns[ABC[i]].Width = Cisla[i];
            }
        }

        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string GlobalID = dataGridView1.Rows[e.RowIndex].Cells["GLOBALID"].Value.ToString();
            DataTable data = SQLDotazy.Hledej("SELECT * FROM TEZAK WHERE GLOBALID='" + GlobalID + "'");

            //uložení vybraného øádku do pomocné tøídy
            Sloupec.CelyRadek =  data.Rows[0];

            //Pøípona souboru uvedená v databázi
            Sloupec.Pripona = Sloupec.CelyRadek[Sloupec.EXT].ToString().ToUpper();
            Sloupec.CestaDatabaze = Cesty.Pomoc + @"/pokus.docx";

            //Uložení vybraného øádu v databázi do souboru xml
            Soubor.SaveXML(data, Cesty.JedenRadekXml);

            switch (Sloupec.Pripona)
            {
                case "DWG":
                    MessageBox.Show("Byl vybrán soubor DWG. \n Název vybraného souboru je: " + Sloupec.CelyRadek[Sloupec.NAZEV].ToString());
                    //pokraèuje v komponentì Autocad
                    LibraryAplikace.Acad la = new();
                    la.Program(Sloupec.CelyRadek);
                    break;
                case "XLS":
                    MessageBox.Show("Bylo XLS " + Sloupec.CelyRadek[Sloupec.NAZEV].ToString());
                    break;
                case "DOC":
                    MessageBox.Show("Byl vybrán soubor DOC. \n Název vybraného souboru je: " + Sloupec.CelyRadek[Sloupec.NAZEV].ToString());
                    Word.Doc(Sloupec.CestaDatabaze, Cesty.JedenRadekXml);
                    break;
                default:
                    MessageBox.Show("Bylo XXX " + Sloupec.CelyRadek[Sloupec.NAZEV].ToString());
                    break;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new LibraryAplikace.Soubor().mo .MojeZakazkyPridat()
        }
    }
}