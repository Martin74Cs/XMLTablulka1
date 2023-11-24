using System.Data;
using System.Diagnostics;
using System.Xml.Linq;
using XMLTabulka1;
using XMLTabulka1.Trida;

public class Kontroly
{
    public static void Linq()
    {
        DbfDotazySQL sql = new();
        DataTable table = sql.HledejPrvek(VyberSloupec.C_PROJ, "P.018806");
        if (table == null) return;
        //LinqDotazy linq = new();
        DataTable Nova = LinqDotazy.Test5(table, VyberSloupec.C_UKOL);
        Nova.DataTabletoCSV(Cesty.Pomoc + "/CSVLinq51.txt");
        Nova = LinqDotazy.Test5(table, VyberSloupec.DIL);
        Nova.DataTabletoCSV(Cesty.Pomoc + "/CSVLinq52.txt");
        Nova = LinqDotazy.Test6(table, VyberSloupec.C_UKOL);
        Nova.DataTabletoCSV(Cesty.Pomoc + "/CSVLinq6.txt");
    }


    public static void CteniZapisXML()
    {
        DbfDotazySQL sql = new();
        DataTable table = sql.HledejPrvek(VyberSloupec.C_PROJ, "P.018806");
        if (table == null) return;

        Soubor.SaveXML(table, Cesty.Pomoc + "/XML.xml");

        XDocument NewTable = Soubor.LoadXML(Cesty.Pomoc + "/XML.xml");
        Soubor.SaveXML(NewTable, Cesty.Pomoc + "/NewXML.xml");

    }

    public static void CteniZapisTXT()
    {
        DbfDotazySQL sql = new();
        DataTable table = sql.HledejPrvek(VyberSloupec.C_PROJ, "P.018806");
        if (table == null) return;
        table.DataTabletoCSV(Cesty.Pomoc + "/CSV.txt");

        //Soubor.SaveJson(table);
        //table.SaveJson();

        //DataTable NewTable = Soubor.CSVtoDataTable(Cesty.Pomoc + "/CSV.txt"); ;
        table.DataTabletoCSV(Cesty.Pomoc + "/CSVNew.txt");
    }

}
