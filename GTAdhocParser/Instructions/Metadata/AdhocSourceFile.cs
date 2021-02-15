using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpSourceFile : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.SOURCE_FILE;
        

        public string FileName { get; set; }
        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            FileName = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
            => $"{CallType}: {FileName}";

        public override void Decompile(CodeBuilder builder)
        {
            builder.AppendLine($"// Source File: {FileName}");
        }
    }
}
