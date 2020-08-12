using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpStaticDefine : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.STATIC_DEFINE;
        

        public string StaticName { get; set; }
        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            StaticName = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
            => $"{CallType}: {StaticName}";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
