using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpStringPush : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.STRING_PUSH;
        public uint Unknown { get; set; }

        public uint StringIndex { get; set; }
        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            StringIndex = sr.ReadUInt32();
        }

        public override string ToString()
            => $"{Unknown, 4}| {CallType}: StringIndex={StringIndex}";
    }
}
