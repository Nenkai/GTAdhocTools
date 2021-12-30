using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

using GTAdhocTools;

namespace GTAdhocTools.Instructions
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
            sb.Append("(");
            if (Code.Arguments.Count != 0)
            {
                for (int i = 0; i < Code.Arguments.Count; i++)
                {
                    sb.Append(Code.Arguments[i].Item1).Append($"[{Code.Arguments[i].Item2}]");
                    if (i != Code.Arguments.Count - 1)
                        sb.Append(", ");
                }
            }

            sb.Append(")");

            if (Code.FunctionConstArguments.Count != 0)
            {
                sb.Append("[");
                for (int i = 0; i < Code.FunctionConstArguments.Count; i++)
                {
                    sb.Append(Code.FunctionConstArguments[i].Item1).Append($"[{Code.FunctionConstArguments[i].Item2}]");
                    if (i != Code.FunctionConstArguments.Count - 1)
                        sb.Append(", ");
                }
                sb.Append("]");
            }

            sb.AppendLine();

            sb.Append("  > Instruction Count: ").Append(Code.InstructionCount).Append(" (").Append(Code.InstructionCountOffset.ToString("X2")).Append(')').AppendLine();
            sb.Append($"  > Stack Size: {Code.StackSize} - Variable Heap Size: {Code.VariableHeapSize} - Variable Heap Size Static: {(Code.CodeVersion < 10 ? "=Variable Heap Size" : $"{Code.VariableHeapStaticSize}")}");

            return sb.ToString();
        }

        public override void Decompile(CodeBuilder builder)
        {
            builder.SetCurrentFunction(this);
            foreach (var ins in Code.Components)
                ins.Decompile(builder);
        }
    }
}
