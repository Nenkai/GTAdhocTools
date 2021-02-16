using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.IO.Compression;
using Syroot.BinaryData;
using Syroot.BinaryData.Memory;

namespace GTAdhocTools
{
    public class AdhocPackage
    {
        public const string Magic = "MPKG";

        public static void ExtractPackage(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var sr = new SpanReader(bytes, encoding: Encoding.ASCII);
            if (!sr.ReadStringRaw(4).Equals(Magic))
                throw new Exception("Invalid MAGIC, doesn't match MPKG.");

            sr.Position += 4;

            uint fileEntryCount = sr.ReadUInt32();
            uint tableOfContentsOffset = sr.ReadUInt32(); // Table of contents

            string dir = Path.GetDirectoryName(path);
            string outDir = $"{Path.GetFileNameWithoutExtension(path)}_extracted";

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

                fileName = fileName.Replace("%P", "gt6");
                Directory.CreateDirectory(outDir + Path.GetDirectoryName(fileName));

                sr.Position = (int)compressedDataOffset;

                int uncompressedSize;

                unsafe
                {
                    fixed (byte* pBuffer = &sr.Span.Slice(sr.Position)[0])
                    {
                        using var ums = new UnmanagedMemoryStream(pBuffer, compressedFileSize);
                        using var ds = new DeflateStream(ums, CompressionMode.Decompress);
                        uncompressedSize = ds.Read(buffer, 0, buffer.Length);

                        using (var fs = new FileStream(outDir + fileName, FileMode.Create))
                            fs.Write(buffer, 0, uncompressedSize);
                    }
                }

                Console.WriteLine($"Extracted: {fileName}");
                Array.Clear(buffer, 0, uncompressedSize);

            }
        }

        public static void PackFromFolder(string inputFolder, string outFile)
        {
            Console.WriteLine("[:] Packing MPackage..");

            var files = Directory.GetFiles(inputFolder, "*", SearchOption.AllDirectories)
                .OrderBy(e => e, StringComparer.Ordinal)
                       .ToArray();

            if (outFile is null)
                outFile = $"{Path.GetFileName(inputFolder)}.mpackage";

            using var fs = new FileStream(outFile, FileMode.Create);
            using var bs = new BinaryStream(fs, ByteConverter.Little);

            bs.WriteString(Magic, StringCoding.Raw);
            bs.WriteInt32(0); // Relocation Ptr
            bs.WriteInt32(files.Length);

            // Skip toc offset for now
            bs.Position = 0x10;

            List<(int stringOffset, int dataOffset, int compressedSize)> toc = new(files.Length);
            foreach (var file in files)
            {
                int stringOffset = (int)bs.Position;
                string fileName = file.Replace('\\', '/'); // Replace to any wanted path separator
                fileName = fileName.Substring(fileName.IndexOf('/')); // Remove the parent
                if (!fileName.StartsWith('/'))
                    fileName = '/' + fileName; // Ensure it starts with '/'

                fileName = fileName.Replace("gt6", "%P");

                bs.WriteString(fileName, StringCoding.ZeroTerminated);
                int entryOffsetPos = (int)bs.Position;
                using (var ds = new DeflateStream(bs, CompressionMode.Compress, true))
                {
                    byte[] fileData = File.ReadAllBytes(file);
                    ds.Write(fileData, 0, fileData.Length);
                }
                int compressSize = (int)bs.Position - entryOffsetPos;

                toc.Add( (stringOffset, entryOffsetPos, compressSize) );
            }

            bs.Align(0x04, true);
            int tocOffset = (int)bs.Position;

            for (int i = 0; i < toc.Count; i++)
            {
                bs.WriteInt32(toc[i].stringOffset);
                bs.WriteInt32(toc[i].dataOffset);
                bs.WriteInt32(toc[i].compressedSize);
            }

            bs.Position = 0x0C;
            bs.WriteInt32(tocOffset);
        }


    }
}
