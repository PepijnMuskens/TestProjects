using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datacreator
{
    internal class Attribute
    {
        public string Name;
        double Value;

        public JsonCreator.DataSet DataSet;
        public List<Limits.Limit> Limits;
        public List<string> displaylimits;

        public Attribute(string name)
        {
            Name = name;
            Value = 0;
            Limits = new List<Limits.Limit>();
            displaylimits = new List<string>();
            DataSet = new JsonCreator.DataSet();
        }

        public void AddLimit(Limits.Limit limit)
        {
            if(Limits.Find(l => l.Name == limit.Name) == null) Limits.Add(limit);

        }
    }
}
