using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace XMLTabulka1.API
{
    /// <summary>
    /// Připojení do restApi na můj server BlazorElektro
    /// </summary>
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void Init()
        {
            ApiClient = new() { BaseAddress = UriApiBaseIP };
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static Uri UriApiBaseIP => new(IP);

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
