using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAdhocParser.Decompiler
{
    public class HObject
    {
        public HObject(HObjectType type)
        {
            Type = type;
        }

        public HObject(string objName, HObjectType type)
        {
            Name = objName;
            Type = type;
        }

        public HObjectType Type;

        public HObject Parent { get; set; }
        public string Name { get; set; }
    }

    public enum HObjectType
    {
        Variable,
        Equals,
    }
}
