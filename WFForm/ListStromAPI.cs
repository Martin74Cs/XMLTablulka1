using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLTabulka1.Trida;
using XMLTabulka1;
using XMLTabulka1.API;

namespace WFForm
{
    public static class APIExtension
    {
        public static async void ListStromAPI(this DataGridView dataGridView1, TreeNode N, string Separator)
        {
            string[] Cesta = N.FullPath.Split(Separator);
            List<TeZakHodnota> querry;
            TeZak teZak = null;
            if (N.Nodes.Count >= 0)
            {
                //SELECT C_UKOL,DIL,[CAST], PROFESE, PORADI
                switch (Cesta.Length)
                {
                    case 0:
                        break;
                    case 1:
                        teZak = new TeZak() { C_PROJ = N.Text };
                        querry = await API.LoadJsonAPI<TeZakHodnota>($"api/TeZak/Projekt/{N.Text}");
                        foreach (TeZakHodnota item in querry)
                            N.Nodes.Add(Sloupec.C_UKOL, item.Hodnota);
                        N.Expand();
                        //vazba na moje zakazky
                        InfoProjekt.CisloProjektu = Cesta[0].ToString();
                        InfoProjekt.CisloTasku = string.Empty;
                        InfoProjekt.Dil = string.Empty;
                        break;
                    case 2:
                        //querry = "SELECT DISTINCT DIL FROM TEZAK WHERE " + N.Parent.Name + "='" + N.Parent.Text + "' AND " + N.Name + "='" + N.Text + "' AND (NOT (DIL IS NULL))";
                        teZak = new TeZak() { C_PROJ = N.Parent.Text, C_UKOL = N.Text };
                        querry = await API.APISaveDatabase<TeZakHodnota>("api/TeZak/Task", teZak);
                        foreach (TeZakHodnota item in querry)
                            N.Nodes.Add(Sloupec.DIL, item.Hodnota);
                        N.Expand();
                        InfoProjekt.CisloTasku = Cesta[1].ToString();
                        InfoProjekt.Dil = string.Empty;
                        break;
                    case 3:
                        //    querry = "SELECT DISTINCT [CAST] FROM TEZAK WHERE " + N.Parent.Parent.Name + "='" + N.Parent.Parent.Text + "'AND " + N.Parent.Name + "='" + N.Parent.Text + "'AND " + N.Name + "='" + N.Text + "' AND (NOT ([CAST] IS NULL))";
                        teZak = new TeZak() { C_PROJ = N.Parent.Parent.Text, C_UKOL = N.Parent.Text, DIL = N.Text };
                        querry = await API.APISaveDatabase<TeZakHodnota>("api/TeZak/dil", teZak);
                        foreach (TeZakHodnota item in querry)
                            N.Nodes.Add(Sloupec.CAST, item.Hodnota);
                        //    foreach (DataRow item in SQLDotazy.Hledej(querry).Rows)
                        //        N.Nodes.Add("CAST", item["CAST"].ToString());
                        N.Expand();
                        InfoProjekt.Dil = Cesta[2].ToString();
                        break;
                    case 4:
                        teZak = new TeZak() { C_PROJ = N.Parent.Parent.Parent.Text, C_UKOL = N.Parent.Parent.Text, DIL = N.Parent.Text, CAST = N.Text };
                        break;
                    default:
                        break;
                }

                var data = await API.APISaveDatabase<TeZak>("api/TeZak/kriteria", teZak);
                if (data == null || data.Count < 1) return;
                dataGridView1.DataSource = data;
                dataGridView1.Barvy();
                dataGridView1.Columns.SloupecSirka();
            }
        }
    }
}
