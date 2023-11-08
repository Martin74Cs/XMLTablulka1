using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Xml.Linq;
using XMLTabulka1.Trida;

namespace XMLTabulka1.API
{
    public static class API
    {
        /// <summary> Načte Data tridy T z adresy api/T a provede serializaci </summary>
        /// 

        public static async Task<List<T>> LoadJsonAPI<T>(string API = "") where T : class
        {
            //vytvožení RestAPI z nazvu třidy
            if (string.IsNullOrEmpty(API)) API = "api/" + typeof(T).ToString().Split('.').Last();
            HttpClient client = new ApiHelper();
            var response = await new ApiHelper().GetFromJsonAsync<List<T>>(API);
            return response;
        }

        public static async Task<List<T>> LoadAPI<T>(string API = "") where T : class
        {
            //vytvožení RestAPI z nazvu třidy
            if (string.IsNullOrEmpty(API)) API = "api/" + typeof(T).ToString().Split('.').Last();

            HttpClient client = new ApiHelper();
            HttpResponseMessage response = await new ApiHelper().GetAsync(API);
            if (response.IsSuccessStatusCode)
            {
                //obsah odpovědi převede na seznam objektů typu Trida
                var result = await response.Content.ReadFromJsonAsync<List<T>>();
                return result;
            }
            return new List<T>();
        }

        /// <summary>
        /// Načti soubor trida.json a ulož do databaze PostAsync api/trida
        /// </summary>       
        public static async Task<bool> FileMaterial_to_AddApiMaterial<T>() where T : class
        {
            string trida = typeof(T).ToString().Split('.').Last();
            string Api = "api/" + trida;
            List<T> material = Soubory.LoadJsonList<T>(Path.Combine(Cesty.AdresarSpusteni, trida + ".json"));
            if (material == null && material.Count < 1) return false;
            HttpClient client = new ApiHelper();
            foreach (T item in material)
            {
                StringContent textq = item.AsJson();
                //textq.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //HttpResponseMessage result = await ApiHelper.ApiClient.PostAsync(Api, textq);
                HttpResponseMessage result = await client.PostAsync(Api, textq);
            }
            return true;
        }

        /// <summary>
        /// Načti RestApi a ulož soubor trida "T.json".
        /// </summary> 
        public static async Task<List<T>> Saving<T>(string API = "")
        {
            Type type = typeof(T);
            string trida = type.ToString().Split('.').Last();
            if (string.IsNullOrEmpty(API)) API = "api/" + trida;
            string soubor = trida + ".json";
            //HttpClient client = new ApiHelper();
            HttpResponseMessage response = await new ApiHelper().GetAsync(API);
            if (response.IsSuccessStatusCode)
            {
                List<T> trubka = await response.Content.ReadFromJsonAsync<List<T>>();
                string jsonString = System.Text.Json.JsonSerializer.Serialize(trubka);
                string cesta = Path.Combine(Cesty.AdresarSpusteni, soubor);
                System.IO.File.WriteAllText(cesta, jsonString);
                return trubka;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public static async Task<List<T>> APISaveDatabase<T>(string API, TeZak tezak)
        {
            StringContent textq = tezak.AsJson();
            //textq.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //var response = await http.PostAsJsonAsync($"api/TeZak/TaskAll", Tezak);
            var response = await new ApiHelper().PostAsync(API, textq);
            List<T> dils = await response.Content.ReadFromJsonAsync<List<T>>();
            return dils;
        }

    }

    /// <summary> Rozšíření </summary>
    public static class Extensions
    {
        public static StringContent AsJson(this object o)
            => new(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
    }

    public static class Soubory
    {
        // <summary> serializace třídy a uložení trídy do souboru dle cesty</summary>
        public static void SaveJson<T>(this List<T> moje, string cesta)
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(moje);
            System.IO.File.WriteAllText(cesta, jsonString);
        }

        /// <summary>Načte soubor z cesta a deserializuje dle T třídy. Pozor třída nemsmí mýt vnořené třídy asi generika </summary>
        public static List<T> LoadJsonList<T>(string cesta) where T : class
        {
            if (System.IO.File.Exists(cesta))
            {
                string jsonString = System.IO.File.ReadAllText(cesta);
                List<T> moje = System.Text.Json.JsonSerializer.Deserialize<List<T>>(jsonString)!;
                return moje;
            }
            return new();
        }

        //od umělé inteligence
        public static string JsonToXml(string json)
        {
            // Parsuje JSON řetězec na objekt JObject
            JObject jObject = JsonConvert.DeserializeObject<JObject>(json);

            // Vytvoří se kořenový element XML dokumentu
            var xmlRoot = new XElement("Root");

            // Rekurzivně projde objekt JObject a přidá jeho prvky do XML
            AddJsonToXml(jObject, xmlRoot);

            // Vrátí se XML řetězec
            return xmlRoot.ToString();
        }

        private static void AddJsonToXml(JObject jObject, XElement parent)
        {
            foreach (var property in jObject.Properties())
            {
                var name = property.Name;
                var value = property.Value;

                var element = new XElement(name);

                if (value.Type == JTokenType.Object)
                {
                    // Pokud je hodnota objekt, rekurzivně se volá AddJsonToXml
                    AddJsonToXml((JObject)value, element);
                }
                else if (value.Type == JTokenType.Array)
                {
                    // Pokud je hodnota pole, rekurzivně se volá AddJsonToXml pro každý prvek v poli
                    foreach (var arrayValue in value.Children())
                    {
                        var arrayElement = new XElement("item");
                        AddJsonToXml((JObject)arrayValue, arrayElement);
                        element.Add(arrayElement);
                    }
                }
                else
                {
                    // Jinak se přidá hodnota jako textový element
                    element.Value = value.ToString();
                }

                parent.Add(element);
            }
        }
        public static void Pruzkumnik()
        {
            try
            {
                Process.Start("explorer.exe", Cesty.AdresarSpusteni);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Došlo k chybě: " + ex.Message);
            }
        }
    }

    public static class Cesty
    {
        ///<summary>
        /// soubor spuštení exe
        /// </summary>
        public static string SouborExe => System.Reflection.Assembly.GetExecutingAssembly().Location;

        ///<summary>
        /// adresar spušteni
        /// </summary>
        public static string AdresarSpusteni => Path.GetDirectoryName(SouborExe);
    }

}
