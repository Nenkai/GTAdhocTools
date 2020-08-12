using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpUnaryAssignOperator : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.UNARY_ASSIGN_OPERATOR;
        

        public string Name { get; set; }
        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Name = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
            => $"{CallType}: {OpBinaryOperator.GetHumanReadable(Name)} ({Name})";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
