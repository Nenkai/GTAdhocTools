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

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Name))
                return Name;

            if (Type == HObjectType.Equals)
                return "==";
            else if (Type == HObjectType.Null)
                return "nil";

            return null;
        }
    }

    public enum HObjectType
    {
        Null,
        Variable,
        Equals,
    }
}
