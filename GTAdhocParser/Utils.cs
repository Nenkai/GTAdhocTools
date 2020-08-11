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
            uint strCount = sr.ReadUInt32();
            List<string> list = new List<string>((int)strCount);

            for (int i = 0; i < strCount; i++)
            {
                string str = ReadADCString(parent, ref sr);
                list.Add(str);
            }

            return list;
        }

        public static string ReadADCString(AdhocFile parent, ref SpanReader sr)
        {
            if (parent.Version <= 8)
            {
                ushort strLen = sr.ReadUInt16();
                return sr.ReadStringRaw((int)strLen);
            }
            else
            {
                uint strTableIndex = (uint)sr.DecodeBitsAndAdvance();
                return parent.StringTable[strTableIndex];
            }
        }
    }
}
