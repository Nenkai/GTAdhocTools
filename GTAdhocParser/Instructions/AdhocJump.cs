﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpJump : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.JUMP;
        

        public uint Value;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = sr.ReadUInt32();
        }

        public override string ToString()
           => $"{CallType}: JumpTo={Value}";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
