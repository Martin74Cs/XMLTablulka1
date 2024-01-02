using SevenZip;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTabulka1.Trida
{
    public class SevenZIPmoje
    {
        public static void Start(string DirZip, string ZipFiles)
        {
            //SevenZipCompressor.SetLibraryPath(Path.Combine(@"C:\Program Files\7-Zip", "7z.dll"));
            var seven = new SevenZipCompressor
            {
                ArchiveFormat = OutArchiveFormat.SevenZip,
                CompressionLevel = SevenZip.CompressionLevel.Normal,
                CompressionMode = SevenZip.CompressionMode.Create,
                PreserveDirectoryRoot = false,
                //VolumeSize = 1024 * 1024,
        };
            //seven.CompressionFinished += (sender, e) => CompressionProgressChanged?.Invoke(this, EventArgs.Empty);
            if (Directory.Exists(DirZip))
            { 
                var files = Soubor.SeznamSouboruAdresarioPod(DirZip);
                seven.CompressFiles(ZipFiles, files.ToArray());
            }          
            return;
        }

        public static void SevenExe(string DirZip, string ExeFiles)
        {
            SevenZipCompressor.SetLibraryPath(@"c:\Program Files\7-Zip\7z.dll");
            // Vytvoření instance knihovny SevenZip
            SevenZipCompressor compressor = new SevenZipCompressor();

            // Nastavení parametrů komprese (zde používáme LZMA kompresi)
            compressor.CompressionMethod = SevenZip.CompressionMethod.Lzma;
            compressor.CompressionLevel = SevenZip.CompressionLevel.Ultra;

            // Komprese obsahu adresáře do EXE souboru
            compressor.CompressDirectory(DirZip, ExeFiles);

            Console.WriteLine("Komprese dokončena.");
        }
    }
}
