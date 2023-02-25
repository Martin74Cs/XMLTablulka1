using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLTabulka1;

namespace WFForm
{
    public  class Aktulizuj
    {
        /// <summary>
        /// Otevri umístění databaze pro vytvoření kopie
        /// </summary>
        /// <returns></returns>
        public string OpenDataze()
        { 
            string cesta = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog 
            { 
                Title = "Vyper databázi Dbf",
                InitialDirectory = @"G:\env",
                Filter = ".dbf",
                FileName = "Tezak.dbf"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            { 
                Cesty.ZdrojDbf = dialog.FileName;
            }
            return cesta;
        }


    }
}
