
using LibraryAplikace.Acad;
using LibraryAplikace.Word;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using XMLTabulka1;
using XMLTabulka1.API;
using XMLTabulka1.Trida;

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
            //XMLTabulka1.Cesty.Nastavit();
            var Akt = Menu.Aktualizuj();
            TreeView1.Nodes.Clear();
            foreach (string item in await XMLTabulka1.Aktualizuj.RestApiListZakazky())
                TreeView1.Nodes.Add("C_PROJ", item);
            ListView1.VypisMojeZakazky(LibraryAplikace.Zakazky.MojeZakazkyList());
            Akt.Visible = false;
            Akt.Close();
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

        private async void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ///
            //DataGridView1.ListStrom(TreeView1.SelectedNode, TreeView1.PathSeparator);
            ///
            DataGridView1.ListStromAPI(TreeView1.SelectedNode, TreeView1.PathSeparator);
            string[] Hodnota = TreeView1.SelectedNode.FullPath.Split(TreeView1.PathSeparator);
            if(Hodnota.Length > 1 ) return;
            InfoProjekt.CisloProjektu = Hodnota.FirstOrDefault();
            if (string.IsNullOrEmpty(InfoProjekt.CisloProjektu)) return;
            var jedna = await API.APIJson<TeZak>($"api/TeZak/Projekt/Jedna/{InfoProjekt.CisloProjektu}");
            _ = new InfoProjekt(jedna);
            label1.Text = InfoProjekt.CisloProjektu;
            label2.Text = InfoProjekt.Projekt;
            label3.Text = InfoProjekt.HIP;
        }

        private async void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            //string GlobalID = DataGridView1.Rows[e.RowIndex].Cells["GLOBALID"].Value.ToString();
            string Apid = DataGridView1.Rows[e.RowIndex].Cells["Apid"].Value.ToString();

            //plat9 pro restAPI
            //var teZak = API.LoadJsonAPIJeden<TeZak>($"/api/tezak/globalid/{GlobalID}");
            var Aktualizace = Menu.Aktualizuj();
            TeZak teZak = await API.APIJson<TeZak>($"/api/tezak/Apid/{Apid}");
            Aktualizace.Close();
            if (teZak == null) return;
            //teZak.SaveJson(Cesty.JedenRadekJson);

            //Pøípadná zmìna disku na C pokud není disk neexistuje.
            teZak.PATH = Word.ZmenaDisku(teZak.PATH);

            switch (teZak.EXT.ToUpperInvariant())
            {
                case "DWG":

                    List<string> ListSouboraCAD = Word.ExistujeSouborPriponou(teZak.PATH, Word.Dokument.Autocad);
                    if (ListSouboraCAD != null && ListSouboraCAD.Count > 0)
                    {
                        var FormAcad = new FormWord();
                        FormAcad.listView1.AddListString(ListSouboraCAD);
                        FormAcad.ShowDialog();
                        if (FormAcad.DialogResult == DialogResult.OK)
                        {
                            switch (FormAcad.Volba)
                            {
                                case FormWord.Vyber.Vyvorit:
                                    if (File.Exists(teZak.PATH))
                                    {
                                        //MessageBox.Show("Soubor existuje, Chceš ho smazat a znovu vytvoøit", "Info", MessageBoxButtons.YesNo);
                                        var result = Menu.UkazANONE("Soubor existuje,\nChceš ho smazat a znovu vytvoøit");
                                        if (result == DialogResult.Yes)
                                        {
                                            //kontrola otevøeného souboru
                                            if (File.Exists(teZak.PATH))
                                            {
                                                try { File.Delete(teZak.PATH); }
                                                catch { MessageBox.Show("Soubor je ASI otevøen, NELZE SMAZAT", "Info"); Word.Zobrazit("ACAD"); return; }
                                            }
                                        }
                                        else
                                        { return; }
                                    }
                                    //soubor neexistuje a bude vytvoøen
                                    Acad.Prace(teZak, teZak.PATH);
                                    break;
                                case FormWord.Vyber.Cesta:
                                    //Soubor existuje a mùže být otevøen
                                    Acad.OtevøitExistujícíSouborAcad(teZak.PATH);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else { return; }
                    }
                    else
                    {
                        //soubor neexistuje
                        DialogResult result1 = MessageBox.Show("SOUBOR NE-EXISTUJE \n Byl vybrán soubor " + teZak.NAZEV
                            + "\nChceš vytvoøit dokumentu", "Vyber", MessageBoxButtons.YesNo);
                        //vytvoøit z databázové cesty
                        if (result1 == DialogResult.Yes)
                        {
                            //soubor neexistuje a bude vytvoøen
                            Acad.Prace(teZak, teZak.PATH);
                        }
                    }
                    //DialogResult result = MessageBox.Show("Byl vybrán soubor typu DWG. \nNázev vybraného souboru je: " + teZak.NAZEV
                    //    + "\nChceš pokraèovat ve vytváøení dokumentu", "Vyber", MessageBoxButtons.YesNo);
                    //pokraèuje v komponentì Autocad
                    //la.Program(Sloupec.CelyRadek);
                    //    if (result == DialogResult.Yes)
                    //{
                    //    //hledání všech souborù které odpovídají názvu výkresu dle poslední 6 znakù.
                    //    //V seznamu jsou všechny typy souboru dwg, pdf, atd.
                    //    List<string> ListSoubor = new SouborApp().HledejZdaExistujeSoubor(teZak.PATH);
                    //    List<string> SouborDwg = ListSoubor.Where(x => Path.GetExtension(x).Equals(".DWG", StringComparison.InvariantCultureIgnoreCase)).ToList();
                    //    //doplnit dialog výbìru souboru

                    //    //pokud nexistuje bude vytvoøen
                    //    string Cesta = SouborDwg.FirstOrDefault();
                    //    Acad.Prace(teZak, Cesta);

                    //}
                    break;
                case "XLS":
                    MessageBox.Show("Byl vybrán soubor typu XLS " + teZak.NAZEV);
                    break;
                case "DOC":
                    List<string> ListSouborWord = Word.ExistujeSouborPriponou(teZak.PATH, Word.Dokument.Word);
                    if (ListSouborWord != null && ListSouborWord.Count > 0)
                    {
                        var FormWord = new FormWord();
                        FormWord.listView1.AddListString(ListSouborWord);
                        FormWord.ShowDialog();
                        if (FormWord.DialogResult == DialogResult.OK)
                        {
                            switch (FormWord.Volba)
                            {
                                case WFForm.FormWord.Vyber.Vyvorit:
                                    //teZak.PATH = Path.ChangeExtension(teZak.PATH, ".docx");
                                    //Kotrola existence dokumnetu
                                    if (File.Exists(teZak.PATH) || File.Exists(Path.ChangeExtension(teZak.PATH, ".docx")))
                                    {
                                        var result = MessageBox.Show("Soubor existuje, Chceš ho smazat a znovu vytvoøit", "Info", MessageBoxButtons.YesNo);
                                        if (result == DialogResult.Yes)
                                        {
                                            if (File.Exists(teZak.PATH))
                                                File.Delete(teZak.PATH);
                                            if (File.Exists(Path.ChangeExtension(teZak.PATH, ".docx")))
                                                File.Delete(Path.ChangeExtension(teZak.PATH, ".docx"));
                                        }
                                        else
                                        { return; }
                                    }
                                    //Blokovani.Zapnuto();
                                    if (!await Word.VytvoøitDokumentDoc(teZak))
                                        MessageBox.Show("Chyba pøi generování Wordu.", "Info", MessageBoxButtons.OK);
                                    else
                                    {
                                        var result = MessageBox.Show("Dokument Wordu VYTVOØEN. \n OTEVØÍT?", "Info", MessageBoxButtons.YesNo);
                                        if (result == DialogResult.Yes)
                                        {
                                            if (!await Word.OtevøiDokument(teZak))
                                                MessageBox.Show("Chyba pøi generování Wordu.", "Info", MessageBoxButtons.OK);
                                        }
                                    }

                                    //Blokovani.Vypnuto();
                                    break;
                                case WFForm.FormWord.Vyber.Cesta:
                                    //Pokud byla zvolena cesta pro otevøení souboru
                                    teZak.PATH = FormWord.Cesta;
                                    //Word.Doc(Sloupec.CestaDatabaze, Cesty.JedenRadekXml);
                                    if (!await Word.OtevøiDokument(teZak))
                                        MessageBox.Show("Chyba pøi generování Wordu.", "Info", MessageBoxButtons.OK);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        { return; }
                    }
                    else
                    {
                        //soubor neexistuje
                        var ANONE = new Menu().VyberANONE("SOUBOR NE-EXISTUJE\nvybrán soubor : " + teZak.NAZEV + "\nChceš dokument vytvoøit");
                        //DialogResult result1 = MessageBox.Show("SOUBOR NE-EXISTUJE\nByl vybrán soubor " + teZak.NAZEV
                        //    + "\nChceš vytvoøit dokumentu", "Vyber", MessageBoxButtons.YesNo);
                        //vytvoøit z databázové cesty
                        if (ANONE.DialogResult == DialogResult.Yes)
                        {
                            if (!await Word.VytvoøitDokumentDoc(teZak))
                                MessageBox.Show("Chyba pøi generování Wordu.", "Info", MessageBoxButtons.OK);
                        }
                    }

                    break;
                default:
                    MessageBox.Show("Bylo XXX " + teZak.NAZEV.ToString());
                    break;
            }


        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            //pøidat položku do zakazky
            var VlastniZakazky = await LibraryAplikace.Zakazky.MojeZakazkyAdd();
            ListView1.VypisMojeZakazky(VlastniZakazky);
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            ListView1.VypisMojeZakazky(LibraryAplikace.Zakazky.MojeZakazkyList());
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            List<MojeZakazky> moje = LibraryAplikace.Zakazky.MojeZakazkyList();
            ListView.SelectedListViewItemCollection V = ListView1.SelectedItems;
            foreach (ListViewItem item in V)
            {
                MojeZakazky del = moje.FirstOrDefault(x => x.CisloProjektu == item.Text);
                if (del != null)
                    moje.Remove(del);
                //moje.SaveXML(Cesty.PodporaDataXml);
                moje.SaveJson(Cesty.PodporaDataJson);
            }
            ListView1.VypisMojeZakazky(LibraryAplikace.Zakazky.MojeZakazkyList());
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
            var Aktu = Menu.Aktualizuj();
            await XMLTabulka1.Aktualizuj.SmazatSoubory(Cesta);
            Aktu.Close();
            //Aktu.Dispose();

            XMLTabulka1.Aktualizuj.AktualizujData();

            TreeView1.Nodes.Clear();
            foreach (string item in await XMLTabulka1.Aktualizuj.RestApiListZakazky())
                //foreach (string item in XMLTabulka1.Soubor.LoadTXT(Cesty.CislaProjektuTxt))
                TreeView1.Nodes.Add("C_PROJ", item);
            //}

            table = DbfDotazySQL.HledejPrvek(VyberSloupec.C_PROJ, InfoProjekt.CisloProjektu);
            DataGridView1.Vypis(table);
        }

        private async void ListView1_SelectedIndexChanged(object sender, EventArgs e)
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
                    TreeView1.TopNode = item;

                    List<TeZak> TableJson = await API.APIJsonList<TeZak>($"api/tezak/{InfoProjekt.CisloProjektu}");
                    DataGridView1.Vypis(TableJson);
                    break;
                }
            }
        }

        public bool ANO { get; set; } = true;

        private async void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            const int mindelka = 3;
            if (ComboBox1.SelectedIndex > -1)
            {
                ANO = false;
                ComboBoxItem selected = (ComboBoxItem)ComboBox1.SelectedItem;
                InfoProjekt.CisloProjektu = selected.Tag.ToString();
                List<TeZak> TableJson = await API.APIJsonList<TeZak>($"api/tezak/{InfoProjekt.CisloProjektu}");
                DataGridView1.Vypis(TableJson);
                return;

            }
            string Hodnota = ComboBox1.Text;
            if (Hodnota.Length >= mindelka)
            {
                if (ANO == false) return;
                ComboBox1.TextChanged -= ComboBox1_TextChanged;
                var result = await API.APIJsonList<TeZak>($"api/tezak/search/{Hodnota}");
                if (result.Count != 0)
                {
                    InfoProjekt.CisloProjektu = result.First().C_PROJ;
                    //comboBox1.Items.Clear(); // Clear existing items before adding new ones
                    ComboBox1.Items.Clear();
                    foreach (var teZak in result)
                    {
                        var polozky = new ComboBoxItem(teZak.C_PROJ + " " + teZak.NAZ_PROJ, teZak.C_PROJ);
                        ComboBox1.Items.Add(polozky);
                    }
                    ComboBox1.DroppedDown = true;
                    ComboBox1.Text = Hodnota;
                    ComboBox1.Select(Hodnota.Length, 0);
                }
                ComboBox1.TextChanged += ComboBox1_TextChanged;
            }
            else
                ANO = true;
            if (Hodnota.Length < 1)
                ComboBox1.Items.Clear();
        }

        private async void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (ComboBox1.SelectedItem != null)
                {
                    ComboBoxItem selected = (ComboBoxItem)ComboBox1.SelectedItem;
                    InfoProjekt.CisloProjektu = selected.Tag.ToString();
                    if (ComboBox1.DroppedDown == true)
                        ComboBox1.DroppedDown = false;
                    List<TeZak> TableJson = await API.APIJsonList<TeZak>($"api/tezak/{InfoProjekt.CisloProjektu}");
                    DataGridView1.Vypis(TableJson);
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public class ComboBoxItem(string displayText, object tag)
    {
        public string DisplayText { get; set; } = displayText;
        public object Tag { get; set; } = tag;

        public override string ToString()
        {
            return DisplayText;
        }
    }
}