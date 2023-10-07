using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datacreator.Limits
{
    internal abstract class Limit
    {
        public string Name;
        public bool IsActive = true;
        public double Deviation;
        public int SamplePointcount;

        public double MinValue;
        public double MaxValue;

        public Limit(string name, double deviation)
        {
            Name = name;
            Deviation = deviation;
        }
        public abstract void AddDatapoint(Datapoint point);

    }
}
