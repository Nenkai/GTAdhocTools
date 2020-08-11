using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace GTAdhocParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var adc = AdhocFile.ReadFromFile(args[0]);

            bool withOffset = false;
            if (args.Contains("--offset"))
                withOffset = true;

            adc.Disassemble(Path.GetFileNameWithoutExtension(args[0]) + ".ad", withOffset);
        }
    }
}
