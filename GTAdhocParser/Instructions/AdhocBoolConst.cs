using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpBoolConst : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.BOOL_CONST;
        public uint Unknown { get; set; }

        public bool Value { get; set; }

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = sr.ReadByte() != 0;
        }

        public override string ToString()
           => $"{Unknown, 4}| {CallType}: {Value}";
    }
}
