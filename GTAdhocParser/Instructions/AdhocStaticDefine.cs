using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpStaticDefine : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.STATIC_DEFINE;
        public uint Unknown { get; set; }

        public string Value { get; set; }
        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
            => $"{Unknown, 4}| {CallType}: {Value}";
    }
}
