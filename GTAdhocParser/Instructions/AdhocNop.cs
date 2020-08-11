using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

using GTAdhocParser;

namespace GTAdhocParser.Instructions
{
    // Used for version <= 6?
    public class OpNop : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.NOP;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {

        }

        public override string ToString()
           => CallType.ToString();
    }
}
