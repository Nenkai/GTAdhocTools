using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpTryCatch: InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.TRY_CATCH;

        public int Value { get; set; }
        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = sr.ReadInt32();
        }

        public override string ToString()
            => $"{CallType}: {Value}";

        public override void Decompile(CodeBuilder builder)
        {

        }
    }
}
