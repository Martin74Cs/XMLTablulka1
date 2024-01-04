using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTabulka1.Trida
{
    public class Instal
    {
        public int Id { get; set; }
        public string Apid { get; set; } = string.Empty;
        public int Verze { get; set; }
        public string Adresar { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string StoredFileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        //[NotMapped]
        //public List<object> Pole { get; set; } = new();
        //[NotMapped]
        //public List<IFormFile> Files { get; set; } = new();
    }
}
