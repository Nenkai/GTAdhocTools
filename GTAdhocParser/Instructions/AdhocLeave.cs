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
        public uint StackRewindIndex;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            val1 = sr.ReadUInt32();
            StackRewindIndex = sr.ReadUInt32();
        }

        public override string ToString()
           => $"{CallType}: Unused:{val1}, RewindTo:{StackRewindIndex}";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
