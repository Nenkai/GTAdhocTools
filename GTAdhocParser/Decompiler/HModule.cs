using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAdhocTools.Decompiler
{
    public class HModule
    {
        public string Name { get; set; }
        public HModule Parent { get; set; }

        public List<HObject> StaticVariables { get; set; }

        public HModule(string moduleName)
            => Name = moduleName;
    }
}
