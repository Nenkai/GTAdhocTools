using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpLeave : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.LEAVE;
        

        public uint val1;
        public uint val2;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            val1 = sr.ReadUInt32();
            val2 = sr.ReadUInt32();
        }

        public override string ToString()
           => $"{CallType}: V1={val1}, V2={val2}";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
