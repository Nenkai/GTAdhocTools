using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpMapConst : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.MAP_CONST;
        

        public bool Value { get; set; }

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {

        }

        public override string ToString()
           => $"{CallType}";

        public void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
