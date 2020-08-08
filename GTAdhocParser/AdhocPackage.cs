using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.IO.Compression;
using Syroot.BinaryData.Memory;

namespace GTAdhocParser
{
    public class AdhocPackage
    {
        public const string Magic = "MPKG";

        public static unsafe void ReadPackage(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var sr = new SpanReader(bytes, encoding: Encoding.ASCII);
            if (!sr.ReadStringRaw(4).Equals(Magic))
                throw new Exception("Invalid MAGIC, doesn't match MPKG.");

            sr.Position += 4;

            uint fileEntryCount = sr.ReadUInt32();
            uint tableOfContentsOffset = sr.ReadUInt32(); // Table of contents

            string dir = Path.GetDirectoryName(path);
            string outDir = Path.Combine(dir, Path.GetFileNameWithoutExtension(path));
            Directory.CreateDirectory(outDir);

            byte[] buffer = new byte[512_000];
            for (int i = 0; i < fileEntryCount; i++)
            {
                sr.Position = (int)tableOfContentsOffset + (i * (sizeof(uint) * 3));

                uint fileNameOffset = sr.ReadUInt32();
                uint compressedDataOffset = sr.ReadUInt32();
                uint compressedFileSize = sr.ReadUInt32();

                sr.Position = (int)fileNameOffset;
                string fileName = sr.ReadString0();

                sr.Position = (int)compressedDataOffset;

                int uncompressedSize;
                fixed (byte* pBuffer = &sr.Span.Slice(sr.Position)[0])
                {
                    using var ums = new UnmanagedMemoryStream(pBuffer, compressedFileSize);
                    using var ds = new DeflateStream(ums, CompressionMode.Decompress);
                    uncompressedSize = ds.Read(buffer, 0, buffer.Length);

                    using (var fs = new FileStream(Path.Combine(outDir, Path.GetFileName(fileName)), FileMode.Create))
                        fs.Write(buffer, 0, uncompressedSize);

                }

                Console.WriteLine($"Extracted: {fileName}");
                Array.Clear(buffer, 0, uncompressedSize);

            }
        }

    }
}
