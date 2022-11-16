using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Net;

namespace HttpClientTest
{
    internal class Program
    {
        private static string url = "https://staging.strijp.openremote.app";
        private static string realm = "strijp";
        private static string sectret = "TsuunSkVxfmSvkDOXpaBQygcW6Lpn8RN";
        private static string client_id = "fontys";
        private static bool wait = true;

        static void Main(string[] args)
        {
            
            GetToken();
            while (wait)
            {

            }
            Console.ReadKey();
        }

        public static async Task<string> GetToken()
        {
            string token = "";
            //var parameters = new Dictionary<string, string> { { "grant_type", "client_credentials" },{ "client_id", client_id }, {"client_secret",sectret }};
            var parameters = new Dictionary<string, string> { { "application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=" + client_id + "&client_secret=" + sectret } };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("content-type", "application/x-www-form-urlencoded");
            //httpClient.DefaultRequestHeaders.Add("cache-control", "no-cache");
            var response = await httpClient.PostAsync(url, encodedContent);

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
            token = response.ToString();
            
            wait = false;
            return token;
        }
    }
}
