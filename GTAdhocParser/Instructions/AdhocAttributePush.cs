using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpAttributePush : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.ATTRIBUTE_PUSH;
        

        public List<string> Attributes;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            if (parent.Version <= 5)
            {
                string attrName = Utils.ReadADCString(parent, ref sr);
                Attributes = new List<string>(1);
                Attributes.Add(attrName);
            }
            else
                Attributes = Utils.ReadADCStringTable(parent, ref sr);
        }

        public override string ToString()
           => $"{CallType}: Attributes={string.Join(',', Attributes)}";

        public void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
