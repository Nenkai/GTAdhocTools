using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public abstract class InstructionBase
    {
        public uint LineNumber { get; set; }
        public uint InstructionOffset { get; set; }

        public abstract void Deserialize(AdhocFile parent, ref SpanReader sr);

        public abstract void Decompile(CodeBuilder builder);
    }
}
