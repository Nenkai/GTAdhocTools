using System;
using System.IO;
using System.IO.Compression;
namespace GTAdhocParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var adc = AdhocFile.ReadFromFile(args[0]);
            adc.Disassemble(Path.GetFileNameWithoutExtension(args[0]) + ".ad");
        }
    }
}
