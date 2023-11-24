using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTabulka1.Trida
{
    public class ProgramInfo
    {
        public string Version { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public Uri DownloadUrl { get; set; } 
    }

    public class Upload
    {
        public int Id { get; set; }
        public string Apid { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string StoredFileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }
}
