using LibraryAplikace;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using XMLTabulka1;
using XMLTabulka1.API;
using XMLTabulka1.Trida;
using static System.Windows.Forms.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WFForm
{
    public partial class Form1 : Form
    {
        DataTable table = new();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
         
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            if (true)
            {
                TreeView1.Nodes.Clear();
                foreach (string item in await new XMLTabulka1.Aktualizuj().RestApiListZakazky())
                    TreeView1.Nodes.Add("C_PROJ", item);
            }
            else 
            {
                if (!File.Exists(Cesty.SouborTezakDbf))
                {
                    string Cesta = Aktualizuj.OpenDataze();
                    Form form = Aktualizuj.Text();
                    form.Show();
                    form.TopLevel = true;
                    await new XMLTabulka1.Aktualizuj().SmazatSoubory(Cesta);
                    form.Close();
                    form.Dispose();
                    new XMLTabulka1.Aktualizuj().AktualizujData();
                }

                new XMLTabulka1.Aktualizuj().AktualizujData();

                DbfDotazySQL sql = new();
                string CisloProjektu = "P.018711";
                //string CisloProjektu = ""; //= "P.002112";
            
                string[] Arg = Environment.GetCommandLineArgs();
                if (Arg.Length > 1)
                {
                    if (sql.HledejPrvek(VyberSloupec.C_PROJ, Arg[1]) != null)
                        CisloProjektu = Arg[1];
                }
                //TreeNode Strom = new("Pokus");

                TreeView1.Nodes.Clear();
                foreach (string item in XMLTabulka1.Soubor.LoadTXT(Cesty.CislaProjektuTxt))
                    TreeView1.Nodes.Add("C_PROJ", item);
                //}

                table = new DbfDotazySQL().HledejPrvek(VyberSloupec.C_PROJ, CisloProjektu);
                DataGridView1.Vypis(table);
                ListView1.VypisMojeZakazky();
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Radek cislo ", DataGridView1.CurrentRow.Index.ToString());
            MessageBox.Show("Radek cislo ", DataGridView1.CurrentCell.Value.ToString());
            if (e.RowIndex < -1) return;

            if (DataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("Radek cislo ", e.RowIndex.ToString());
                //DataTable dataTable = new();
                //DataGridView1.SelectedRows[e.RowIndex];

                //DataTable dt = (DataTable)DataGridView1.DataSource;
                //new Soubor().SaveXML(dt, Cesty.AdresarSpusteni + @"XML.xml");

            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (DataGridView1.SelectedRows.Count > 0)
            {
                foreach (ListViewItem item in DataGridView1.SelectedRows)
                {
                    MessageBox.Show(item.SubItems[0].Text);
                }
            }
            //DataGridView1.SelectionChanged -= DataGridView1_SelectionChanged;
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ///
            ///
            //DataGridView1.ListStrom(TreeView1.SelectedNode, TreeView1.PathSeparator);
            ///
            ///
            DataGridView1.ListStromAPI(TreeView1.SelectedNode, TreeView1.PathSeparator);

        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string GlobalID = DataGridView1.Rows[e.RowIndex].Cells["GLOBALID"].Value.ToString();

            if (true)
            { 
                //plat9 pro restAPI
                var teZak = API.LoadJsonAPIJeden<TeZak>($"/api/tezak/globalid/{GlobalID}");
                
            }
            else 
            { 
                //plat� pro soubor
                DataTable data = DbfDotazySQL.Hledej("SELECT * FROM TEZAK WHERE GLOBALID='" + GlobalID + "'");

                //ulo�en� vybran�ho ��dku do pomocn� t��dy
                Sloupec.CelyRadek =  data.Rows[0];

                //P��pona souboru uveden� v datab�zi
                Sloupec.Pripona = Sloupec.CelyRadek[Sloupec.EXT].ToString().ToUpper();
                Sloupec.CestaDatabaze = Cesty.Pomoc + @"\pokus.docx";

                //Ulo�en� vybran�ho ��du v datab�zi do souboru xml
                XMLTabulka1.Soubor.SaveXML(data, Cesty.JedenRadekXml);

                List<TeZak> json = data.DataTabletoJson<TeZak>();
                json.SaveJson(Cesty.JedenRadekJson);
            
                switch (Sloupec.Pripona)
                {
                    case "DWG":
                        DialogResult result = MessageBox.Show("Byl vybr�n soubor DWG. \nN�zev vybran�ho souboru je: " + Sloupec.CelyRadek[Sloupec.NAZEV].ToString() + "\nChce� pokra�ovat ve vytv��en� dokumentu", "Vyber", MessageBoxButtons.YesNo);
                        //pokra�uje v komponent� Autocad
                        //la.Program(Sloupec.CelyRadek);
                        if (result == DialogResult.Yes)
                            Acad.Program(json.First());
                        break;
                    case "XLS":
                        MessageBox.Show("Bylo XLS " + Sloupec.CelyRadek[Sloupec.NAZEV].ToString());
                        break;
                    case "DOC":
                        DialogResult result1 = MessageBox.Show("Byl vybr�n soubor DOC. \nN�zev vybran�ho souboru je: " + Sloupec.CelyRadek[Sloupec.NAZEV].ToString() + "\nChce� pokra�ovat ve vytv��en� dokumentu", "Vyber", MessageBoxButtons.YesNo);
                        if (result1 == DialogResult.Yes)
                            Word.Doc(Sloupec.CestaDatabaze, Cesty.JedenRadekXml);
                        break;
                    default:
                        MessageBox.Show("Bylo XXX " + Sloupec.CelyRadek[Sloupec.NAZEV].ToString());
                        break;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            LibraryAplikace.Zakazky.MojeZakazkyAdd();
            ListView1.VypisMojeZakazky();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            ListView1.VypisMojeZakazky();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            List<MojeZakazky> moje = LibraryAplikace.Zakazky.MojeZakazkyList();
            SelectedListViewItemCollection V = ListView1.SelectedItems;
            foreach (ListViewItem item in V)
            {
                MojeZakazky del = moje.FirstOrDefault(x => x.CisloProjektu == item.Text);
                if (del != null)
                    moje.Remove(del);
                moje.SaveXML(Cesty.PodporaDataXml);
                moje.SaveJson(Cesty.PodporaDataJson);
            }
            ListView1.VypisMojeZakazky();
        }

        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Button3_Click(sender, e);
            }
        }

        private void Button2_Click_2(object sender, EventArgs e)
        {
            //var proc = new System.Diagnostics.Process();
            if (!string.IsNullOrEmpty(InfoProjekt.CisloProjektu) && !string.IsNullOrEmpty(InfoProjekt.Task))
                Process.Start("explorer.exe ", @"G:\z\" + InfoProjekt.CisloProjektu + @"\" + InfoProjekt.Task);
            else if (!string.IsNullOrEmpty(InfoProjekt.CisloProjektu))
            {
                Process.Start("explorer.exe ", @"G:\z\" + InfoProjekt.CisloProjektu);
            }
        }

        private async void Button4_Click(object sender, EventArgs e)
        {
            string Cesta = Aktualizuj.OpenDataze();
            Form form = Aktualizuj.Text();
            form.Show();
            await new XMLTabulka1.Aktualizuj().SmazatSoubory(Cesta);
            form.Close();
            form.Dispose();

            new XMLTabulka1.Aktualizuj().AktualizujData();

            TreeView1.Nodes.Clear();
            foreach (string item in await new XMLTabulka1.Aktualizuj().RestApiListZakazky())
            //foreach (string item in XMLTabulka1.Soubor.LoadTXT(Cesty.CislaProjektuTxt))
                TreeView1.Nodes.Add("C_PROJ", item);
            //}

            table = new DbfDotazySQL().HledejPrvek(VyberSloupec.C_PROJ, InfoProjekt.CisloProjektu);
            DataGridView1.Vypis(table);
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<MojeZakazky> moje = new LibraryAplikace.Zakazky().MojeZakazkyList();
            var V = ListView1.FocusedItem.SubItems; //
            string Vyber = V[0].Text;
            foreach (TreeNode item in TreeView1.Nodes)
            {
                if (item.Text == Vyber)
                {
                    InfoProjekt.CisloProjektu = Vyber;
                    item.ExpandAll();
                    TreeView1.TopNode= item;

                    table = new DbfDotazySQL().HledejPrvek(VyberSloupec.C_PROJ, InfoProjekt.CisloProjektu);
                    DataGridView1.Vypis(table);
                    break;
                }
            }
        }

    }
}