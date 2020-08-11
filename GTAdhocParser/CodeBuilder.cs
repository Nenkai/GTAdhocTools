using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTAdhocParser.Instructions;
using GTAdhocParser.Decompiler;

namespace GTAdhocParser
{
    public class CodeBuilder
    {
        public int _depth;

        public HModule CurrentModule { get; set; }

        private StringBuilder sb = new StringBuilder();

        public void AppendRaw(string code)
        {
            sb.Append(code);
        }

        public void Append(string code)
        {
            sb.Append(new string('\t', _depth)).Append(code);
            sb.Append(code);
        }

        public void AppendLine(string code)
        {
            sb.Append(new string('\t', _depth)).AppendLine(code);
        }

        public void SetModule(OpModule module)
        {
            _depth++;
            CurrentModule = new HModule(module.Names[^1]);
        }
    }
}
