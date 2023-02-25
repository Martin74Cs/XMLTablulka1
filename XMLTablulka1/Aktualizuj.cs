using System.Data;

namespace XMLTabulka1
{
    public class Aktualizuj
    {
        /// <summary>
        /// Aktualizuje podpůrné soubory pro databazi
        /// </summary>
        public void AktualizujData()
        {
            string[] Hromada = new SQLDotazy().SeznamJeden(VyberSloupec.C_PROJ);
            Hromada.SaveTXT(Cesty.CislaProjektuTxt);

            Hromada = new SQLDotazy().SeznamJeden(VyberSloupec.NAZ_PROJ);
            Hromada.SaveTXT(Cesty.NazevProjektuTxt);

            string Querry = "SELECT DISTINCT PCC,TD,PCDOC FROM TEZAK ORDER BY PCC,TD,PCDOC";
            DataSet data = Dbf.Pripoj(Querry);
            data.Tables[0].WriteXml(Cesty.CislaDokumentu);
        }

        public string DatumTeZak()
        {
            if (File.Exists(Cesty.SouborDbf))
            {
                DateTime date = File.GetLastWriteTime(Cesty.SouborDbf);
                return "Databaze v PC je ze dne : " + date.ToString("dd/MM/yyyy");
            }
            return "Databaze v PC neexistuje.";
        }

        public void Smazat()
        {
            if (File.Exists(Cesty.ZdrojDbf))
            { 
                //if(File.Exists(Cesty.SouborDbf)) File.Delete(Cesty.SouborDbf);
                //if (File.Exists(Cesty.CislaProjektuTxt)) File.Delete(Cesty.CislaProjektuTxt);
                //if (File.Exists(Cesty.NazevProjektuTxt)) File.Delete(Cesty.NazevProjektuTxt);
                Thread.Sleep(10000);
            }

            Task kop = new Task(async () => await Kopie());
            kop.Start();

            kop.Wait();
            Console.WriteLine("Bylo dokonceno kopirovaní");
            kop.Dispose();
        }

        public async Task Kopie()
        { 
           File.Copy(Cesty.ZdrojDbf, Cesty.SouborDbf);
        }

    }
}
