using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpLogicalAnd : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.LOGICAL_OR;
        

        public uint Value;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = sr.ReadUInt32();
        }

        public override string ToString()
           => $"{CallType}: Value={Value}";

        public void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
