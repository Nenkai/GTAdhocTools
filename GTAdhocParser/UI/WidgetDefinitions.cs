using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GTAdhocTools.UI
{
    public class WidgetDefinitions
    {

        public static Dictionary<string, UIDefType> Types = new();

        static WidgetDefinitions()
        {
            Read();
        }

        public static void Read()
        {
            var txt = File.ReadAllLines("UIWidgetDefinitions.txt");
            foreach (var line in txt)
            {
                if (string.IsNullOrEmpty(line) || line.StartsWith("//"))
                    continue;

                var spl = line.Split('|');
                if (spl.Length <= 1)
                    continue;

                if (spl[0] == "add_field" && spl.Length == 3)
                {
                    if (Enum.TryParse(spl[2], out UIDefType res))
                        Types.Add(spl[1], res);
                }
            }
        }
    }

    public enum UIDefType
    {
        Unknown,
        Float,
        UInt,
        Int,
        Long,
        ULong,
        Double,
        Byte,
        SByte,
        Short,
        UShort,
        Bool,
    }
}
