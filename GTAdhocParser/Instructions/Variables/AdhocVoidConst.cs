using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    /// <summary>
    /// Line ending?
    /// </summary>
    public class OpVoidConst : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.VOID_CONST;
        

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {

        }

        public override string ToString()
            => $"{CallType}";

        public void Decompile(CodeBuilder builder)
        {
            builder.AppendLine(string.Empty);
        }
    }
}
