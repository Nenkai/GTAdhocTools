using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpImport : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.IMPORT;
        public uint Unknown { get; set; }

        public List<string> Unk { get; set; }
        public string Unk2;
        public string Unk3;

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Unk = Utils.ReadADCStringTable(parent, ref sr);
            Unk2 = Utils.ReadADCString(parent, ref sr);


            // if version > 9
            Unk3 = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
            => $"{Unknown, 4}| {CallType}: {string.Join(',', Unk)}, Unk2={Unk2}, Unk3={Unk3}";
    }
}
