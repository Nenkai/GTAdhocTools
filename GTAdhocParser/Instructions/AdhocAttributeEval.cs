using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpAttributeEval : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.ATTRIBUTE_EVAL;
        

        public List<string> Names = new List<string>();

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Names = Utils.ReadADCStringTable(parent, ref sr);
        }

        public override string ToString()
            => $"{CallType}: {string.Join(',', Names)}";

        public void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
