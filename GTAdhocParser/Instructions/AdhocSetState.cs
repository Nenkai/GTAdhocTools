using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpSetState : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.SET_STATE;
        

        public byte State { get; set; }
        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            State = sr.ReadByte();
        }

        public override string ToString()
            => $"{CallType}: State={State}";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
