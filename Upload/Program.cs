// See https://aka.ms/new-console-template for more information

using Instal;
using Library;
using XMLTabulka1;

Console.WriteLine("Poslat instalační soubor na WEB .....");
if (Console.ReadKey().Key == ConsoleKey.A)
{
    string cesta = @"D:\OneDrive\Databaze\Tezak\XMLTablulka1\Setup\Debug\Setup.msi";
    string Target = Path.Combine(Path.GetDirectoryName(cesta), "Instal.msi");
    File.Copy(cesta, Target, true);
    string SoubourCode = await Install.Upload(Target);
    if (string.IsNullOrEmpty(SoubourCode))
        Console.WriteLine("Chyba nahrání souboru");
    else
        Console.WriteLine($"Byl nahran soubor : {SoubourCode}");
}

using Instal;

Console.WriteLine("Poslat novou verzi ZIP na WEB .....");
if (Console.ReadKey().Key == ConsoleKey.A)
{

    Console.Write("Zip .....");
    Zip.Start(Cesty.AdresarDebugWFForm, Cesty.ZIP);
    Console.WriteLine("Ok");

if(Environment.MachineName == "KANCELAR")
    cesta = @"D:\OneDrive\Databaze\Tezak\XMLTablulka1\Setup\Debug\Instal.msi";
else
    cesta = @"D:\OneDrive\Databaze\Tezak\XMLTablulka1\Setup\Debug\Instal.msi";

    Console.WriteLine("Poslat soubor na WEB .....");
    string SoubourZip = await Install.Upload(Cesty.ZIP);
    if (string.IsNullOrEmpty(SoubourZip))
        Console.WriteLine("Chyba nahrání souboru");
    else
        Console.WriteLine($"Byl nahran soubor : {SoubourZip}");
}