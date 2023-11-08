using System.Data;
using System.Diagnostics;
using System.Xml.Linq;
using XMLTabulka1;
using XMLTabulka1.Trida;

//string[] Arg = Environment.GetCommandLineArgs();
//Dictionary<string, string> Pole = Database.Argument.GetArgument(Arg);

Console.Clear();
Console.WriteLine("Environment.MachineName " + Environment.MachineName);
Console.WriteLine("Environment.UserName " + Environment.UserName);
Console.WriteLine("Environment.CurrentDirectory " + Environment.CurrentDirectory);
Console.WriteLine("Environment.OSVersion " + Environment.OSVersion);
Console.WriteLine("Environment.GetCommandLineArgs " + Environment.GetCommandLineArgs());
Console.WriteLine("Sucesfully ....");
Console.WriteLine("Continuos Press Key ....");
//Console.ReadKey();
//Environment.Exit(0);

//Soubor soubor = new();
Console.Title = "Pokus";
Console.WriteLine("Aktuální Adresář " + Cesty.AktualniAdresar);
Console.WriteLine("Adresář Spuštěni " + Cesty.AdresarSpusteni);
Console.WriteLine("Soubor Exe " + Cesty.SouborExe);
Console.WriteLine("Soubor Dbf " + Cesty.SouborDbf);
Console.WriteLine("Soubor Pomoc " + Cesty.Pomoc);
//Console.ReadKey();

//Kontrola zanam v SQL vuci DBF
await SQL.DataSqlAdd();

//funguje seznam tabulek SQL3DPlant
//
//SQL sql = new SQL();
//sql.Databaze().SaveTXT(Cesty.SQL3DPlant);

//funguje odladěno
//
//Console.WriteLine("Převod dbf na SQL ANO/NE");
//ConsoleKeyInfo k = Console.ReadKey(true);
//if (k.Key == ConsoleKey.A || k.Key == ConsoleKey.Z)
//{ 
//    //převede databazi dbf na sql
//    SQL.DataSql();
//    return;
//}


//InfoProjekt.CisloProjektu = "P.018806";
//DataTable tabulka = new DbfDotazySQL().JedenTezak(Sloupec.C_PROJ, InfoProjekt.CisloProjektu);
//List<TeZak> Txt = tabulka.DataTabletoJson<TeZak>();
//Txt.SaveJson(Cesty.JedenRadekJson);
//Soubor.Pruzkumnik(Cesty.JedenRadekJson);

//LibraryAplikace.Acad.Program(tabulka.Rows[0]);
//DataTable data = new SQLDotazy().HledejVse();

//Kontroly kontroly = new();
//Kontroly.CteniZapisXML();
//Kontroly.CteniZapisTXT();
//Kontroly.Linq();

//LinqDotazy ld = new();

//DataTable data1 = sql.HledejVse();
//ld.Test4(data1).Vypis();
//ld.Slopec1(data1, VyberSloupec.C_UKOL).Vypis();
//soubor.SaveXML(data1, Cesty.SouborDbf + "/Test4Linq.xml");
//Console.WriteLine(data1.Rows[0][VyberSloupec.NAZ_PROJ.ToString()].ToString());

//Najde číslo záznam se zadaným číslem dokuemntu.
//
//DataTable table = new DbfDotazySQL().HledejCislo("N7000");
//if (table != null)
//{
//    Console.WriteLine(table.Rows[0][VyberSloupec.NAZ_PROJ.ToString()].ToString());
//    Console.WriteLine(table.Rows[0][VyberSloupec.C_UKOL.ToString()].ToString());
//    Sloupec.CelyRadek = table.Rows[0];
//    Razitko razitko = new();
//    Dictionary<string, string> Deleni = razitko.Prenos(Sloupec.CelyRadek);
//    Deleni.Vypis();


//    Soubor.SaveXML(table, Cesty.Pomoc + "/XML.xml");
//    //table.WriteXml(Cesty.SouborDbf);
//    //table = ld.Test(table);
//    //Soubor.SaveXML(table, Cesty.Pomoc + "/Testinq.xml");
//}

//DataTable Nove = Soubor.CSVtoDataTable(Cesty.Pomoc + "/DataTabletoCSV.txt");
//Soubor.SaveXML(Nove, Cesty.Pomoc + "/NoveXML.xml");

//Console.WriteLine(table.Rows[0][VyberSloupec.NAZ_PROJ.ToString()].ToString());
//Nove.DataTabletoCSV(Cesty.Pomoc + "/NoveTabletoCSV.txt");



