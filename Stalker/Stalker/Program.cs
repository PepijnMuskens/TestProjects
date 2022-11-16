using System;
using System.Collections.Generic;
using System.Data;
using System.Device.Location;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Stalker
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            CLocation myLocation = new CLocation();
            myLocation.GetLocationEvent();
            GeoCoordinate geoCoordinate = new GeoCoordinate();
            Console.WriteLine("Enter 1 to quit.");
            
            while (true)
            {
                string text = Console.ReadLine();
                if(text == "1") break;
                else if(text == "2")
                {
                    Console.WriteLine("Enter Distance in Meters: ");
                    string dist = Console.ReadLine();
                    try
                    {
                        Console.WriteLine("Color: " + DistancetoColor(Convert.ToInt32(dist), new int[] { 255, 0, 100 }));
                    }
                    catch
                    {
                        Console.WriteLine("Enter a valid number");
                    }
                }
                else if(text == "3")
                {
                    double lon = myLocation.natlabcoordinets[0];
                    double lat = myLocation.natlabcoordinets[1];
                    Console.WriteLine("Angle: " + myLocation.CalculateAngle(lon, lat, lon + 1, lat + 1).ToString());
                }
                else if(myLocation.url != "")
                {
                    Process p = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo(myLocation.url);
                    p.StartInfo = psi;
                    p.Start();
                }
            }

            string DistancetoColor(double meters, int[] rgb)
            {
                if (meters > 100) return "000000";
                for (int i = 0; i < 3; i++)
                {
                    rgb[i] = (int)((double)rgb[i] * ((100 - (int)meters) * 0.01));
                }
                return string.Format("{0:X2}{1:X2}{2:X2}", rgb[0], rgb[1], rgb[2]);
            }
        }    
    }
    class CLocation
    {
        GeoCoordinateWatcher watcher;
        public string url = "";
        public double[] natlabcoordinets = { 51.4444723, 5.4562081};
        public void GetLocationEvent()
        {  
            this.watcher = new GeoCoordinateWatcher();
            watcher.Start();
            Console.WriteLine(watcher.Position.Location);
            this.watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            bool started = this.watcher.TryStart(false, TimeSpan.FromMilliseconds(2000));
            if (!started)
            {
                Console.WriteLine("GeoCoordinateWatcher timed out on start.");
            }
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            PrintPosition(e.Position.Location.Latitude, e.Position.Location.Longitude);
        }

        void PrintPosition(double Latitude, double Longitude)
        {
            Console.Write("Latitude: {0}, Longitude {1}", Latitude, Longitude);
            Console.WriteLine("  Distance to Natlab:"+  measure(natlabcoordinets, Latitude, Longitude) + " Meter");
            url = "https://www.google.com/maps/place/" + Latitude.ToString().Replace(',', '.') + "," + Longitude.ToString().Replace(',', '.');
        }

        /// <summary>
        /// pythagoras function on 2 dimantional plane
        /// </summary>
        /// <param name="cords1"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        double CalculateDistance(double[] cords1, double latitude, double longitude )
        {
            double x = cords1[0] * cords1[0] - latitude * latitude;
            if(x < 0) x = x * -1;

            double y = cords1[1] * cords1[1] - longitude * longitude;
            if (y < 0) y = y * -1;

            double dist = Math.Sqrt(x + y);
            return dist * 111.139;
        }

        double measure(double[] cords, double lat2, double lon2)
        {
            var R = 6378.137;
            var dLat = lat2 * Math.PI / 180 - cords[0] * Math.PI / 180;
            var dLon = lon2 * Math.PI / 180 - cords[1] * Math.PI / 180;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(cords[0] * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d * 1000;
        }

        public int CalculateAngle(double lon1 , double lat1, double lon2, double lat2)
        {
            double angle = -400;
            double aanliggend = lat2 - lat1;
            double overstaand = lon2 - lon1;
            angle = Math.Atan(overstaand / aanliggend) * 180 / Math.PI;
            if (lat1 <= lat2)
            {
                if(angle < 0)  angle += 360;
            }
            else
            {
                angle += 180;
            }

            return (int)angle;
        }
        
    }
}

