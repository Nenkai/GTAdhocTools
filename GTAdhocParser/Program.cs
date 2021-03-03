using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

using GTAdhocTools.UI;
using CommandLine;
using CommandLine.Text;

namespace GTAdhocTools
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments provided.");
                return;
            }

            if (args[0].EndsWith(".adc"))
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

                adc.Disassemble(Path.ChangeExtension(args[0], ".ad"), withOffset);

                if (adc.Version == 12)
                    adc.PrintStrings(Path.ChangeExtension(args[0], ".strings"));
            }
            else
            {
                Parser.Default.ParseArguments<PackVerbs, UnpackVerbs>(args)
                    .WithParsed<PackVerbs>(Pack)
                    .WithParsed<UnpackVerbs>(Unpack);
            }
        }

        public static void Pack(PackVerbs packVerbs)
        {
            if (packVerbs.OutputPath.EndsWith("gpb"))
            {
                var gpb = GpbData.CreateFromFolder(packVerbs.InputPath);
                gpb.Pack(packVerbs.OutputPath, !packVerbs.LittleEndian);
            }
            else if (packVerbs.OutputPath.EndsWith("mpackage"))
            {
                AdhocPackage.PackFromFolder(packVerbs.InputPath, packVerbs.OutputPath);
            }
            else
            {
                Console.WriteLine("Found nothing to pack - ensure the provided output path has the proper file extension (gpb/mpackage)");
            }
        }

        public static void Unpack(UnpackVerbs unpackVerbs)
        {
            if (unpackVerbs.InputPath.EndsWith("gpb"))
            {
                Console.WriteLine("Assuming input is GPB");
                var gpb = GpbData.Read(unpackVerbs.InputPath);
                gpb.Unpack(Path.GetFileNameWithoutExtension(unpackVerbs.InputPath), unpackVerbs.OutputPath);
            }
            else if (unpackVerbs.InputPath.EndsWith("mpackage"))
            {
                Console.WriteLine("Assuming input is MPackage");
                AdhocPackage.ExtractPackage(unpackVerbs.InputPath);
            }
            else
            {
                Console.WriteLine("Found nothing to unpack - ensure the provided input file has the proper file extension (gpb/mpackage)");
            }
        }
    }

    [Verb("unpack", HelpText = "Unpack files like gpb's, or mpackage's.")]
    public class UnpackVerbs
    {
        [Option('i', "input", Required = true, HelpText = "Input file. (GPB's, mPackages)")]
        public string InputPath { get; set; }

        [Option('o', "output", HelpText = "Output folder for unpacked files.")]
        public string OutputPath { get; set; }

    }

    [Verb("pack", HelpText = "Pack files like gpb's, or mpackage's.")]
    public class PackVerbs
    {
        [Option('i', "input", Required = true, HelpText = "Input folder.")]
        public string InputPath { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output folder for packed files.")]
        public string OutputPath { get; set; }

        [Option("le", HelpText = "Pack as little endian?")]
        public bool LittleEndian { get; set; }

    }
}
