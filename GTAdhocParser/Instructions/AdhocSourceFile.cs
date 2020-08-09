using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpSourceFile : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.SOURCE_FILE;
        public uint Unknown { get; set; }

        public string FileName { get; set; }
        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            FileName = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
            => $"{Unknown, 4}| {CallType}: {FileName}";
    }
}
