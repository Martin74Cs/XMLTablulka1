using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using XMLTabulka1.Trida;

namespace XMLTabulka1.Trida
{
    public class Entity 
    {
        public int Id { get; set; }
        public string Apid { get; set; } = string.Empty;
    }

    public class TeZakHodnota : Entity
    {
        //public int Id { get; set; }
        public string Hodnota { get; set; } = string.Empty;
        public List<TeZakHodnota> Children { get; set; } = new();
        public string GlobalID { get; set; } = string.Empty;
    }

    public sealed class TeZak
    {
        public string C_PROJ { get; set; } = string.Empty;
        public string CYCOSTATUS { get; set; } = string.Empty;
        public string NAZ_PROJ { get; set; } = string.Empty;
        public string INVESTOR { get; set; } = string.Empty;
        public string M_STAVBY { get; set; } = string.Empty;
        public string C_UKOL { get; set; } = string.Empty;
        public string NAZ_UKOL { get; set; } = string.Empty;
        public string HIP { get; set; } = string.Empty;
        public string DIL { get; set; } = string.Empty;
        public string CAST { get; set; } = string.Empty;
        public string PROFESE { get; set; } = string.Empty;
        public string PORADI { get; set; } = string.Empty;
        public string OR_CISLO { get; set; } = string.Empty;
        public string NAZEV { get; set; } = string.Empty;
        public string AUTOR { get; set; } = string.Empty;
        public string PSSO { get; set; } = string.Empty;
        public string PCC { get; set; } = string.Empty;
        public string TD { get; set; } = string.Empty;
        public string PCDOC { get; set; } = string.Empty;
        public string TYP_DOC { get; set; } = string.Empty;
        public string VYBAVENI { get; set; } = string.Empty;
        public string P_STR { get; set; } = string.Empty;
        public string POV { get; set; } = string.Empty;
        public string DAT_REV { get; set; } = string.Empty;
        public string C_REV { get; set; } = string.Empty;
        public string AUT_REV { get; set; } = string.Empty;
        public string ID_DOC { get; set; } = string.Empty;
        public string PATH { get; set; } = string.Empty;
        public string GLOBALID { get; set; } = string.Empty;
        public string LOCKFLAG { get; set; } = string.Empty;
        public string REVFLAG { get; set; } = string.Empty;
        public string EXT { get; set; } = string.Empty;
        public string APSSO { get; set; } = string.Empty;
        public string FPC { get; set; } = string.Empty;
        public string ID_DOCR { get; set; } = string.Empty;
        public string OR_CIT { get; set; } = string.Empty;
        public string POM { get; set; } = string.Empty;
        public string PROF_CX { get; set; } = string.Empty;
        public string C_SUBREVpublic { get; set; } = string.Empty;
        public string YES { get; set; } = string.Empty;
        public string NO { get; set; } = string.Empty;
        public string MANAG { get; set; } = string.Empty;
        public string ID_DOCM { get; set; } = string.Empty;
        public string SIGNUM { get; set; } = string.Empty;
        public string TITLEBLOCK { get; set; } = string.Empty;
        public string TITLESCALE { get; set; } = string.Empty;
        public string TITLESHEET { get; set; } = string.Empty;
        public string FLGN_E { get; set; } = string.Empty;
        public string POP_REV { get; set; } = string.Empty;
        public string SIG { get; set; } = string.Empty;
        public string ID_DOCD { get; set; } = string.Empty;
        public string ID_DOCC { get; set; } = string.Empty;
        public string ODBOR { get; set; } = string.Empty;
        public string PS_SO { get; set; } = string.Empty;
        public string CISLO { get; set; } = string.Empty;
        public string SPD_LA { get; set; } = string.Empty;
        public string SPD_TX { get; set; } = string.Empty;
        public string KONTROL { get; set; } = string.Empty;
        public string SCHVALIL { get; set; } = string.Empty;
        public string CD_PUV { get; set; } = string.Empty;
        public string NAZEV2 { get; set; } = string.Empty;
        public string ISSUER { get; set; } = string.Empty;
        public string PROJECT { get; set; } = string.Empty;
        public string TYPE { get; set; } = string.Empty;
        public string NUMBER { get; set; } = string.Empty;
        public string FOLIO { get; set; } = string.Empty;
    } 
}
