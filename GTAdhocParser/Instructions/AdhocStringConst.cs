using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpStringConst : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.STRING_CONST;
        public uint Unknown { get; set; }

        public string Str { get; set; }

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Str = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
           => $"{Unknown, 4}| {CallType}: {Str}";
    }
}
