// See https://aka.ms/new-console-template for more information

using Instal;
using Library;
using System.Diagnostics;
using XMLTabulka1;
using XMLTabulka1.Trida;

Console.WriteLine("Poslat kompletní intalaci zip na WEB .....[Ano/Ne]");
if (Console.ReadKey(true).Key == ConsoleKey.A)
{
    ////nefunguje z jiného umsátění
    //string cesta = @"D:\OneDrive\Databaze\Tezak\XMLTablulka1\Setup\Debug\Setup.msi";
    //if (Environment.MachineName == "KANCELAR")
    //    cesta = @"c:\Users\Martin\OneDrive\Databaze\Tezak\XMLTablulka1\Setup\Debug\Setup.msi";

    Console.Write("Vytvožení kopie TeZak pro instalaci......");
    Zip.KopirovatSlozku(Cesty.AdresarDebugWFForm, Cesty.PripravaTeZak);
    Console.WriteLine("Ok");

    Console.Write("Vytvožení kopie Instlal pro instalaci......");
    Zip.KopirovatSlozku(Cesty.AdresarDebugInstal, Cesty.PripravaTeZakInstal);
    Console.WriteLine("Ok");

    Console.Write("Zip .....");
    Zip.Start(Cesty.PripravaTeZak, Cesty.ZIP);
    //SevenZIP.Start(Cesty.AdresarDebugInstal, Cesty.SevenZIP);
    Console.WriteLine("Ok");

    Console.WriteLine("Poslat soubor na WEB .....");
    string SoubourZip = await Install.Upload(Cesty.ZIP);
    if (string.IsNullOrEmpty(SoubourZip))
        Console.WriteLine("Chyba nahrání souboru");
    else
        Console.WriteLine($"Byl nahran soubor : {SoubourZip}");

    //Smazaní adresaže ZIP
    if (File.Exists(Cesty.ZIP))
    {
        if (Directory.Exists(Path.GetDirectoryName(Cesty.ZIP)))
        {
            //adresar zip někdo používá možná nahrávání na web 
            //Directory.Delete(Path.GetDirectoryName(Cesty.ZIP), true);
        }
    }

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

Console.WriteLine("Poslat novou verzi ZIP na WEB .....[Ano/Ne]");
if (Console.ReadKey(true).Key == ConsoleKey.A)
{
    Console.Write("Zip .....");
    Zip.Start(Cesty.AdresarDebugWFForm, Cesty.ZIP);
    Console.WriteLine("Ok");

    Console.WriteLine("Poslat soubor na WEB .....");
    string SoubourZip = await Install.Upload(Cesty.ZIP);
    if (string.IsNullOrEmpty(SoubourZip))
        Console.WriteLine("Chyba nahrání souboru");
    else
        Console.WriteLine($"Byl nahran soubor : {SoubourZip}");

    //Smazaní adresaže ZIP
    if (File.Exists(Cesty.ZIP))
    {
        if (Directory.Exists(Path.GetDirectoryName(Cesty.ZIP)))
        { 
            //adresar zip někdo používá možná nahrávání na web 
            //Nejde smazat
            //Directory.Delete(Path.GetDirectoryName(Cesty.ZIP), true);
        }
    }

}

Console.WriteLine("Poslat novou verzi Manifest na WEB .....[Ano/Ne]");
if (Console.ReadKey(true).Key == ConsoleKey.A)
{
    var result = await Install.ManifestDownloadAsync();
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
        await Install.ManifestUploadAsync(Uprava);
        var Vysledek = await Install.ManifestDownloadAsync();
        Console.WriteLine($"Manifes nový : {Vysledek.Version}");
    }
}



