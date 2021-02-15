using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class Op69 : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.UNK_69;

        public string Value { get; set; }
        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = Utils.ReadADCString(parent, ref sr);
        }

        public override string ToString()
            => $"{CallType}: {Value}";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
