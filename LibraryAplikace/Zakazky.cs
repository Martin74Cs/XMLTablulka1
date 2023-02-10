using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XMLTabulka1;

namespace LibraryAplikace
{
    public partial class Zakazky
    {
        public void MojeZakazky()
        {
            if (File.Exists(Cesty.PodporaDataXml))
            { 
                XmlDocument doc = new XmlDocument();
                doc.XmlResolver = null;
                Stream fs = new FileStream(Cesty.PodporaDataXml, FileMode.Open);
                XmlReader rdr = XmlReader.Create(fs);
                doc.Load(rdr);
                rdr.Close();

                Dictionary<string, string> ListZak = new();
                XmlElement room = doc.DocumentElement;
                foreach (XmlNode sd in room.ChildNodes)
                {
                    foreach (XmlNode n in sd.ChildNodes)
                    {
                        switch (n.Name)
                        {
                            case "C_PROJ":
                                ListZak[n.Name] = n.InnerText;
                                break;
                            case "NAZ_PROJ":
                                ListZak[n.Name] = n.InnerText;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }


        }
    }
}
