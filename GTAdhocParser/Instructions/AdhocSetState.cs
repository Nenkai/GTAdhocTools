using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpSetState : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.SET_STATE;
        public uint Unknown { get; set; }

        public byte State { get; set; }
        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            State = sr.ReadByte();
        }

        public override string ToString()
            => $"{Unknown, 4}| {CallType}: State={State}";
    }
}
