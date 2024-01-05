using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLTabulka1;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library
{
    public class Zip
    {
        /// <summary>
        /// ZIPOVÁNNÍ ZADANÉ SLOŽKY
        /// </summary>
        /// <param name="DirZip"></param>
        public static bool Start(string DirZip, string zipSoubor)
        {
            // Zkontrolujte, zda zadaná složka existuje
            if (Directory.Exists(DirZip))
            {
                // Nastavte název výsledného zip souboru
                if (string.IsNullOrEmpty(zipSoubor))
                    return false;
                    //zipSoubor = @"c:\Z\Zip.zip";

                //Smazaní původního ZIP soboru
                if(File.Exists(zipSoubor))
                       File.Delete(zipSoubor);

                // Vytvoření archivu ZIP //výjimkou nebude souboru data.json
                ZipArchiveZip(DirZip, zipSoubor);
                return true;

                //ZipFile.CreateFromDirectory(DirZip, zipSoubor);
            }
            else
            {
                Console.WriteLine("Zadaná složka neexistuje.");
                return false;
            }
        }

        //public static void ZipArchiveZip(string DirZip, string zipSoubor)
        //{
        //    // Otevření souboru ZIP pro zápis
        //    using var zipFileStream = new FileStream(zipSoubor, FileMode.Create);
        //    using var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create);
        //    // Přidání všech souborů z adresáře do archivu
        //    //foreach (string soubor in Directory.GetFiles(DirZip))
        //    //foreach (string soubor in Directory.GetFiles(DirZip).Where(file => Path.GetFileName(file) != "data.json"))
        //    foreach (string soubor in Soubor.SeznamSouboruAdresarioPod(DirZip).Where(file => Path.GetFileName(file) != "data.json"))
        //    {
        //        //výjimkou souboru data.json
        //        //if (Path.GetFileName(soubor) != "data.json")
        //        //{
        //            // Přidání souboru do archivu
        //            // Nepřídává adresáře
        //            ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(soubor));
        //            using Stream entryStream = entry.Open();
        //            using var fileStream = new FileStream(soubor, FileMode.Open, FileAccess.Read);
        //            fileStream.CopyTo(entryStream);
        //        //}
        //    }

        //}


        public static void ZipArchiveZip(string sourceDirectory, string zipFileName)
        {
            using (var zipFileStream = new FileStream(zipFileName, FileMode.Create))
            { 
                using (var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
                {
                    AddFilesToZip(archive, sourceDirectory, "");
                }
            }
            
        }
        private static void AddFilesToZip(ZipArchive archive, string sourceDirectory, string relativePath)
        {
            string[] files = Directory.GetFiles(Path.Combine(sourceDirectory, relativePath));
            foreach (string filePath in files.Where(file => Path.GetFileName(file) != "data.json"))
            {
                string entryName = Path.Combine(relativePath, Path.GetFileName(filePath));
                ZipArchiveEntry entry = archive.CreateEntry(entryName);
                using (Stream entryStream = entry.Open())
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    fileStream.CopyTo(entryStream);
                }
            }

            string[] subdirectories = Directory.GetDirectories(Path.Combine(sourceDirectory, relativePath));
            foreach (string subdirectory in subdirectories)
            {
                string relativeSubdirectory = Path.Combine(relativePath, Path.GetFileName(subdirectory));
                AddFilesToZip(archive, sourceDirectory, relativeSubdirectory);
            }
        }

        /// <summary>
        /// Rozipování zadaného souboru do vygenerované složkdy tem
        /// </summary>
        /// <param name="slozkaunzip"></param>
        /// <returns></returns>
        public static string UnStart(string slozkaunzip)
        {
            // Zkontrolujte, zda zadaná soubor existuje
            if (File.Exists(slozkaunzip))
            {
                DirectoryInfo Cesta = Directory.CreateTempSubdirectory();
                string zipFilePath = Path.Combine(Cesta.FullName);

                // Extrahování souborů z archivu
                System.IO.Compression.ZipFile.ExtractToDirectory(slozkaunzip, zipFilePath );
                return zipFilePath;
            }
            return null;
        }

        /// <summary>
        /// Kopírování souboru složek a podsložek rekuzivní
        /// </summary>
        public static void KopirovatSlozku(string zdrojovaSlozka, string cilovaSlozka)
        {
            try
            {
                // Získání seznamu všech souborů a podsložek ve zdrojové složce
                string[] vsechnySoubory = Directory.GetFileSystemEntries(zdrojovaSlozka);

                // Kopírování souborů a podsložek
                foreach (string souborOboji in vsechnySoubory)
                {
                    // Sestavení cílového názvu souboru nebo složky
                    string cilovySouborNeboSlozka = Path.Combine(cilovaSlozka, Path.GetFileName(souborOboji));

                    if (Directory.Exists(souborOboji))
                    {
                        if (!Directory.Exists(cilovySouborNeboSlozka))
                            Directory.CreateDirectory(cilovySouborNeboSlozka);
                        // Rekurzivní kopírování podsložky
                        KopirovatSlozku(souborOboji, cilovySouborNeboSlozka);
                    }
                    else
                    {
                        if (File.Exists(cilovySouborNeboSlozka))
                            File.Delete(cilovySouborNeboSlozka);
                        // Kopírování souboru
                        File.Copy(souborOboji, cilovySouborNeboSlozka);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chyba při kopírování složky: {ex.Message}");
            }
        }
    }

}

