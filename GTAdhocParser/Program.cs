﻿using System;
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

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            if (args[0].EndsWith(".mpackage"))
            {
                AdhocPackage.ExtractPackage(args[0]);
            }
            else
            {
                AdhocFile adc = null;
                bool withOffset = true;
                try
                {
                    adc = AdhocFile.ReadFromFile(args[0]);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Errored while reading: {e.Message}");
                }

                //adc.Decompile(Path.GetFileNameWithoutExtension(args[0]) + ".ad");
                adc.Disassemble(Path.ChangeExtension(args[0], ".ad"), withOffset);
                adc.PrintStrings(Path.ChangeExtension(args[0], ".strings"));
            }
        }
    }
}
