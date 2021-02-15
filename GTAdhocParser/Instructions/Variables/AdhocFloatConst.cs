using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpFloatConst : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.FLOAT_CONST;
        

        public float Value;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = sr.ReadSingle(); // Game reads it as uint, we read it as float.
        }

        public override string ToString()
           => $"{CallType}: Value={Value}";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
