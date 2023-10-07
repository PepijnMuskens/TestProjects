using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datacreator.Limits
{
    internal class ChangeLimit : Limit
    {
        
        Dictionary<int, double> Increases;
        double MinIncrease;
        double MaxIncrease;

        int MinIncreaseTimestamp;
        int MaxIncreaseTimestamp;

        Datapoint lastpoint;
        public ChangeLimit(string name, double deviation, List<Datapoint> points, int samplePointcount): base(name,deviation)
        {
            Name = "ChangeLimit";
            SamplePointcount = samplePointcount;
            Increases = new Dictionary<int, double>();
            List<Datapoint> datapoints = new List<Datapoint>();
            if(points.Count < SamplePointcount) SamplePointcount = points.Count;
            lastpoint = points[points.Count - 1];
            for (int x = SamplePointcount; x > 0; x--)
            {
                datapoints.Add(points[points.Count - x]);
            }
            if (datapoints.Count > 1)
            {
                MinIncrease = int.MaxValue;
                MaxIncrease = int.MinValue;
            }
            for (int i = 1; i < datapoints.Count; i++)
            {
                double increase = datapoints[i].value - datapoints[i - 1].value;
                if (!Increases.ContainsKey(datapoints[i].timestamp)) Increases.Add(datapoints[i].timestamp, increase);
                if (MinIncrease > increase)
                {
                    MinIncrease = increase;
                    MinIncreaseTimestamp = datapoints[i].timestamp;
                }
                if (MaxIncrease < increase)
                {
                    MaxIncrease = increase;
                    MaxIncreaseTimestamp = datapoints[i].timestamp;
                    
                }
            }
            MinValue = datapoints[datapoints.Count -1].value + MinIncrease * (1 + Deviation);
            MaxValue = datapoints[datapoints.Count -1].value + MaxIncrease * (1 + Deviation);
        }

        
        public override void AddDatapoint(Datapoint point)
        {
            if (Increases.ContainsKey(point.timestamp)) return;
            double inc = point.value - lastpoint.value;
            Increases.Add(point.timestamp, inc);
            lastpoint = point;
            //loop trough all datapoints remove oldest and save the second biggest increase timestamp in case the oldest was the biggest
            int firsttimestamp = int.MaxValue;
            int lasttimestamp = int.MinValue;
            double SecondMinIncrease = int.MaxValue;
            double SecondMaxIncrease = int.MinValue;
            int SecondMinIncreaseTimestamp = 0;
            int SecondMaxIncreaseTimestamp = 0;
            foreach (KeyValuePair<int,double> increase in Increases)
            {
                if(increase.Key < firsttimestamp)
                {
                    firsttimestamp = increase.Key;
                }
                else if(increase.Key > lasttimestamp)
                {
                    lasttimestamp = increase.Key;
                }
                if(increase.Key != MinIncreaseTimestamp || increase.Key != MaxIncreaseTimestamp)
                {
                    if (SecondMinIncrease > increase.Value)
                    {
                        SecondMinIncrease = increase.Value;
                        SecondMinIncreaseTimestamp = increase.Key;
                    }
                    else if (SecondMaxIncrease < increase.Value)
                    {
                        SecondMaxIncrease = increase.Value;
                        SecondMaxIncreaseTimestamp = increase.Key;
                    }
                }
            }
            if (Increases[firsttimestamp] == MinIncrease || MinIncrease > SecondMinIncrease)
            {
                MinIncrease = SecondMinIncrease;
                MinIncreaseTimestamp = SecondMinIncreaseTimestamp;

            }
            if (Increases[firsttimestamp] == MaxIncrease || MaxIncrease < SecondMaxIncrease)
            {
                MaxIncrease = SecondMaxIncrease;
                MaxIncreaseTimestamp = SecondMaxIncreaseTimestamp;

            }
            Increases.Remove(firsttimestamp);
            MinValue = point.value + MinIncrease * (1 + Deviation);
            MaxValue = point.value + MaxIncrease * (1 + Deviation);
        }
    }
}
