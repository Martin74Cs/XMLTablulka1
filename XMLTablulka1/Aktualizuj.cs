using System.Data;
using XMLTabulka1.API;
using XMLTabulka1.Trida;

namespace XMLTabulka1
{
    public class Aktualizuj
    {
        /// <summary>
        /// Aktualizuje podpůrné soubory pro databazi
        /// </summary>
        public static void AktualizujData()
        {
            //Načti seznam projektů Cesty.CislaProjektuTxt.
            if (!File.Exists(Cesty.CislaProjektuTxt))
            {
                string[] Hromada = new DbfDotazySQL().SeznamJeden(VyberSloupec.C_PROJ);
                Hromada.SaveTXT(Cesty.CislaProjektuTxt);
                InfoProjekt.CisloProjektu = Hromada.Last();
            }

            if (!File.Exists(Cesty.NazevProjektuTxt))
            {
                string[]  Hromada = new DbfDotazySQL().SeznamJeden(VyberSloupec.NAZ_PROJ);
                Hromada.SaveTXT(Cesty.NazevProjektuTxt);
            }

            if (!File.Exists(Cesty.CislaDokumentuXML) && !File.Exists(Cesty.CislaDokumentuJson))
            {
                string Querry = "SELECT DISTINCT PCC,TD,PCDOC FROM TEZAK ORDER BY PCC,TD,PCDOC";
                DataSet data = new Dbf(Cesty.SouborTezakDbf).Pripoj(Querry);
                data.Tables[0].WriteXml(Cesty.CislaDokumentuXML);
                List<TeZak> teZaks = data.Tables[0].DataTabletoJson<TeZak>();
                teZaks.SaveJson(Cesty.CislaDokumentuJson);
            }
        }

        public static async Task<string[]> RestApiListZakazky()
        {
            List<TeZakHodnota> data = await API.API.APIJsonList<TeZakHodnota>("api/tezak/ListZakazky");
            string[] Hromada = data.Select(x => x.Hodnota).ToArray();
            return Hromada;
        }

        public static string DatumTeZak()
        {
            if (File.Exists(Cesty.SouborTezakDbf))
            {
                DateTime date = File.GetLastWriteTime(Cesty.SouborDbf);
                return "Databaze v PC je ze dne : " + date.ToString("dd/MM/yyyy");
            }
            return "Databaze v PC neexistuje.";
        }

        public static async Task SmazatSoubory(string Cesta)
        {
            if (File.Exists(Cesta))
            {
                Cesty.ZdrojDbf = Cesta;
                if(File.Exists(Cesty.SouborTezakDbf)) File.Delete(Cesty.SouborTezakDbf);
                if (File.Exists(Cesty.CislaProjektuTxt)) File.Delete(Cesty.CislaProjektuTxt);
                if (File.Exists(Cesty.NazevProjektuTxt)) File.Delete(Cesty.NazevProjektuTxt);
                if (File.Exists(Cesty.CislaDokumentuJson)) File.Delete(Cesty.CislaDokumentuJson);
                if (File.Exists(Cesty.CislaDokumentuXML)) File.Delete(Cesty.CislaDokumentuXML);
            }

            //paralelní spuštění metody. Hlavní metoda pokracuje
            //Kopie();

            //paralelní spuštění metody. Hlavní metoda čeká na dokončení metody
            await Kopie();
            Console.WriteLine("pokracuj");
        }

        public static async Task Kopie()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            File.Copy(Cesty.ZdrojDbf, Cesty.SouborTezakDbf);
        }

    }
}
