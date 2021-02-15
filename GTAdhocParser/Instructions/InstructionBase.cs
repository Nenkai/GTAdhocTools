using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public abstract class InstructionBase
    {
        public uint SourceLineNumber { get; set; }
        public uint InstructionOffset { get; set; }

        public abstract void Deserialize(AdhocFile parent, ref SpanReader sr);

        public abstract void Decompile(CodeBuilder builder);
    }
}
