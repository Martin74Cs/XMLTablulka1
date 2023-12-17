

namespace WFForm
{
    public partial class FormSoubor : Form
    {
        public string Cesta { get; set; }

        public Vyber Volba { get; set; }
        public enum Vyber
        { 
            Vyvorit,
            Cesta
        }

        public FormSoubor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Volba = Vyber.Vyvorit;
            DialogResult = DialogResult.OK;
        }

        private async void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Získání vybraného řádku
                ListViewItem selectedRow = listView1.SelectedItems[0];

                // Příklad: Získání hodnoty prvního sloupce
                Cesta = selectedRow.SubItems[0].Text;
                Volba = Vyber.Cesta;
                DialogResult = DialogResult.OK;
                Close();

            }
        }

        private void FormWord_Load(object sender, EventArgs e)
        {

        }
    }
}
