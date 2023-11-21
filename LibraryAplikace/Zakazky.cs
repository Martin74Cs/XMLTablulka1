using AutoCAD;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using XMLTabulka1.API;
using XMLTabulka1.Trida;

namespace LibraryAplikace
{
    public partial class Zakazky
    {
        public static string JmenoProjektuDlePrvniho()
        {
           DataTable table = new DbfDotazySQL().JedenTezak(Sloupec.C_PROJ,InfoProjekt.CisloProjektu);
           List<TeZak> teZak = table.DataTabletoJson<TeZak>();
           //var Jeden = teZak.First();
           //InfoProjekt.Projekt = Jeden.NAZEV;
           return teZak.First().NAZ_PROJ;
        }

        /// <summary>
        /// Uložení dat do vlasního seznamu zakáze
        /// </summary>
        public static async Task<List<MojeZakazky>> MojeZakazkyAddJson()
        {
            await Task.Delay(1);
            if (!File.Exists(Cesty.PodporaDataJson))
            {
                List<MojeZakazky> moje = [];
                moje.SaveJson(Cesty.PodporaDataJson);
            }
            //if (!File.Exists(Cesty.PodporaDataXml))
            //{
            //    XDocument docnew = new("SEZNAM");
            //    XElement xElement = new("SEZNAM");
            //    docnew.Add(xElement);

            //    XmlSerializer xmlSerializer = new(typeof(List<MojeZakazky>));
            //    XmlWriter writer = docnew.CreateWriter();
            //    List<MojeZakazky> moje = new();
            //    xmlSerializer.Serialize(writer, moje);
            //    docnew.Save(Cesty.PodporaDataXml);
            //}

            List<MojeZakazky> mojelist = XMLTabulka1.Soubor.LoadFileJsonList<MojeZakazky>(Cesty.PodporaDataJson);
            MojeZakazky nove = new()
            {
                CisloProjektu = InfoProjekt.CisloProjektu,
                ProjektNazev = InfoProjekt.Projekt,
            };

            //kontrola existence prvku
            var kontrola = mojelist.FirstOrDefault(x => x == nove);
            if (kontrola == null)
            { 
                //pokud není bude přidán a ulozen
                mojelist.Add(nove);

                mojelist.SaveJson(Cesty.PodporaDataJson);
            }
            Task.Delay(1);
            return mojelist;
        }

        /// <summary>
        /// Přidání položky do souboru moje zakázky a s navratem upraveného seznamu
        /// </summary>
        public static async Task<List<MojeZakazky>> MojeZakazkyAdd()
        {

                var tezak = await API.APIJson<TeZak>($"api/Tezak/Projekt/Jedna/{InfoProjekt.CisloProjektu}");
                if (tezak == null) return [];
                InfoProjekt.CisloProjektu = tezak.C_PROJ;
                InfoProjekt.Projekt = tezak.NAZ_PROJ;
            return await MojeZakazkyAddJson();
        }

        /// <summary>
        /// Načtení souboru Cesty.PodporaDataJson do List<MojeZakazky>
        /// </summary>
        public static List<MojeZakazky> MojeZakazkyList()
        {
            //if (File.Exists(Cesty.PodporaDataXml) == false) return new();
            //List<MojeZakazky> Pole = MojeZakazkyListXML();
            //List<MojeZakazky> mojelist = new();
            var mojelist = XMLTabulka1.Soubor.LoadFileJsonList<MojeZakazky>(Cesty.PodporaDataJson);
            return mojelist;
        }

        /// <summary>
        /// Načtení List<MojeZakazky> ze soboru Cesty.PodporaDataXml
        /// </summary>
        public static List<MojeZakazky> MojeZakazkyListXML()
        {
            if (File.Exists(Cesty.PodporaDataXml) == false) return [];
            XDocument doc = XDocument.Load(Cesty.PodporaDataXml);
            List<MojeZakazky> Pole = XMLTabulka1.Soubor.LoadXML<MojeZakazky>(Cesty.PodporaDataXml);
            return Pole;
        }

        public static void MojeZakazkyOLD()
        {
            if (File.Exists(Cesty.PodporaDataXml))
            {
                XmlDocument doc = new() { XmlResolver = null };
                Stream fs = new FileStream(Cesty.PodporaDataXml, FileMode.Open);
                XmlReader rdr = XmlReader.Create(fs);
                doc.Load(rdr);
                rdr.Close();
                fs.Close();

                Dictionary<string, string> ListZak = [];
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

        //poznámka
        //lze použí starší spůsob vytvořebí XML dokumentu

        //XmlDocument docnew = new XmlDocument();

        //XmlNode rootNode = docnew.CreateElement("SEZNAM");
        //docnew.AppendChild(rootNode);

        //docnew.Save(Cesty.PodporaDataXml);

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
    }
}
