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
string lamp1 = "3v4kD62VpTAHAmMFW536hQ";
string lamp2 = "4ErkQXvRN0b1aFS1z5Mi8t";


//Reset();

int[] Color = new int[] { 255, 0, 0 };
for(int i = 0; i < 3; i++)
{
    int x = i + 1;
    if (x == 3) x = 0;
    
    for(int j = 0; j <= 255; j = j + 5)
    {
        Thread.Sleep(1000);
        Color[x] = j;
        ChangeColor(lamp1, Color);
    }
    token = getToken();
    for (int j = 255; j >= 0; j = j -5)
    {
        Thread.Sleep(1000);
        Color[i] = j;
        ChangeColor(lamp1, Color);
    }
    token = getToken();


}

//Console.WriteLine(GetAsset(lamp1));
//Console.WriteLine( "\n" + GetAsset(lamp2));


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

void ChangeColor(string assetid, int[] color)
{
    var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid + "/attribute/colourRgbLed");
    var request = new RestRequest();
    request.Method = Method.Put;
    request.AddBody(color);
    request.AddHeader("authorization", "Bearer " + token);
    Console.WriteLine(client.Execute(request).Content);
    return;
}

void TurnOn(string assetid)
{
    var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid + "/attribute/onOff");
    var request = new RestRequest();
    request.Method = Method.Put;
    request.AddBody(true);
    request.AddHeader("authorization", "Bearer " + token);
    Console.WriteLine(client.Execute(request).Content);
    return;
}

void TurnOff(string assetid)
{
    var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid + "/attribute/onOff");
    var request = new RestRequest();
    request.Method = Method.Put;
    request.AddBody(false);
    request.AddHeader("authorization", "Bearer " + token);
    Console.WriteLine(client.Execute(request).Content);
    return;
}

void Reset()
{
    TurnOff(broeinestid);
    ChangeColor(broeinestid, new int[] {0,0,0});
}