using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datacreator
{
    public class Datapoint : IComparable
    {
        public Datapoint(int time, double value)
        {
            timestamp = time;
            this.value = value;
        }
        public int timestamp;
        public double value;


        public int CompareTo(object obj)
        {

            Datapoint otherpoint = obj as Datapoint;
            return (timestamp - otherpoint.timestamp);
        }
    }
}
