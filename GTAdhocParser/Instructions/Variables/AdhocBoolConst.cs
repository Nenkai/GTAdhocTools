using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpBoolConst : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.BOOL_CONST;
        

        public bool Value { get; set; }
        public int offset;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            offset = sr.Position;
            Value = sr.ReadByte() != 0;
        }

        public override string ToString()
           => $"{CallType}: {Value}";

        public void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
