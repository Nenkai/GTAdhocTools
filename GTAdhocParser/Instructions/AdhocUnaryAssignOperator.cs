using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpUnaryAssignOperator : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.UNARY_ASSIGN_OPERATOR;
        public uint Unknown { get; set; }

        public string Name { get; set; }
        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Name = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
            => $"{Unknown, 4}| {CallType}: {OpBinaryOperator.GetHumanReadable(Name)} ({Name})";
    }
}
