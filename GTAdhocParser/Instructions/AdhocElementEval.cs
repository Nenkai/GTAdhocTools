using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpElementEval : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.ELEMENT_EVAL;
        public uint Unknown { get; set; }

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {

        }

        public override string ToString()
            => $"{Unknown, 4}| {CallType}";
    }
}
