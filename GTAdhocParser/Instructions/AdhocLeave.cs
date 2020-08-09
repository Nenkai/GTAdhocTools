using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpLeave : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.LEAVE;
        public uint Unknown { get; set; }

        public uint val1;
        public uint val2;

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            val1 = sr.ReadUInt32();
            val2 = sr.ReadUInt32();
        }

        public override string ToString()
           => $"{Unknown, 4}| {CallType}: V1={val1}, V2={val2}";
    }
}
