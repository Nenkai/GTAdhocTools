using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpLongConst : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.LONG_CONST;
        
        public long Value { get; set; }
        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = sr.ReadInt64();
        }

        public override string ToString()
            => $"{CallType}: {Value} (0x{Value:X2})";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
