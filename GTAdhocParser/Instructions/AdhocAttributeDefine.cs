using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpAttributeDefine : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.ATTRIBUTE_PUSH;
        public string AttributeName { get; set; }

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            AttributeName = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
           => $"{CallType}: {AttributeName}";

        public void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
