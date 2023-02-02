using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Podpora;

namespace XMLTabulka1
{
    public class Word
    {
        /// <summary>
        /// volani dynamické knihovny
        /// </summary>
        /// <param name="cesta">cesta kde umístěn soubor</param>
        /// <param name="XML">cesta k XML záznam z databaze</param>
        public static void Doc(string cesta, string XML)
        { 
            WordPodpora app = new();
            bool Volba = app.NovyDoc(cesta, XML);
            return;
        }
    }
}
