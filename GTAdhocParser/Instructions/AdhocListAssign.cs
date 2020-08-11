using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpListAssign : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.LIST_ASSIGN;
        

        public uint Unk { get; set; }

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Unk = sr.ReadUInt32();
            if (parent.Version > 11)
            {
                sr.ReadByte();
                /* Wat
                uVar1 = HStreamReader::ReadByte();
                uVar2 = ((uVar1 ^ 1) & 0xff) - 1;
                uVar1 = uVar2 >> 0x3f;
                *(byte*)(param_1 + 8) = (byte)(uVar2 >> 0x3f);*/
            }
        }

        public override string ToString()
           => $"{CallType}: Unk={Unk}";

        public void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
