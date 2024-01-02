using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTabulka1.Trida
{
    public class InfoProjekt
    {
        public static string CisloProjektu { get; set; } = string.Empty;
        public static string Projekt { get; set; } = string.Empty;
        public static string CisloTasku { get; set; } = string.Empty;
        public static string Dil { get; set; } = string.Empty;
        public static string Cast { get; set; } = string.Empty;
        public static string Task { get; set; } = string.Empty;
        public static string Nazev { get; set; } = string.Empty;
        public static string Misto { get; set; } = string.Empty;
        public static string Investor { get; set; } = string.Empty;
        public static string HIP { get; set; } = string.Empty;

        public InfoProjekt(TeZak teZak)
        {
            Projekt = teZak.NAZ_PROJ;
            CisloProjektu = teZak.C_PROJ;
            HIP = teZak.HIP;
            Misto = teZak.M_STAVBY;
            Investor = teZak.INVESTOR;
            //doplnit další pokud bude potřeba
        }
    }
}
