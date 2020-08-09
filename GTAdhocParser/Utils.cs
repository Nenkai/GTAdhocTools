using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser
{
    public static class Utils
    {
        public static ulong DecodeBitsAndAdvance(this ref SpanReader sr)
        {
            ulong value = sr.ReadByte();
            ulong mask = 0x80;

            while ((value & mask) != 0)
            {
                value = ((value - mask) << 8) | (sr.ReadByte());
                mask <<= 7;
            }
            return value;
        }

        public static List<string> ReadADCStringTable(AdhocFile parent, ref SpanReader sr)
        {
            // if version < 9..
            // read from continuous data
            // else parse from string table..

            uint strCount = sr.ReadUInt32();
            List<string> list = new List<string>((int)strCount);

            for (int i = 0; i < strCount; i++)
            {
                uint strTableIndex = (uint)sr.DecodeBitsAndAdvance();
                list.Add(parent.StringTable[strTableIndex]);
            }

            return list;
        }

        public static string ReadADCString(AdhocFile parent, ref SpanReader sr)
        {
            // if version < 9..
            // read from continuous data
            // else parse from string table..

            uint strTableIndex = (uint)sr.DecodeBitsAndAdvance();
            return parent.StringTable[strTableIndex];
        }
    }
}
