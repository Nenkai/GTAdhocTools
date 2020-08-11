using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpVariableEval : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.VARIABLE_EVAL;
        

        public List<string> Names = new List<string>();
        public uint Value;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Names = Utils.ReadADCStringTable(parent, ref sr);
            Value = sr.ReadUInt32();
        }

        public override string ToString()
            => $"{CallType}: {Names[^1]}, ValueEval={Value}";

        public void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
