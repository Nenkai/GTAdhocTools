using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpDoubleConst : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.DOUBLE_CONST;
        

        public double Value;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = sr.ReadDouble();
        }

        public override string ToString()
           => $"{CallType}: Value={Value}";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
