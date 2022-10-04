﻿using RestSharp;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LightControler
{
    public class LightController
    {
        int requests = 0;
        Stopwatch Stopwatch = new Stopwatch();

        public bool Stop = false;
        string realm = "strijp";
        string sectret = "TsuunSkVxfmSvkDOXpaBQygcW6Lpn8RN";
        string client_id = "fontys";
        string url = "https://staging.strijp.openremote.app";
        string token;
        string broeinestid = "3lGbluNj94x8A7b3NFieiy";
        int[] ints;

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
        public string[] lights;

        public LightController()
        {
            lights = new string[] { O6021, O6022, O6023, N6024, N6025, O6026, O6027, O6028 };
            ints = offset(new int[] { 250, 255, 0 }, 5);
            token = getToken();
        }

        public async Task FadeAllLights(string[] lights)
        {
            int[] color = new int[] { 255, 0, 0 };
            while (true)
            {
                if (Stop)
                {
                    Stop = false;
                    break;
                }
                if (Stopwatch.ElapsedMilliseconds > 55000)
                {
                    Stopwatch.Restart();
                    token = getToken();
                }
                color = offset(color, 10);
                foreach (string light in lights)
                {
                    Console.WriteLine(DateTime.Now);
                    ChangeColor(light, offset(color, 1536 / lights.Length));
                }
            }
        }

        string getToken()
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

        public string GetAsset(string assetid)
        {
            var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("authorization", "Bearer " + token);
            RestResponse response = client.Execute(request);
            return response.Content.ToString();
        }

        public async void ChangeColor(string assetid, int[] color)
        {
            var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid + "/attribute/colourRgbLed");
            var request = new RestRequest();
            request.Method = Method.Put;
            request.AddBody(color);
            request.AddHeader("authorization", "Bearer " + token);
            client.ExecuteAsync(request);
            return;
        }

        public void TurnOn(string assetid)
        {
            var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid + "/attribute/onOff");
            var request = new RestRequest();
            request.Method = Method.Put;
            request.AddBody(true);
            request.AddHeader("authorization", "Bearer " + token);
            Console.WriteLine(client.Execute(request).Content);
            return;
        }

        public void TurnOff(string assetid)
        {
            var client = new RestClient(url + "/api/" + realm + "/asset/" + assetid + "/attribute/onOff");
            var request = new RestRequest();
            request.Method = Method.Put;
            request.AddBody(false);
            request.AddHeader("authorization", "Bearer " + token);
            Console.WriteLine(client.Execute(request).Content);
            return;
        }

        public async Task FadeLight(string assetid)
        {
            int[] Color = new int[] { 255, 0, 0 };
            while (true)
            {
                offset(Color, 10);
                Thread.Sleep(100);
                ChangeColor(assetid, Color);
                if (Stop)
                {
                    Stop = false;
                    break;
                }
            }
        }

        int[] offset(int[] color, int offset)
        {
            for (int i = 0; i < color.Length; i++)
            {
                if (color[i] == 255)
                {
                    int after = i + 1;
                    int before = i - 1;
                    if (after == color.Length) after = 0;
                    if (before == -1) before = color.Length - 1;

                    if (color[before] == 0 && color[after] < color[i])
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
                        if (color[before] < 0)
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
        public void Reset()
        {
            TurnOff(broeinestid);
            ChangeColor(broeinestid, new int[] { 0, 0, 0 });
        }
    }
}