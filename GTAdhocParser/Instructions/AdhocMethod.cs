using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

using GTAdhocParser;

namespace GTAdhocParser.Instructions
{
    public class OpMethod : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.METHOD_DEFINE;
        public uint Unknown { get; set; }

        public string MethodName;
        public AdhocCode Code;

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            ulong v = sr.DecodeBitsAndAdvance();
            MethodName = parent.StringTable[v];

            Code = new AdhocCode();
            Code.Deserialize(parent, ref sr);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.Append($"{Unknown, 4}| ").Append(CallType.ToString()).Append(" - ").Append(MethodName);
            if (Code.Arguments.Count != 0)
            {
                sb.Append(" (");
                for (int i = 0; i < Code.Arguments.Count; i++)
                {
                    sb.Append(Code.Arguments[i].Item1).Append($"[{Code.Arguments[i].Item2}]");
                    if (i != Code.Arguments.Count - 1)
                        sb.Append(',');
                }
                sb.Append(')');
            }

            sb.AppendLine();

            foreach (var comp in Code.Components)
                sb.Append("   ").AppendLine(comp.ToString());

            return sb.ToString();
        }
    }
}
