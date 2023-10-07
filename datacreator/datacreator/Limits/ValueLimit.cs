using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datacreator.Limits
{
    internal class ValueLimit : Limit
    {
        
        Dictionary<int, double> Values;

        int MinValueTimestamp;
        int MaxValueTimestamp;
        double avg;
        double min;
        double max;
        Datapoint lastpoint;
        public ValueLimit(string name, double deviation, List<Datapoint> points, int samplePointcount): base(name,deviation)
        {
            Name = "ValueLimit";
            SamplePointcount = samplePointcount;
            Values = new Dictionary<int, double>();
            List<Datapoint> datapoints = new List<Datapoint>();
            if(points.Count < SamplePointcount) SamplePointcount = points.Count;
            lastpoint = points[points.Count - 1];
            for (int x = SamplePointcount; x > 0; x--)
            {
                datapoints.Add(points[points.Count - x]);
            }
            if (datapoints.Count > 1)
            {
                min = int.MaxValue;
                max = int.MinValue;
            }
            for (int i = 1; i < datapoints.Count; i++)
            {
                if (!Values.ContainsKey(datapoints[i].timestamp)) Values.Add(datapoints[i].timestamp, datapoints[i].value);
                if (min > datapoints[i].value)
                {
                    min = datapoints[i].value;
                    MinValueTimestamp = datapoints[i].timestamp;
                }
                if (max < datapoints[i].value)
                {
                    max = datapoints[i].value;
                    MaxValueTimestamp = datapoints[i].timestamp;
                    
                }
                avg += datapoints[i].value;
            }
            avg /= datapoints.Count;
            MinValue = min + (min - avg) * (Deviation);
            MaxValue = max + (max - avg) * (Deviation);
        }

        
        public override void AddDatapoint(Datapoint point)
        {
            if (Values.ContainsKey(point.timestamp)) return;
            Values.Add(point.timestamp, point.value);
            int firsttimestamp = int.MaxValue;
            int lasttimestamp = int.MinValue;
            min = int.MaxValue;
            max = int.MinValue;
            avg = 0;
            foreach (KeyValuePair<int,double> value in Values)
            {
                if(value.Key < firsttimestamp)
                {
                    firsttimestamp = value.Key;
                }
                else if(value.Key > lasttimestamp)
                {
                    lasttimestamp = value.Key;
                }  
            }
            Values.Remove(firsttimestamp);
            foreach (KeyValuePair<int, double> value in Values)
            {
                avg += value.Value;
                if (value.Key != MinValueTimestamp || value.Key != MaxValueTimestamp)
                {
                    if (min > value.Value)
                    {
                        min = value.Value;
                        MinValueTimestamp = value.Key;
                    }
                    if (max < value.Value)
                    {
                        max = value.Value;
                        MaxValueTimestamp = value.Key;
                    }
                }
            }
            avg /= Values.Count;
            MinValue = min + (min - avg) * (Deviation);
            MaxValue = max + (max - avg) * (Deviation);
        }
    }
}
