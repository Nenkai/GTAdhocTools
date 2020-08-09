using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpAttributeEval : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.ATTRIBUTE_EVAL;
        public uint Unknown { get; set; }

        public List<string> Names = new List<string>();

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Names = Utils.ReadADCStringTable(parent, ref sr);
        }

        public override string ToString()
            => $"{Unknown, 4}| {CallType}: {string.Join(',', Names)}";
    }
}
