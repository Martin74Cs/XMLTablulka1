//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace XMLTabulka1.Trida
//{
//    [Flags]
//    public enum VyberSloupec
//    {
//        C_PROJ,
//        C_UKOL,
//        DIL,
//        CAST,
//        PROFESE,
//        PORADI,
//        NAZ_PROJ,
//        NAZ_UKOL,
//        INVESTOR,
//        M_STAVBY,
//        HIP,
//        NAZEV,
//        AUTOR,
//        AUT_REV,
//        KONTROL,
//        SCHVALIL,
//        EXT,
//        DAT_REV,
//        PRIDANO,
//        APSSO,
//        PROF_CX,
//        OR_CIT,
//        GLOBALID,
//        PATH
//    }

//    public static class Sloupec
//    {
//        public static string C_UKOL => VyberSloupec.C_UKOL.ToString();
//        public static string DIL => VyberSloupec.DIL.ToString();
//        public static string CAST => VyberSloupec.CAST.ToString();
//        public static string PROFESE => VyberSloupec.PROFESE.ToString();
//        public static string C_PROJ => VyberSloupec.C_PROJ.ToString();
//        public static string EXT => VyberSloupec.EXT.ToString();
//        public static string NAZEV => VyberSloupec.NAZEV.ToString();
//        public static string PATH => VyberSloupec.PATH.ToString();
//        public static DataRow CelyRadek { get; set; }
//        public static string Pripona { get; set; }
//        public static string CestaDatabaze { get; set; }
//    }
//}
