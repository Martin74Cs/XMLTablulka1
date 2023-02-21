using AutoCAD;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XMLTabulka1;
using XMLTabulka1.Trida;

namespace LibraryAplikace
{
    public partial class Zakazky
    {
        public void MojeZakazkyAdd()
        {
            //string json = JsonSerializer.Serialize(InfoProjekt);

            //TeZak.GetTeZak().SaveJson(Cesty.PodporaDataXml);
            //string test = InfoProjekt.Projekt;

            //InfoProjekt.Projekt;
            //InfoProjekt.CisloProjektu;           

            if (File.Exists(Cesty.PodporaDataXml) == false)
            {
                FileStream fs = new FileStream(Cesty.PodporaDataXml,FileMode.CreateNew);
                fs.Close();

                StreamWriter fw = new StreamWriter(Cesty.PodporaDataXml);
                fw.WriteLine("<SEZNAM></SEZNAM>");
                fw.Close();
            }
            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Cesty.PodporaDataXml);
            XmlElement elem = doc.CreateElement("C_PROJ");


        }

        
        public void MojeZakazkyOLD()
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
                XmlElement? room = doc.DocumentElement;
                if (room == null) return;
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
