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

        private static string url = "https://api.pubg.com/shards/steam/";
        private static string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiJmOGZhN2NlMC1hNTk5LTAxM2ItMGFiYS02ZDdmOTc3MzllOTciLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNjc4OTEwNDM1LCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6Ii02NjdhOTcwNC02Y2ZiLTQ1ZjYtYmE0OS0yN2UyNzI2NzIyMGEifQ.dbXPKz-5Wml2Lpf9KRYUo6G8GyQzqNPqKeDEp6Uvmoc";

        static void Main(string[] args)
        {
            //Console.WriteLine(getplayerid("P1PP1N001"));
            Console.WriteLine(GetMatchStats("405050b1-6a5f-4b89-ada4-1159406b1973"));
            Console.ReadKey();
        }


        private static string getplayerid(string name)
        {
            var client = new RestClient(url + "players?filter[playerNames]=P1PP1N001");
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("authorization", "Bearer " + token);
            request.AddHeader("Accept", "application/vnd.api+json");
            RestResponse response = client.Execute(request);
            string content = response.Content.ToString();
            return content;
        }

        private static string GetWeaponStats()
        {
            var client = new RestClient(url+ "players/account.d1fa5d0376484529aeff0c893c6b0bbd/weapon_mastery");
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("authorization", "Bearer " + token);
            request.AddHeader("Accept", "application/vnd.api+json");
            RestResponse response = client.Execute(request);
            string content = response.Content.ToString();
            return content;
        }
        private static string GetMatchStats(string id)
        {
            var client = new RestClient(url + "matches/" + id);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("authorization", "Bearer " + token);
            request.AddHeader("Accept", "application/vnd.api+json");
            RestResponse response = client.Execute(request);
            string content = response.Content.ToString();
            return content;
        }

        private static string GetSeasonStats()
        {
            var client = new RestClient(url + "players/account.d1fa5d0376484529aeff0c893c6b0bbd/seasons/division.bro.official.pc-2018-20");
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("authorization", "Bearer " + token);
            request.AddHeader("Accept", "application/vnd.api+json");
            RestResponse response = client.Execute(request);
            string content = response.Content.ToString();
            return content;
        }
        private static string GetSeasons()
        {
            var client = new RestClient(url + "seasons");
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("authorization", "Bearer " + token);
            request.AddHeader("Accept", "application/vnd.api+json");
            RestResponse response = client.Execute(request);
            string content = response.Content.ToString();
            return content;
        }
    }
}
