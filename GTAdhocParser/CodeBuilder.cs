using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAdhocParser
{
    public class CodeBuilder
    {
        public int IndentationDepth;
        private StringBuilder sb;

        public void AppendRaw(string code)
        {
            sb.Append(code);
        }

        public void Append(string code)
        {
            sb.Append(new string('\t', IndentationDepth)).Append(code);
            sb.Append(code);
        }

        public void AppendLine(string code)
        {
            sb.Append(new string('\t', IndentationDepth)).AppendLine(code);
        }
    }
}
