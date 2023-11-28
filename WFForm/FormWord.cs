

namespace WFForm
{
    public partial class FormWord : Form
    {
        public string Cesta { get; set; }
        public FormWord()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private async void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Získání vybraného řádku
                ListViewItem selectedRow = listView1.SelectedItems[0];

                // Příklad: Získání hodnoty prvního sloupce
                Cesta = selectedRow.SubItems[0].Text;

                DialogResult = DialogResult.OK;
                Close();

            }
        }

        private void FormWord_Load(object sender, EventArgs e)
        {

        }
    }
}
