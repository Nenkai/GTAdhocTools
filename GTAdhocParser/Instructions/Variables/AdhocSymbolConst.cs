using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpSymbolConst: InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.SYMBOL_CONST;
        

        public string Str { get; set; }

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Str = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
           => $"{CallType}: {Str}";

        public void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
