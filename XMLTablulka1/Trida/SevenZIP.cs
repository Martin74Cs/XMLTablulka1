﻿using SevenZip;
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

     }
}
