using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpAttributeDefine : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.ATTRIBUTE_DEFINE;
        public string Name { get; set; }

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Name = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
           => $"{CallType}: {Name}";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
