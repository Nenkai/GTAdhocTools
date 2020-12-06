using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTAdhocParser.Decompiler;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpVariableEval : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.VARIABLE_EVAL;
        

        public List<string> Names = new List<string>();
        public int Value;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Names = Utils.ReadADCStringTable(parent, ref sr);
            Value = sr.ReadInt32();
        }

        public override string ToString()
            => $"{CallType}: {string.Join(',', Names)}, ValueEval={Value}";

        public override void Decompile(CodeBuilder builder)
        {
            if (builder.CurrentFunction != null)
            {
                builder.Variables.Add(new HObject(Names[^1], HObjectType.Variable));
                var argExists = builder.CurrentFunction.Code.Arguments.Exists(e => e.argumentIndex == Value); // Evaluating function argument?
                if (argExists)
                    return; // Then we don't need it, we already keep track of it
            }
        }
    }
}
