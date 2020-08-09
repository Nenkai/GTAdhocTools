using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpLogicalAnd : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.LOGICAL_OR;
        public uint Unknown { get; set; }

        public uint Value;

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = sr.ReadUInt32();
        }

        public override string ToString()
           => $"{Unknown, 4}| {CallType}: Value={Value}";
    }
}
