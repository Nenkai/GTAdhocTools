using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Syroot.BinaryData.Memory;

namespace GTAdhocParser
{
    class Adhoc
    {
        public const string MAGIC = "ADHC012";

        public byte[] _buffer;

        public string[] StringTable { get; }
        public uint DataOffset { get; set; }

        private Adhoc(string[] stringTable)
        {
            StringTable = stringTable;

        }

        public static Adhoc ReadFromFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var sr = new SpanReader(bytes, encoding: Encoding.ASCII);
            if (sr.ReadStringRaw(7).Equals(MAGIC))
                throw new Exception("Invalid MAGIC, doesn't match ADCH012.");
            sr.ReadByte();

            uint entryCount = (uint)DecodeBitsAndAdvance(ref sr);
            var results = new string[entryCount];
            for (var i = 0; i < entryCount; i++)
                results[i] = sr.ReadString1();

            var dataOffset = sr.Position;
            var adc = new Adhoc(results);
            adc.DataOffset = (uint)dataOffset;
            adc._buffer = bytes;
            return adc;
        }

        public void PrintAll(string outPath)
        {
            var sr = new SpanReader(_buffer);
            sr.Position = 8;

            using var sw = new StreamWriter(outPath);
            uint entryCount = (uint)DecodeBitsAndAdvance(ref sr);
            var results = new string[entryCount];
            for (var i = 0; i < entryCount; i++)
            {
                sw.WriteLine($"0x{sr.Position:X2} | {sr.ReadString1()}");
            }
            sw.Flush();
        }

        public void PrintData(string outPath)
        {
            File.WriteAllBytes(outPath, _buffer.AsSpan((int)DataOffset).ToArray());
        }

        public static ulong DecodeBitsAndAdvance(ref SpanReader sr)
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

    }
}
