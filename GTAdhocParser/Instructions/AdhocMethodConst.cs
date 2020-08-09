using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

using GTAdhocParser;

namespace GTAdhocParser.Instructions
{
    public class OpMethodConst : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.METHOD_CONST;
        public uint Unknown { get; set; }

        public AdhocCode Code;

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Code = new AdhocCode();
            Code.Deserialize(parent, ref sr);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.Append($"{Unknown, 4}| ").AppendLine(CallType.ToString());
            foreach (var comp in Code.Components)
                sb.Append("   ").AppendLine(comp.ToString());

            return sb.ToString();
        }
    }
}
