using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpStringPush : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.STRING_PUSH;
        

        public uint StringIndex { get; set; }
        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            StringIndex = sr.ReadUInt32();
        }

        public override string ToString()
            => $"{CallType}: StringIndex={StringIndex}";

        public void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
