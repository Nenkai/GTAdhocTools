using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

using GTAdhocParser;

namespace GTAdhocParser.Instructions
{
    public class OpModule : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.MODULE_DEFINE;
        

        public List<string> Names = new List<string>();

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Names = Utils.ReadADCStringTable(parent, ref sr);
        }

        public override string ToString()
           => $"{CallType}: {Names[^1]}";

        public void Decompile(CodeBuilder builder)
        {
            builder.SetModule(this);
        }
    }
}
