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
<<<<<<< HEAD
using Newtonsoft.Json;
=======
<<<<<<< HEAD
using Newtonsoft.Json;
=======
>>>>>>> 0a2e8f445cee5dd6e862eb667ec121347dc2c2ea
>>>>>>> 3ead3e226201900623f00999e70c7b2f05213640

string url = "https://aternos.org";
var client = new RestClient(url + "/panel/ajax/start.php?headstart=0&access-credits=0&SEC=dxp9m8wxivc00000%3Ag3a1venqxz800000&TOKEN=6xoIfAOpIaYC6qyGngXV");
var request = new RestRequest();

login("", "");

Console.ReadLine();

<<<<<<< HEAD
void login(string email, string pass)
=======
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
<<<<<<< HEAD


int[] color = { 255, 0, 0 };

while (true)
{
    offset(color, 151);
    Console.WriteLine($"R:{color[0]}  G:{color[1]}   B:{color[2]}");
    Console.ReadKey();
}

//Console.WriteLine(GetAsset(O6021));
//SetColdBrightness(O6021,30);
//SetWarmBrightness(O6021,30);
//ChangeColor(O6021, new int[] { 0, 0, 0 });
//Reset();
//FadeAllLights(lights, new int[] { 255, 0, 0 });

=======


//ChangeColor(O6021, new int[] { 0, 0, 0 });
Reset();
//FadeAllLights(lights, new int[] { 255, 0, 0 });

>>>>>>> 0a2e8f445cee5dd6e862eb667ec121347dc2c2ea
//FadeLight(lights[0]);
//Reset();
void FadeAllLights(string[] lights, int[] color)
>>>>>>> 3ead3e226201900623f00999e70c7b2f05213640
{
    var client = new RestClient(url + "/panel/ajax/account/login.php");
    var request = new RestRequest();
    request.AddHeader("origin", "https://aternos.org");
    request.AddHeader("referer", "https://aternos.org/go/");
    request.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36");
    request.AddHeader("x-requested-with", "XMLHttpRequest");
    request.Method = Method.Post;
<<<<<<< HEAD
    request.AddBody("{user}");
    Console.WriteLine(client.Execute(request).ResponseStatus);
}


=======
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
    var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid);
    var request = new RestRequest();
    request.Method = Method.Get;
    request.AddHeader("authorization", "Bearer " + token);
    RestResponse response = client.Execute(request);
    try
    {
        LightModel lightModel = JsonConvert.DeserializeObject<LightModel>(response.Content);
        

    }
    catch
    {

    }
    return response.Content.ToString();
}

async void ChangeColor(string assetid, int[] color)
{
    var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid + "/attribute/colourRgbLed");
    var request = new RestRequest();
    request.Method = Method.Put;
    request.AddBody(color);
    request.AddHeader("authorization", "Bearer " + token);
    client.ExecuteAsync(request);
<<<<<<< HEAD
    return;
}

void SetWarmBrightness(string assetid, int brightness)
{
    var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid + "/attribute/brightnessWhiteWarmLed");
    var request = new RestRequest();
    request.Method = Method.Put;
    request.AddBody(brightness);
    request.AddHeader("authorization", "Bearer " + token);
    Console.WriteLine(client.Execute(request).Content);
=======
>>>>>>> 0a2e8f445cee5dd6e862eb667ec121347dc2c2ea
    return;
}

void SetColdBrightness(string assetid, int brightness)
{
    var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid + "/attribute/brightnessWhiteColdLed");
    var request = new RestRequest();
    request.Method = Method.Put;
    request.AddBody(brightness);
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

void FadeLight(string assetid)
{
    int[] Color = new int[] { 255, 0, 0 };
    for (int i = 0; i < 3; i++)
    {
        int x = i + 1;
        if (x == 3) x = 0;

        for (int j = 0; j <= 255; j = j + 5)
        {
            Thread.Sleep(500);
            Color[x] = j;
            ChangeColor(assetid, Color);
        }
        token = getToken();
        for (int j = 255; j >= 0; j = j - 5)
        {
            Thread.Sleep(500);
            Color[i] = j;
            ChangeColor(assetid, Color);
        }
        token = getToken();
    }
}

int[] offset(int[] color, int offset)
{
    for (int i = 0; i < color.Length; i++)
    {
        if (color[i] == 255)
        {
            int after = i+ 1;
            int before = i-1;
            if (after == color.Length) after = 0;
            if (before == -1) before = color.Length -1;

<<<<<<< HEAD
            if (color[before] == 0 && color[after] <= color[i])
=======
            if (color[before] == 0 && color[after] < color[i])
>>>>>>> 0a2e8f445cee5dd6e862eb667ec121347dc2c2ea
            {
                color[after] += offset;
                if (color[after] > 255)
                {
                    color[i] -= color[after] - 255;
                    color[after] = 255;
                }
            }
            else
            {
                color[before] -= offset;
<<<<<<< HEAD
                if (color[before] <= 0)
=======
                if (color[before] < 0)
>>>>>>> 0a2e8f445cee5dd6e862eb667ec121347dc2c2ea
                {
                    color[after] -= color[before];
                    color[before] = 0;
                }
            }

            break;
        }
    }
    return color;
}
void Reset()
{
    TurnOff(broeinestid);
    ChangeColor(broeinestid, new int[] {0,0,0});
}
>>>>>>> 3ead3e226201900623f00999e70c7b2f05213640
