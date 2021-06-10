using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData;
using Syroot.BinaryData.Memory;

namespace GTAdhocTools
{
    public static class Utils
    {
        public static string Read7BitString(this BinaryStream sr)
        {
            ulong strLen = DecodeBitsAndAdvance(sr);
            return Encoding.UTF8.GetString(sr.ReadBytes((int)strLen));
        }

        public static ulong DecodeBitsAndAdvance(this BinaryStream sr)
        {
            ulong value = (ulong)sr.ReadByte();
            ulong mask = 0x80;

            while ((value & mask) != 0)
            {
                value = ((value - mask) << 8) | (sr.Read1Byte());
                mask <<= 7;
            }
            return value;
        }

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

        public static void AlignWithValue(this BinaryStream bs, int alignment, byte value, bool grow = false)
        {
            long basePos = bs.Position;
            long newPos = bs.Align(alignment);

            bs.Position = basePos;
            for (long i = basePos; i < newPos; i++)
                bs.WriteByte(value);
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
                return sr.ReadString2();
            else
            {
                uint strTableIndex = (uint)sr.DecodeBitsAndAdvance();
                return parent.StringTable[strTableIndex];
            }
        }
    }
}
