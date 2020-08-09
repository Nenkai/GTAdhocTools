using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpVariablePush : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.VARIABLE_PUSH;
        public uint Unknown { get; set; }

        public List<string> Names { get; set; }
        public uint Value { get; set; }

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Names = Utils.ReadADCStringTable(parent, ref sr);
            Value = sr.ReadUInt32();
        }

        public override string ToString()
            => $"{Unknown, 4}| {CallType}: {string.Join(',', Names)}, Value={Value}";
    }
}
