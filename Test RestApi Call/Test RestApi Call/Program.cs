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


using Newtonsoft.Json;



string url = "https://client.falixnodes.net/auth/login";
var client = new RestClient(url);
var request = new RestRequest();
request.Method = Method.Post;
request.AddBody("password", "z9g7%JZ$5XCflqePrQOd");
request.AddBody("email-address","p1pp1n3794@gmail.com");
request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36");
request.AddHeader("x-requested-with", "XMLHttpRequest");
Console.WriteLine(client.Execute(request).ResponseStatus);
Console.ReadLine();




string O6021 = "3v4kD62VpTAHAmMFW536hQ";
string W6021 = "4ErkQXvRN0b1aFS1z5Mi8t";
string O6022 = "50WxcFY2bcWYuX9BCJaPdN";
string W6022 = "2zxSJTBK2HyCJRTVP25HER";
string O6023 = "5WSH1baydm37mEQaVfpRtt";
string W6023 = "5QOQbbZbIcMrneDToD7Wgu";
string N6024 = "7S324dnACVjlg2TO53C4zj";
string Z6024 = "3XXJACAqHZCkhgn8PWZKaQ";
string N6025 = "4k8ThtXzLEc50LHowdGYan";
string Z6025 = "777yWSXa64OdBpQLzWUv5R";
string O6026 = "6e5JpYJ0g17nwS9vLFzgfp";
string W6026 = "2mezxE1lOSngRyXubh0wOf";
string O6027 = "5kYW7teouE8TFqmWwdsMz0";
string W6027 = "5UMschrkm2g3hsmEXuYXbq";
string O6028 = "6F1eDFmy5CQW9NFiM7JDSJ";
string W6028 = "76oDMkuQEqM90lYy2eskyv";

string[] lights = { O6021, O6022, O6023, N6024, N6025, O6026, O6027, O6028 };



int[] color = { 255, 0, 0 };

void Login()

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
