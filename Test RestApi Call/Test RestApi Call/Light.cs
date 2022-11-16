using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

    public class Light
    {
        public string Id { get; private set; }
    
        public string Name { get; private set; }
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }
    [JsonProperty(PropertyName = "mastHeight.value")]
    public int Height { get; private set; }
        public int Angle { get; private set; }

        public int[] Color { get; private set; }
        public int WarmBrightness { get; private set; }
        public int ColdBrightness { get; private set; }
        public bool OnOff { get; private set; }
    
        public Thread Thread { get; private set; }
        private bool Working { get; set; }
        private bool Stop { get; set; }


        private string realm = "strijp";
        private string url = "https://staging.strijp.openremote.app";

        public Light(string id, string name, double longitude, double latitude, int height)
        {
            Id = id;
            Name = name;
            Longitude = longitude;
            Latitude = latitude;

            Color = new int[] { 0, 0, 0 };
            WarmBrightness = 30;
            ColdBrightness = 30;
            OnOff = false;

            Working = false;
            Stop = false;
            Height = height;
        }
    }
