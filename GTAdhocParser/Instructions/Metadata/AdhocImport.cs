using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpImport : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.IMPORT;
        

        public List<string> ImportNames { get; set; }
        public string Unk2;
        public string Unk3;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            ImportNames = Utils.ReadADCStringTable(parent, ref sr);
            Unk2 = Utils.ReadADCString(parent, ref sr);


            // if version > 9
            Unk3 = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
            => $"{CallType}: {ImportNames[^1]}, Unk2={Unk2}, Unk3={Unk3}";

        public void Decompile(CodeBuilder builder)
        {
            builder.AppendLine($"import {ImportNames[^1]}");
        }
    }
}
