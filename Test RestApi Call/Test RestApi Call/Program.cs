using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text;

string realm = "strijp";
string sectret = "TsuunSkVxfmSvkDOXpaBQygcW6Lpn8RN";
string client_id = "fontys";
string url = "https://staging.strijp.openremote.app";
string token = getToken();
string broeinestid = "3lGbluNj94x8A7b3NFieiy";
string lamp = "3v4kD62VpTAHAmMFW536hQ";

PutAttribute("3v4kD62VpTAHAmMFW536hQ", "#100000");
Console.WriteLine(GetAsset(lamp));


string getToken()
{
    var client = new RestClient(url + "/auth/realms/" + realm + "/protocol/openid-connect/token");
    var request = new RestRequest();
    request.Method = Method.Post;
    request.AddHeader("cache-control", "no-cache");
    request.AddHeader("content-type", "application/x-www-form-urlencoded");
    request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id="+ client_id + "&client_secret=" + sectret, ParameterType.RequestBody);
    RestResponse response = client.Execute(request);
    string content = response.Content.ToString();
    string[] p = content.Split(':');
    p = p[1].Split(',');
    return p[0].Split('"')[1];
}

string GetAsset(string assetid)
{
    //Console.WriteLine( '\n' + token + '\n');
    var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid);
    var request = new RestRequest();
    request.Method = Method.Get;
    request.AddHeader("authorization", "Bearer " + token);
    RestResponse response = client.Execute(request);
    return response.Content.ToString();
}

void PutAttribute(string assetid, string color)
{
    var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid + "/attribute/colourRgbLed");
    var request = new RestRequest();

    var requestContent = new StringContent("\"value\":\"#100000\"", Encoding.UTF8, "application/json");

    request.Method = Method.Put;
    request.AddBody(requestContent);
    request.AddHeader("authorization", "Bearer " + token);
    Console.WriteLine(client.Execute(request).Content);
    return;
}