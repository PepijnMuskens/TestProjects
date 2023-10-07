using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace datacreator
{
    public class JsonCreator
    {
        static Random random = new Random();

        public string getData(int interval, int timespan, double diffrence, double avgtemp, double flux, double offset, double frequenty)
        {
            string temp = TempDataGennerator(interval, timespan, diffrence, avgtemp, flux, offset, frequenty).ToJson();
            return temp;
        }

        public string getRandomData(int interval, int timespan)
        {
            DataSet dataset = new DataSet();
            int value = 20;
            for (int i = 0; i < timespan * 3600; i += interval * 60)
            {
                value += random.Next(-200, 201);
                dataset.datapoints.Add(new Datapoint(i, value/100d));
            }
            return dataset.ToJson();
        }

        public string getBoolData(int interval, int timespan)
        {
            DataSet dataset = new DataSet();
            double val = 0.1;
            double value = 0;
            for (int i = 0; i < timespan * 3600; i += interval * 60)
            {
                val = (Math.Sin((i - 31000d) / 13751d) * (10d / 2d)) + 10d / 50d * random.Next(-20,21);
                if(val > 5)
                {
                    value = 1.1;
                }
                else if(val < -5)
                {
                    value = 0.1;
                }
                
                dataset.datapoints.Add(new Datapoint(i, value));
            }
            return dataset.ToJson();
        }

        public Dictionary<string, DataSet> getCsvData()
        {
            DataSet dataset = new DataSet();
            Dictionary<string, DataSet> datasets = new Dictionary<string, DataSet>();
            var reader = new StreamReader(@"C:\Users\pepij\Downloads\Data\data 8 week.csv");
            //var reader = new StreamReader(@"C:\Users\pepij\Downloads\Data\1weekpowerexport.csv");
            //var reader = new StreamReader(@"C:\Users\pepij\Downloads\Data\1daypowerexport.csv");

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if(line != "timestamp,name,attribute_name,value")
                {
                    var values = line.Split(',');
                    bool match = false;
                    values[3] = values[3].Replace('.', ',');
                    DateTime date;
                    if (values[0].Length == 23)
                    {
                         date = DateTime.ParseExact(values[0], "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else if(values[0].Length == 22)
                    {
                        date = DateTime.ParseExact(values[0], "yyyy-MM-dd HH:mm:ss.ff", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else if (values[0].Length == 21)
                    {
                        date = DateTime.ParseExact(values[0], "yyyy-MM-dd HH:mm:ss.f", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        date = DateTime.ParseExact(values[0], "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    }

                    foreach (KeyValuePair<string,DataSet> pair in datasets)
                    {
                       
                        if(pair.Key == values[1])
                        {
                            match = true;
                            int timestamp = date.DayOfYear * 86400 + date.Hour * 3600 + date.Minute * 60;
                            pair.Value.datapoints.Add(new Datapoint(timestamp, Convert.ToDouble(values[3])));
                        }
                    }
                    if (!match)
                    {
                        int timestamp = date.DayOfYear * 86400 + date.Hour * 3600 + date.Minute * 60;
                        datasets.Add(values[1], new DataSet() { datapoints = new List<Datapoint>() { new Datapoint(timestamp, Convert.ToDouble(values[3])) } });
                        
                    }
                }
                
            }
            foreach (KeyValuePair<string, DataSet> pair in datasets)
            {

                pair.Value.datapoints.Sort();
            }

            return datasets;
        }
        
        public DataSet TempDataGennerator(int interval, int timespan, double diffrence, double avgtemp, double flux, double offset, double frequenty)
        {
            DataSet dataset = new DataSet();
            for (int i = 0; i < timespan * 3600; i +=interval *60)
            {
                
                dataset.datapoints.Add(new Datapoint(i, tempgenerator(i,diffrence,avgtemp,flux, offset, frequenty)));
            }
            return dataset;
        }
        

        double tempgenerator(int timestamp, double diffrence, double avgtemp, double flux, double offset, double frequenty)
        {
            double daycycle = 13751;
            offset *= 1000;
            offset += 30000;
            frequenty /= 4;
            frequenty = Math.Sqrt(frequenty);
            daycycle /= frequenty;
            flux = random.Next(0 - (int)flux / 2, (int)flux / 2);
            return (Math.Sin((timestamp - offset) / daycycle) * (diffrence / 2f) + avgtemp) + diffrence/50 *flux;
        }


        public class DataSet
        {
            public List<Datapoint> datapoints = new List<Datapoint>();

            public string ToJson()
            {
                string temp = JsonConvert.SerializeObject(datapoints);
                return temp;
            }
        }
        
    }
}
