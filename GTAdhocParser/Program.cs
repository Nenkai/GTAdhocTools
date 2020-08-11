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
            if (args.Length == 0)
            {
                Console.WriteLine("Missing file.");
                return;
            }

            AdhocFile adc = null;
            bool withOffset = false;
            try
            {
                adc = AdhocFile.ReadFromFile(args[0]);

                if (args.Contains("--offset"))
                    withOffset = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Errored while reading: {e.Message}");
            }

            adc.Disassemble(Path.GetFileNameWithoutExtension(args[0]) + ".ad", withOffset);
        }
    }
}
