using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpAttributeDefine : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.ATTRIBUTE_PUSH;
        public uint Unknown { get; set; }

        public string AttributeName { get; set; }

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            AttributeName = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
           => $"{Unknown, 4}| {CallType}: {AttributeName}";
    }
}
