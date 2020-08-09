using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpAttributePush : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.ATTRIBUTE_PUSH;
        public uint Unknown { get; set; }

        public List<string> Attributes;

        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            /*
            if ((int)param_2->adcVersionCurrent < 6)
            {
                iVar1 = FUN_00a1e998();
                HStreamReader::ReadAdhocEntryData(param_2, acStack56);
                FUN_00a30e18(auStack60, acStack56);
                FUN_00a426c8(auStack52, auStack60);
                FUN_00a42778((ulonglong)(param_1 + 4), auStack52);
                FUN_00a41fc8(auStack52);
                FUN_00a3173c(auStack60);
                DisposeObject(&DAT_fffffff4 + iVar1 + 0xc, auStack64);
            }
            * else */
            Attributes = Utils.ReadADCStringTable(parent, ref sr);
        }

        public override string ToString()
           => $"{Unknown, 4}| {CallType}: Attributes={string.Join(',', Attributes)}";
    }
}
