using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public interface IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; }
        public uint Unknown { get; set; }

        public void Deserialize(AdhocFile parent, ref SpanReader sr);

        public string ToString()
            => $"{Unknown, 4}| {CallType}: ";
    }
}
