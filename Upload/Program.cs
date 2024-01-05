// See https://aka.ms/new-console-template for more information


using Library;
using System.Diagnostics;
using XMLTabulka1;
using XMLTabulka1.Trida;

//Console.WriteLine("Poslat insalační program exe na WEB .....[Ano/Ne]");
//if (Console.ReadKey(true).Key == ConsoleKey.A)
//{
//    Console.Write("Vytvožení kopie InstalPrvni Setup pro instalaci......");
//    Zip.KopirovatSlozku(Cesty.AdresarInstalPrvni, Cesty.PripravaSetup);
//    Console.WriteLine("Ok");

//    Console.Write("Vytvožení Zip ze složky PripravaTeZak.....");
//    Zip.Start(Cesty.AdresarInstalPrvni, Cesty.InstalZIP);

//    //pokus o vytvoření EXE
//    SevenZIPmoje.SevenExe(Cesty.AdresarInstalPrvni, Cesty.InstalExe);
//    Console.WriteLine("Poslat soubor na WEB .....");
//    string SoubourZip = await Install.Upload(Cesty.InstalExe);
//    if (string.IsNullOrEmpty(SoubourZip))
//        Console.WriteLine("Chyba nahrání souboru");
//    else
//        Console.WriteLine($"Byl nahran soubor : {SoubourZip}");
//}


Console.WriteLine("Poslat kompletní intalaci zip na WEB .....[Ano/Ne]");
if (Console.ReadKey(true).Key == ConsoleKey.A)
{
    ////nefunguje z jiného umsátění
    //string cesta = @"D:\OneDrive\Databaze\Tezak\XMLTablulka1\Setup\Debug\Setup.msi";
    //if (Environment.MachineName == "KANCELAR")
    //    cesta = @"c:\Users\Martin\OneDrive\Databaze\Tezak\XMLTablulka1\Setup\Debug\Setup.msi";

    Console.Write("Vytvožení kopie TeZak pro instalaci......");
    Zip.KopirovatSlozku(Cesty.AdresarDebugWFForm, Cesty.PripravaTeZakAll);
    Console.WriteLine("Ok");

    Console.Write("Vytvožení kopie Instlal pro instalaci......");
    Zip.KopirovatSlozku(Cesty.AdresarDebugInstal, Cesty.PripravaTeZakInstal);
    Console.WriteLine("Ok");

    Console.Write("Vytvožení Zip ze složky PripravaTeZakAll.....");
    Zip.Start(Cesty.PripravaTeZakAll, Cesty.InstalTeZakAll);

    Console.Write("Vytvožení kopie TeZak pro instalaci......");
    Zip.KopirovatSlozku(Cesty.AdresarDebugWFForm, Cesty.PripravaTeZak);
    Console.WriteLine("Ok");

    Console.Write("Vytvožení Zip ze složky PripravaTeZak.....");
    Zip.Start(Cesty.PripravaTeZak, Cesty.InstalTeZak);

    //SevenZIP.Start(Cesty.AdresarDebugInstal, Cesty.SevenZIP);
    Console.WriteLine("Ok");

    Console.WriteLine("Poslat soubor PripravaTeZakAll na WEB .....");
    string SoubourZip = await Install.Install.Upload(Cesty.InstalTeZakAll);
    if (string.IsNullOrEmpty(SoubourZip))
        Console.WriteLine("Chyba nahrání souboru");
    else
        Console.WriteLine($"Byl nahran soubor : {SoubourZip}");

    Console.WriteLine("Poslat soubor PripravaTeZakAll na WEB .....");
    SoubourZip = await Install.Install.Upload(Cesty.InstalTeZak);
    if (string.IsNullOrEmpty(SoubourZip))
        Console.WriteLine("Chyba nahrání souboru");
    else
        Console.WriteLine($"Byl nahran soubor : {SoubourZip}");

    //Smazaní adresaže ZIP
    //if (File.Exists(Cesty.InstalTeZak))
    //{
    //    File.Delete(Cesty.InstalTeZakAll);
    //    File.Delete(Cesty.InstalTeZak);

    //    if (Directory.Exists(Cesty.ZIP))
    //    {
    //        //Adresar Zip někdo používá možná nahrávání na web 
    //        Directory.Delete(Cesty.InstalTeZak, true);
    //    }
    //}

    //if (File.Exists(cesta)) 
    //{
    //    string Target = Path.Combine(Path.GetDirectoryName(cesta), "Instal.msi");
    //    File.Copy(cesta, Target, true);
    //    string SoubourCode = await Install.Upload(Target);
    //    if (string.IsNullOrEmpty(SoubourCode))
    //        Console.WriteLine("Chyba nahrání souboru");
    //    else
    //        Console.WriteLine($"Byl nahran soubor : {SoubourCode}");
    //}
}

//Console.WriteLine("Poslat novou verzi hlavního programu v ZIP na WEB jako nouvou verzi programu .....[Ano/Ne]");
//if (Console.ReadKey(true).Key == ConsoleKey.A)
//{
//    Console.Write("Zip .....");
//    Zip.Start(Cesty.AdresarDebugWFForm, Cesty.InstalZIP);
//    Console.WriteLine("Ok");

//    Console.WriteLine("Poslat soubor na WEB .....");
//    string SoubourZip = await Install.Install.Upload(Cesty.InstalZIP);
//    if (string.IsNullOrEmpty(SoubourZip))
//        Console.WriteLine("Chyba nahrání souboru");
//    else
//        Console.WriteLine($"Byl nahran soubor : {SoubourZip}");

//    //Smazaní adresaže ZIP
//    //if (File.Exists(Cesty.ZIPProgram))
//    //{
//    //    if (File.Exists(Cesty.InstalZIP))
//    //    {
//    //        File.Delete(Cesty.InstalZIP);
//    //        //adresar zip někdo používá možná nahrávání na web 
//    //        //Nejde smazat
//    //        //Directory.Delete(Path.GetDirectoryName(Cesty.ZIP), true);
//    //    }
//    //}

//}

Console.WriteLine("Poslat novou verzi Manifest na WEB pro kontrolu publikování nového programu.....[Ano/Ne]");
if (Console.ReadKey(true).Key == ConsoleKey.A)
{
    string file = "TeZak.json";

    //stažení původního manifestu
    List<Instal> Pole = await Install.Install.GetSearchAsync(file);
    if (Pole.Count < 1) return;
    Instal Prvni = Pole.FirstOrDefault();

    var result = await Install.Install.ManifestDownloadAsync(Prvni.StoredFileName);
    if (result == null)
        result = new ProgramInfo() { Version = "0.0.1", DownloadUrl= new Uri("http://10.55.1.100/api/Instal/Manifest/"), ReleaseDate = DateTime.Now};

    Console.WriteLine($"Manifes původní : {result.Version}");
    //navýšení verze
    string[] Verze = result.Version.Split('.');
    if (int.TryParse(Verze.Last(), out int Cislo))
    {
        Cislo++;
        string Uprava = string.Empty;
        for (int i = 0; i < Verze.Length-1; i++)
            Uprava += Verze[i] + ".";
        Uprava += Cislo.ToString();

        //nahrání upraveného manifestu
        await Install.Install.ManifestUploadAsync(file, Uprava);

        //stažení upraveného manifestu pro kontrolu
        var Vysledek = await Install.Install.ManifestDownloadAsync(file);
        Console.WriteLine($"Manifes nový : {Vysledek.Version}");
    }
}



