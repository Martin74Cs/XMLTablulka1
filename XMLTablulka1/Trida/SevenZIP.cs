using SevenZip;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTabulka1.Trida
{
    public class SevenZIP
    {
        public static void Start(string DirZip, string ZipFiles)
        {
            var seven = new SevenZipCompressor
            {
                ArchiveFormat = OutArchiveFormat.SevenZip,
                CompressionLevel = SevenZip.CompressionLevel.Normal,
                //VolumeSize = 1024 * 1024,
            };
            //seven.CompressionFinished += (sender, e) => CompressionProgressChanged?.Invoke(this, EventArgs.Empty);
            if (Directory.Exists(DirZip))
            { 
                var files = Soubor.SeznamSouboruAdresarioPod(DirZip);
                seven.CompressFiles(ZipFiles, [.. files]);
            }          
            return;
        }

     }
}
