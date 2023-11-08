using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMLTabulka1.Trida;
using XMLTabulka1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography.X509Certificates;

namespace WFForm
{
    public static class Strom
    {
        public static void ListStrom(this DataGridView DataGridView1, TreeNode N, string Separator)
        {
            string[] Cesta = N.FullPath.Split(Separator);
            if (N.Nodes.Count <= 0)
            {
                switch (Cesta.Length)
                {
                    case 0:
                        break;
                    case 1:
                        string querry = "SELECT DISTINCT " + Sloupec.C_UKOL + "  FROM TEZAK WHERE [" + N.Name + "]='" + N.Text + "'";

                        foreach (DataRow item in DbfDotazySQL.Hledej(querry).Rows)
                            N.Nodes.Add(Sloupec.C_UKOL, item[Sloupec.C_UKOL].ToString());
                        N.Expand();
                        //vazba na moje zakazky
                        InfoProjekt.CisloProjektu = Cesta[0].ToString();
                        break;
                    case 2:
                        querry = "SELECT DISTINCT DIL FROM TEZAK WHERE " + N.Parent.Name + "='" + N.Parent.Text + "' AND " + N.Name + "='" + N.Text + "' AND (NOT (DIL IS NULL))";
                        foreach (DataRow item in DbfDotazySQL.Hledej(querry).Rows)
                            N.Nodes.Add("DIL", item["DIL"].ToString());
                        N.Expand();
                        break;
                    case 3:
                        querry = "SELECT DISTINCT [CAST] FROM TEZAK WHERE " + N.Parent.Parent.Name + "='" + N.Parent.Parent.Text + "'AND " + N.Parent.Name + "='" + N.Parent.Text + "'AND " + N.Name + "='" + N.Text + "' AND (NOT ([CAST] IS NULL))";
                        foreach (DataRow item in DbfDotazySQL.Hledej(querry).Rows)
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
                    DataGridView1.DataSource = DbfDotazySQL.Hledej(querry);
                    break;
                case 2:
                    querry = "SELECT DIL,[CAST],PROFESE,PORADI,OR_CISLO,PROF_CX,OR_CIT,NAZEV,PCC,TD,PCDOC,C_REV,ID_DOCR,EXT,GLOBALID FROM TEZAK WHERE C_PROJ='" + Cesta[0] + "' AND C_UKOL = '" + Cesta[1] + "' ORDER BY DIL,[CAST],PROFESE,PORADI,OR_CISLO,PROF_CX,OR_CIT";
                    DataGridView1.DataSource = DbfDotazySQL.Hledej(querry);
                    break;
                case 3:
                    querry = "SELECT DIL,[CAST],PROFESE,PORADI,OR_CISLO,PROF_CX,OR_CIT,NAZEV,PCC,TD,PCDOC,C_REV,ID_DOCR,EXT,GLOBALID FROM TEZAK WHERE C_PROJ='" + Cesta[0] + "' AND C_UKOL = '" + Cesta[1] + "' AND DIL = '" + Cesta[2] + "' ORDER BY DIL,[CAST],PROFESE,PORADI,OR_CISLO,PROF_CX,OR_CIT";
                    DataGridView1.DataSource = DbfDotazySQL.Hledej(querry);
                    break;
                case 4:
                    querry = "SELECT * FROM TEZAK WHERE C_PROJ='" + Cesta[0] + "' AND C_UKOL = '" + Cesta[1] + "' AND [DIL] = '" + Cesta[2] + "' AND [CAST] = '" + Cesta[3] + "' ORDER BY [DIL],[CAST],PROFESE,PORADI,OR_CISLO,PROF_CX,OR_CIT";
                    DataGridView1.DataSource = DbfDotazySQL.Hledej(querry);
                    break;
                default:
                    break;
            }
            DataGridView1.Barvy();
            DataGridView1.Columns.SloupecSirka();
        }

        public static void Barvy(this DataGridView data)
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

        public static void SloupecSirka(this DataGridViewColumnCollection Columns)
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

        public static void Vypis(this DataGridView DataGridView1, DataTable table)
        {
            DataGridView1.DataSource = table;
            DataGridView1.Barvy();
            DataGridView1.Columns.SloupecSirka();
        }
    }
}
