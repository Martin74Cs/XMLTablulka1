using System.Data;
using System.Diagnostics;
using System.Xml.Linq;
using XMLTabulka1;
using XMLTabulka1.Trida;

//string[] Arg = Environment.GetCommandLineArgs();
//Dictionary<string, string> Pole = Database.Argument.GetArgument(Arg);

//Soubor soubor = new();
Console.Title = "Pokus";
Console.WriteLine("Aktuální Adresář " + Cesty.AktualniAdresar);
Console.WriteLine("Adresář Spuštěni " + Cesty.AdresarSpusteni);
Console.WriteLine("Soubor Exe " + Cesty.SouborExe);
Console.WriteLine("Soubor Dbf " + Cesty.SouborDbf);
Console.WriteLine("Soubor Pomoc " + Cesty.Pomoc);
Console.WriteLine("Environment.MachineName " + Environment.MachineName);

//Console.ReadKey();

//funguje seznam tabulek SQL3DPlant
//SQL sql = new SQL();
//sql.Databaze().SaveTXT(Cesty.SQL3DPlant);

//funguje odladěno
Console.WriteLine("Převod dbf na SQL ANO/NE");
ConsoleKeyInfo k = Console.ReadKey(true);
if (k.Key == ConsoleKey.A || k.Key == ConsoleKey.Z)
{ 
    //převede databazi dbf na sql
    SQL.DataSql();
    return;
}

InfoProjekt.CisloProjektu = "P.018806";
DataTable tabulka = new SQLDotazy().JedenTezak(Sloupec.C_PROJ, InfoProjekt.CisloProjektu);
List<TeZak> Txt = tabulka.DataTabletoJson<TeZak>();
Txt.SaveJson(Cesty.JedenRadekJson);
Soubor.Pruzkumnik(Cesty.JedenRadekJson);

//LibraryAplikace.Acad.Program(tabulka.Rows[0]);

//DataTable data = new SQLDotazy().HledejVse();

//Kontroly kontroly = new();
Kontroly.CteniZapisXML();
Kontroly.CteniZapisTXT();
Kontroly.Linq();

//LinqDotazy ld = new();

//DataTable data1 = sql.HledejVse();
//ld.Test4(data1).Vypis();

//ld.Slopec1(data1, VyberSloupec.C_UKOL).Vypis();

//soubor.SaveXML(data1, Cesty.SouborDbf + "/Test4Linq.xml");
//Console.WriteLine(data1.Rows[0][VyberSloupec.NAZ_PROJ.ToString()].ToString());

DataTable table = new SQLDotazy().HledejCislo("N7000");
if (table != null)
{
    Console.WriteLine(table.Rows[0][VyberSloupec.NAZ_PROJ.ToString()].ToString());
    Console.WriteLine(table.Rows[0][VyberSloupec.C_UKOL.ToString()].ToString());
    Sloupec.CelyRadek = table.Rows[0];
    Razitko razitko = new();
    Dictionary<string, string> Deleni = razitko.Prenos(Sloupec.CelyRadek);
    Deleni.Vypis();

//    Soubor.SaveXML(table, Cesty.Pomoc + "/XML.xml");
//    //table.WriteXml(Cesty.SouborDbf);

//    //table = ld.Test(table);
//    //Soubor.SaveXML(table, Cesty.Pomoc + "/Testinq.xml");
}

//DataTable Nove = Soubor.CSVtoDataTable(Cesty.Pomoc + "/DataTabletoCSV.txt");
//Soubor.SaveXML(Nove, Cesty.Pomoc + "/NoveXML.xml");

//Console.WriteLine(table.Rows[0][VyberSloupec.NAZ_PROJ.ToString()].ToString());
//Nove.DataTabletoCSV(Cesty.Pomoc + "/NoveTabletoCSV.txt");

//Console.WriteLine("Continuos Press Key ....");
Console.WriteLine("Sucesfully ....");
//Console.ReadKey();
//Console.Clear();
Console.WriteLine("Environment.MachineName " + Environment.MachineName);
Console.WriteLine("Environment.UserName " + Environment.UserName);
 Environment.Exit(0);

public class Kontroly
{
    public static void Linq()
    {
        SQLDotazy sql = new();
        DataTable table = sql.HledejPrvek(VyberSloupec.C_PROJ, "P.018806");
        if (table == null) return;
        //LinqDotazy linq = new();
        DataTable Nova = LinqDotazy.Test5(table,VyberSloupec.C_UKOL);
        Nova.DataTabletoCSV(Cesty.Pomoc + "/CSVLinq51.txt");
        Nova = LinqDotazy.Test5(table, VyberSloupec.DIL);
        Nova.DataTabletoCSV(Cesty.Pomoc + "/CSVLinq52.txt");
        Nova = LinqDotazy.Test6(table, VyberSloupec.C_UKOL);
        Nova.DataTabletoCSV(Cesty.Pomoc + "/CSVLinq6.txt");
    }


    public static void CteniZapisXML()
    {
        SQLDotazy sql = new();
        DataTable table = sql.HledejPrvek(VyberSloupec.C_PROJ, "P.018806");
        if (table == null) return;

        Soubor.SaveXML(table, Cesty.Pomoc + "/XML.xml");

        XDocument NewTable = Soubor.LoadXML(Cesty.Pomoc + "/XML.xml"); 
        Soubor.SaveXML(NewTable, Cesty.Pomoc + "/NewXML.xml");

    }

    public static void CteniZapisTXT()
    {
        SQLDotazy sql = new();
        DataTable table = sql.HledejPrvek(VyberSloupec.C_PROJ, "P.018806");
        if (table == null) return;
        table.DataTabletoCSV(Cesty.Pomoc + "/CSV.txt");

        //Soubor.SaveJson(table);
        //table.SaveJson();

        //DataTable NewTable = Soubor.CSVtoDataTable(Cesty.Pomoc + "/CSV.txt"); ;
        table.DataTabletoCSV(Cesty.Pomoc + "/CSVNew.txt");
    }
}


public static class Rozšíření
{
    public static void Vypis(this DataTable data)
    {
        foreach (DataRow item in data.Rows)
        {
            foreach (DataColumn col in data.Columns)
            {
                Console.Write(item[col.ColumnName].ToString() + ", ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("Continuos Press Key ....");
        Console.ReadKey(true);
    }

    public static void Vypis(this Dictionary<string, string> data)
    {
        foreach (KeyValuePair<string, string> item in data)
        {
            Console.WriteLine(item.Key + item.Value);
        }
        Console.WriteLine("Continuos Press Key ....");
        Console.ReadKey(true);
    }

    public static void Vypis(this string[] data)
    {
        foreach (string item in data)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("Continuos Press Key ....");
        Console.ReadKey(true);
    }
}

