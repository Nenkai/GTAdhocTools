﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpUndef : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.UNDEF;

        public List<string> Names { get; set; }
        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Names = Utils.ReadADCStringTable(parent, ref sr);
        }

        public override string ToString()
            => $"{CallType}: {string.Join(",", Names)}";

        public override void Decompile(CodeBuilder builder)
        {
            
        }
    }
}
