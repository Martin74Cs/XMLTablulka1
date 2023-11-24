// See https://aka.ms/new-console-template for more information



using Instal;

Console.WriteLine("Poslat soubor na WEB .....");
string cesta = string.Empty;

if(Environment.MachineName == "KANCELAR")
    cesta = @"D:\OneDrive\Databaze\Tezak\XMLTablulka1\Setup\Debug\Instal.msi";
else
    cesta = @"D:\OneDrive\Databaze\Tezak\XMLTablulka1\Setup\Debug\Instal.msi";

string SoubourCode = await Install.Upload(cesta);
if (string.IsNullOrEmpty(SoubourCode))
    Console.WriteLine("Chyba nahrání souboru");
else
    Console.WriteLine($"Byl nahran soubor : {SoubourCode}");