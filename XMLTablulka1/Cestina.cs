using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTabulka1
{
    public class Cestina
    {
        /// <summary>
        /// Převod znakové sady 852 - do 1250
        /// </summary>
        string Prevod(string text)
        {
            //převede text na bajty
            byte[] bajty = System.Text.Encoding.GetEncoding(852).GetBytes(text);
            return System.Text.Encoding.GetEncoding(1250).GetString(bajty);
        }

        /// <summary>
        /// Převod Dos (sada 852) do (sady 1250) převod Tabulky DataSet do češtiny
        /// </summary>
        public DataTable Tabulka(DataTable Dos)
        {
            //zpřístupní kodování pro převod 852 na 1250 nutno System.Text.Encoding & System.Text.Encoding.CodePages; 
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Console.WriteLine("Převod znakové sady do češtiny");
            DataTable cestina = new DataTable { TableName = "cestina" };
            foreach (DataColumn i in Dos.Columns) //Sloupce 
                //Názvy sloupců do navratové tabulky
                cestina.Columns.Add(i.ToString(), typeof(string));

            DataRow slo;
            foreach (DataRow i in Dos.Rows)
            {
                slo = cestina.NewRow();  //vždy nový řádek
                int pi = 0;
                for (int j = 0; j < Dos.Columns.Count; j++)
                {
                    slo[pi] = Prevod(i[j].ToString());
                    pi++;
                }
                cestina.Rows.Add(slo); //řádky do návratové tabulky
            }
            if (cestina == null) throw new Exception("Chyba převod znakové sady do češtiny");
            return cestina;
        }
    }
}
