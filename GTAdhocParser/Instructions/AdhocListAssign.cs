using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpListAssign : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.LIST_ASSIGN;
        

        public uint ElemCount { get; set; }
        public bool UnkBool { get; set; }

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            ElemCount = sr.ReadUInt32();
            if (parent.Version > 11)
            {
                UnkBool = sr.ReadBoolean();
            }
        }

        public override string ToString()
           => $"{CallType}: ElemCount={ElemCount}, UnkBool={UnkBool}";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
