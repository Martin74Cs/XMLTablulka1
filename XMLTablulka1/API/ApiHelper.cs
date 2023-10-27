using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace XMLTabulka1.API
{
    /// <summary>
    /// Připojení do restApi na můj server BlazorElektro
    /// </summary>
    public class ApiHelper : HttpClient
    {
        public ApiHelper()
        {
            BaseAddress = new Uri(IP);
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //ApiHelper = new HttpClient();
        }

        public static HttpClient ApiClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(IP);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        public static string IP
        {
            get
            {
                if (System.Environment.MachineName == "KANCELAR")
                    return "http://192.168.1.210/";
                else
                    return "http://10.55.1.100/";
            }
        }
    }
}
