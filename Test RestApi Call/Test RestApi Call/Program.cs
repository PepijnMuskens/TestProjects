using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

string url = "https://aternos.org";
var client = new RestClient(url + "/panel/ajax/start.php?headstart=0&access-credits=0&SEC=dxp9m8wxivc00000%3Ag3a1venqxz800000&TOKEN=6xoIfAOpIaYC6qyGngXV");
var request = new RestRequest();

login("", "");

Console.ReadLine();

void login(string email, string pass)
{
    var client = new RestClient(url + "/panel/ajax/account/login.php");
    var request = new RestRequest();
    request.AddHeader("origin", "https://aternos.org");
    request.AddHeader("referer", "https://aternos.org/go/");
    request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36");
    request.AddHeader("x-requested-with", "XMLHttpRequest");
    request.Method = Method.Post;
    request.AddBody("{user}");
    Console.WriteLine(client.Execute(request).ResponseStatus);
}


