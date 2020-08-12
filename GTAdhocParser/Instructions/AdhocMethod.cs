using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

using GTAdhocParser;

namespace GTAdhocParser.Instructions
{
    public class OpMethod : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.METHOD_DEFINE;

        public OpMethod(AdhocCallType callType)
            => CallType = callType;

        public string MethodName;
        public AdhocCode Code;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            if (CallType != AdhocCallType.METHOD_CONST && CallType != AdhocCallType.FUNCTION_CONST)
                MethodName = Utils.ReadADCString(parent, ref sr);

            Code = new AdhocCode();
            Code.Deserialize(parent, ref sr);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(CallType.ToString()).Append(" - ").Append(MethodName);
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

            return sb.ToString();
        }

        public override void Decompile(CodeBuilder builder)
        {
            builder.SetCurrentFunction(this);
        }
    }
}
