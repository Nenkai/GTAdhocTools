using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpVariablePush : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.VARIABLE_PUSH;
        

        public List<string> Names { get; set; }
        public uint Value { get; set; }

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Names = Utils.ReadADCStringTable(parent, ref sr);
            Value = sr.ReadUInt32();
        }

        public override string ToString()
            => $"{CallType}: {string.Join(',', Names)}, Value={Value}";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
