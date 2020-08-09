using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpFloatConst : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.FLOAT_CONST;
        public uint Unknown { get; set; }

        public float Value;

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Value = sr.ReadSingle(); // Game reads it as uint, we read it as float.
        }

        public override string ToString()
           => $"{Unknown, 4}| {CallType}: Value={Value}";
    }
}
