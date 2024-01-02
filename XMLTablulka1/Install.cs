using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XMLTabulka1;
using XMLTabulka1.Trida;

namespace Instal
{
    public class Install
    {
        public static async Task<List<Upload>> GetSearchAsync(string FileName)
        {
            var http = new HttpApi();
            var response = await http.GetFromJsonAsync<List<Upload>>($"/api/File/Search/{FileName}");
            if (response == null)
                return [];
            return response;
        }

        /// <summary>
        /// Nahraní souboru na WEB
        /// </summary>
        /// <param name="file"></param>
        /// 
        public static async Task<string> Upload(string file)
        {
            if (!File.Exists(file))
            { Console.WriteLine("Soubor nebyl nalezen"); return ""; }
              
            var fileStream = System.IO.File.OpenRead(file);
            var streamContent = new StreamContent(fileStream);
            var content = new MultipartFormDataContent();

            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MediaTypeNames.Application.Zip);

            //fileNames.Add(file.Name);
            content.Add(content: streamContent, name: "\"files\"", fileName: Path.GetFileName(file));

            var http = new HttpApi();
            var response = await http.PostAsync("/api/File", content);
            //zpětné načtení souboru který byl uložen
            var newUploadResult = await response.Content.ReadFromJsonAsync<List<Upload>>();
            if (newUploadResult != null)
            {
                var uploads = new List<Upload>();
                //uploads = uploads.Concat(newUploadResult).ToList();
                uploads = [.. uploads, .. newUploadResult];
                return uploads.First().StoredFileName;
            }
            return null;
        }

        /// <summary>
        /// Download zadaného souboru
        /// </summary>
        public static async Task<bool> Download(string StoredFileName, string Uložit)
        {
            var http = new HttpApi();
            var response = await http.GetAsync($"/api/File/{StoredFileName}");
            if (response.IsSuccessStatusCode)
            {
                //cesta dočasného uložení
                string zipFilePath = Path.Combine(Path.GetTempPath(), "temp.zip");

                //Stažení proudu dat jako soubor zip
                byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

                //vytvoření souboru z proudu dat
                File.WriteAllBytes(zipFilePath, fileBytes);

                //Extrahování souborů z archivu, true - přepsání souborů,
                //System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, Uložit,true);

                //sleduj zipování 
                if(!SledujZip(zipFilePath, Uložit))
                    return false;

                //Smazaní dočasného uložení
                if(File.Exists(zipFilePath))
                    File.Delete(zipFilePath);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool SledujZip(string zipFilePath, string Uložit)
        {
            //try
            //{
                using ZipArchive archive = ZipFile.OpenRead(zipFilePath);
                int totalCount = archive.Entries.Count;
                //int currentCount = 0;

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Aktualizace ProgressBar
                    //currentCount++;
                    //double progress = (double)currentCount / totalCount * 100;
                    //UpdateProgressBar(progress);

                    // Extrahování každé položky
                    entry.ExtractToFile(Path.Combine(Uložit, entry.FullName), true);
                }
                return true;
                //Console.WriteLine("Extrakce dokončena.");
            //}
            //catch 
            //{
            //    return false;
            //    //Console.WriteLine($"Chyba při extrakci: {ex.Message}");
            //}
        }


        public static async Task<ProgramInfo> ManifestDownloadAsync()
        {
            var http = new HttpApi();
            var response = await http.GetAsync($"/api/File/Manifest");
            if (response.IsSuccessStatusCode)
            {
                var fileStream = response.Content.ReadAsStream();
                ProgramInfo myData = JsonSerializer.Deserialize<ProgramInfo>(fileStream);
                return myData;
            }
            return null;

        }

        public static async Task<bool> ManifestUploadAsync(string Verze)
        {
            string Cesta = Path.Combine(Cesty.Manifest, "Manifest.txt");

            //ProgramInfo program = new() { Version = Verze, ReleaseDate = DateTime.Now.ToString(), DownloadUrl = "192.168.1.210" };
            ProgramInfo program = new() { Version = Verze, ReleaseDate = DateTime.Now, DownloadUrl = HttpApi.IP() };
            string Json = System.Text.Json.JsonSerializer.Serialize(program);

            byte[] jsonBytes = Encoding.UTF8.GetBytes(Json);
            var memoryStream = new MemoryStream(jsonBytes);

            //var fileStream = System.IO.File.OpenRead(Cesta);
            var streamContent = new StreamContent(memoryStream);
            var content = new MultipartFormDataContent();

            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MediaTypeNames.Text.Plain);

            //fileNames.Add(file.Name);
            content.Add(content: streamContent, name: "\"files\"", fileName: Path.GetFileName(Cesta));

            var http = new HttpApi();
            var response = await http.PostAsync("/api/File/Manifest", content);
            var newUploadResult = await response.Content.ReadAsStringAsync();
            if (newUploadResult != null)
            {
                return true;
            }
            return false;
        }


    }

    public class HttpApi : HttpClient
    {
        public HttpApi()
        {
            BaseAddress = IP();
        }

        public static Uri IP()
        {
            if (Environment.MachineName.Equals("KANCELAR", StringComparison.InvariantCultureIgnoreCase))
                return new Uri("http://192.168.1.210/");
            else
                return new Uri("http://10.55.1.100/");
            //BaseAddress = new Uri("https://localhost:7208/");
        }
    }
}
