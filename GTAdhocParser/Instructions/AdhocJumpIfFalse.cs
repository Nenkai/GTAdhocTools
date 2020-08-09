using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpJumpIfFalse : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.JUMP_IF_FALSE;
        public uint Unknown { get; set; }

        public uint InstructionIndex { get; set; }

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            InstructionIndex = sr.ReadUInt32();
        }

        public override string ToString()
           => $"{Unknown, 4}| {CallType}: Jump To Instruction {InstructionIndex}";
    }
}
