using AutoCAD;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XMLTabulka1;
using XMLTabulka1.Trida;

namespace LibraryAplikace
{
    public partial class Zakazky
    {
        public string JmenoProjektuDlePrvniho()
        {
           DataTable table = new SQLDotazy().JedenTezak(Sloupec.C_PROJ,InfoProjekt.CisloProjektu);
           List<TeZak> teZak = table.DataTabletoJson<TeZak>();
           //var Jeden = teZak.First();
           //InfoProjekt.Projekt = Jeden.NAZEV;
           return teZak.First().NAZ_PROJ;
        }

        public void MojeZakazkyAddJson()
        {
            if (File.Exists(Cesty.PodporaDataXml) == false)
            {
                List<MojeZakazky> moje = new();
                //moje.CisloProjektu = InfoProjekt.CisloProjektu;
                //moje.ProjektNazev = InfoProjekt.Projekt;
                moje.SaveJson(Cesty.PodporaDataJson);
            }

            //MojeZakazky moje1 = new MojeZakazky();
            //MojeZakazky xx = XMLTabulka1.Soubor.LoadJson<MojeZakazky>(Cesty.PodporaDataJson);
        }

        public void MojeZakazkyAdd()
        {
            InfoProjekt.Projekt = JmenoProjektuDlePrvniho();

            MojeZakazkyAddJson();

            if (File.Exists(Cesty.PodporaDataXml) == false)
            {
                //XmlDocument docnew = new XmlDocument();

                //XmlNode rootNode = docnew.CreateElement("SEZNAM");
                //docnew.AppendChild(rootNode);

                //docnew.Save(Cesty.PodporaDataXml);

                XDocument docnew = new XDocument();
                XElement xElement = new XElement("SEZNAM");
                docnew.Add(xElement);
                docnew.Save(Cesty.PodporaDataXml);
            }
            XDocument doc = XDocument.Load(Cesty.PodporaDataXml);
            XElement xEle = new XElement("C_PROJ", InfoProjekt.CisloProjektu);
            doc.Root.Add(xEle);

            xEle = new XElement("NAZ_PROJ", InfoProjekt.Nazev);
            doc.Root.Add(xEle);
            doc.Save(Cesty.PodporaDataXml);

            //string xmlString = File.ReadAllText(Cesty.PodporaDataXml);
            //if (string.IsNullOrEmpty(xmlString)) return;

            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(xmlString);

            //XmlElement radek = doc.CreateElement("RADEK");
            //XmlNode Rodic = doc.DocumentElement.AppendChild(radek);

            //XmlElement elem = doc.CreateElement("C_PROJ");
            //elem.InnerText = InfoProjekt.CisloProjektu;
            //Rodic.AppendChild(elem);

            //elem = doc.CreateElement("NAZ_PROJ");
            //elem.InnerText = InfoProjekt.Projekt;
            //Rodic.AppendChild(elem);

            //doc.PreserveWhitespace = true;
            //doc.Save(Cesty.PodporaDataXml);

            //MojeZakazky moje = new MojeZakazky();
            //moje = XMLTabulka1.Soubor.LoadJson<MojeZakazky>(Cesty.PodporaDataJson);

            List<MojeZakazky> mojelist = new();
            mojelist = XMLTabulka1.Soubor.LoadJsonList<MojeZakazky>(Cesty.PodporaDataJson);

            MojeZakazky nove = new MojeZakazky 
            { 
                CisloProjektu = InfoProjekt.CisloProjektu,
                ProjektNazev = InfoProjekt.Projekt,
            };
            mojelist.Add(nove);
            mojelist.SaveJson(Cesty.PodporaDataJson);
        }

        public List<MojeZakazky> MojeZakazkyList()
        {
            if (File.Exists(Cesty.PodporaDataXml) == false) return new();        

            string xmlString = File.ReadAllText(Cesty.PodporaDataXml);
            if (string.IsNullOrEmpty(xmlString)) return new();

            XDocument doc = XDocument.Load(Cesty.PodporaDataXml);          
            string Json = XMLTabulka1.Soubor.XmlToJsonWithoutRoot(doc);

            List<MojeZakazky> mojelist = new();
            mojelist = XMLTabulka1.Soubor.LoadJsonList<MojeZakazky>(Cesty.PodporaDataJson);
            return mojelist;
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
