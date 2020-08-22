using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpULongConst : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.U_LONG_CONST;

        public ulong Value { get; set; }
        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = sr.ReadUInt64();
        }

        public override string ToString()
            => $"{CallType}: {Value} (0x{Value:X2})";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
