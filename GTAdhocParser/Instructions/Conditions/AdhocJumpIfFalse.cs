using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpJumpIfFalse : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.JUMP_IF_FALSE;
        

        public uint InstructionIndex { get; set; }

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            InstructionIndex = sr.ReadUInt32();
        }

        public override string ToString()
           => $"{CallType}: Jump To Instruction {InstructionIndex}";

        public void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
