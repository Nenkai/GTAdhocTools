using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

using GTAdhocParser;

namespace GTAdhocParser.Instructions
{
    public class OpModule : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.MODULE_DEFINE;
        public uint Unknown { get; set; }

        public List<string> Names = new List<string>();

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Names = Utils.ReadADCStringTable(parent, ref sr);
        }

        public override string ToString()
           => $"{Unknown, 4}| {CallType}: {Names[^1]}";

        public void GetHumanCode(CodeBuilder builder)
        {
            builder.AppendLine($"module {Names[^1]}");
            builder.AppendLine("{");
            builder.IndentationDepth++;
        }
    }
}
