using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTAdhocParser.Decompiler;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpNilConst : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.NIL_CONST;
        

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {

        }

        public override string ToString()
            => $"{CallType}";

        public override void Decompile(CodeBuilder builder)
        {
            if (builder.CurrentFunction is null)
                ;// builder.AppendLine("");
            else
                builder.Variables.Add(new HObject(HObjectType.Null));
        }
    }
}
