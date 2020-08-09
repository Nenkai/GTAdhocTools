using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpIntConst : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.INT_CONST;
        public uint Unknown { get; set; }

        public int Value { get; set; }
        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = sr.ReadInt32();
        }

        public override string ToString()
            => $"{Unknown, 4}| {CallType}: {Value}";
    }
}
