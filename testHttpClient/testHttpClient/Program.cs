using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace testHttpClient
{
    internal class Program
    {

        private static string url = "https://staging.strijp.openremote.app";
        private static string realm = "strijp";
        private static string sectret = "TsuunSkVxfmSvkDOXpaBQygcW6Lpn8RN";
        private static string client_id = "fontys";


        static void Main(string[] args)
        {
            Console.WriteLine(getToken());
            Console.ReadKey();
        }


        private static string getToken()
        {
            var client = new RestClient(url + "/auth/realms/" + realm + "/protocol/openid-connect/token");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=" + client_id + "&client_secret=" + sectret, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            string content = response.Content.ToString();
            string[] p = content.Split(':');
            p = p[1].Split(',');
            return p[0].Split('"')[1];
        }
    }
}
