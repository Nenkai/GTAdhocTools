using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

using Syroot.BinaryData;
using Syroot.BinaryData.Memory;


namespace GTAdhocTools
{
    public class GpbData
    {
        public List<Pair> Files { get; set; } = new List<Pair>();
        public const int HeaderSize = 0x20;

        public static GpbData Read(string fileName)
        {
            using var fs = new FileStream(fileName, FileMode.Open);
            using var bs = new BinaryStream(fs, ByteConverter.Big);

            string magic = bs.ReadString(4);
            if (magic == "3bpg")
                bs.ByteConverter = ByteConverter.Big;
            else if (magic == "gpb3")
                bs.ByteConverter = ByteConverter.Little;
            else
                throw new InvalidDataException("File is not a GPB3 format.");

            GpbData gpb = new GpbData();

            bs.ReadInt32(); // Relocation ptr
            int headerSize = bs.ReadInt32(); // Empty
            int entryCount = bs.ReadInt32();

            int entriesOffset = bs.ReadInt32();
            int fileNamesOffset = bs.ReadInt32();
            int fileDataOffset = bs.ReadInt32();

            for (int i = 0; i < entryCount; i++)
            {
                bs.Position = entriesOffset + (i * Pair.EntrySize);
                var file = new Pair();
                file.Read(bs);
                gpb.Files.Add(file);
            }

            return gpb;
        }

        public static GpbData CreateFromFolder(string folderName)
        {
            var gpb = new GpbData();
            string[] files = Directory.GetFiles(folderName, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var pair = new Pair();

                string fileName = file.Replace('\\', '/'); // Replace to any wanted path separator
                fileName = fileName.Substring(fileName.IndexOf('/')); // Remove the parent
                if (!fileName.StartsWith('/'))
                    fileName = '/' + fileName; // Ensure it starts with '/'

                pair.FileName = fileName;
                pair.FileData = File.ReadAllBytes(file);
                gpb.Files.Add(pair);
            }

            return gpb;
        }
        
        public void Unpack(string fileName, string outputFolder)
        {
            if (outputFolder is null)
                outputFolder = fileName;

            foreach (var file in Files)
            {
                Console.WriteLine($"[:] GPB: Unpack -> {file.FileName}");

                string path = file.FileName.Substring(1); // Ignore first '/'
                Directory.CreateDirectory(Path.Combine(outputFolder, Path.GetDirectoryName(path)));
                File.WriteAllBytes(Path.Combine(outputFolder, path), file.FileData);
            }
        }

        public void Pack(string outputFileName, bool bigEndian = true)
        {
            Console.WriteLine($"[:] GPB: Packing {Files.Count} to -> {outputFileName}..");

            using var fs = new FileStream(outputFileName, FileMode.Create);
            using var bs = new BinaryStream(fs, bigEndian ? ByteConverter.Big : ByteConverter.Little);

            if (bigEndian)
                bs.WriteString("3bpg", StringCoding.Raw);
            else
                bs.WriteString("gpb3", StringCoding.Raw);

            bs.WriteInt32(0);
            bs.WriteInt32(HeaderSize); // Header Size
            bs.WriteInt32(Files.Count);

            // Write all file names first
            int baseFileNameOffset = HeaderSize + (Pair.EntrySize * Files.Count);
            int currentFileNameOffset = baseFileNameOffset;

            // Game uses bsearch, important
            Files = Files.OrderBy(e => e.FileName).ToList();

            for (int i = 0; i < Files.Count; i++)
            {
                bs.Position = HeaderSize + (i * Pair.EntrySize);
                bs.WriteInt32(currentFileNameOffset);

                bs.Position = currentFileNameOffset;
                bs.WriteString(Files[i].FileName, StringCoding.ZeroTerminated);
                currentFileNameOffset = (int)bs.Position;
            }

            bs.AlignWithValue(0x80, 0x5E, true);
            int baseDataOffset = (int)bs.Position; // Align with 0x5E todo
            int currentFileDataOffset = baseDataOffset;

            // Write the buffers
            for (int i = 0; i < Files.Count; i++)
            {
                bs.Position = HeaderSize + (i * Pair.EntrySize) + 4;
                bs.WriteInt32(currentFileDataOffset);
                bs.WriteInt32(Files[i].FileData.Length);

                bs.Position = currentFileDataOffset;
                bs.WriteBytes(Files[i].FileData);

                bs.AlignWithValue(0x80, 0x5E, true);
                currentFileDataOffset = (int)bs.Position;
            }

            bs.Position = 0x10;
            bs.WriteInt32(HeaderSize); // Offset of entries
            bs.WriteInt32(baseFileNameOffset);
            bs.WriteInt32(baseDataOffset);

            Console.WriteLine($"[:] GPB: Done packing {Files.Count} files to {outputFileName}.");
        }

        public class Pair
        {
            public const int EntrySize = 0x10;
            public const int Alignment = 0x80;

            public string FileName { get; set; }
            public byte[] FileData { get; set; }

            public void Read(BinaryStream bs)
            {
                int fileNameOffset = bs.ReadInt32();
                int fileDataOffset = bs.ReadInt32();
                int fileSize = bs.ReadInt32();

                bs.Position = fileNameOffset;
                FileName = bs.ReadString(StringCoding.ZeroTerminated);
                FileData = new byte[fileSize];

                bs.Position = fileDataOffset;
                int read = bs.Read(FileData, 0, fileSize);
                Debug.Assert(read == fileSize, $"Gpb Size @ 0x{fileDataOffset} did not match ({read} != {fileSize})");
            }

        }
    }
}
