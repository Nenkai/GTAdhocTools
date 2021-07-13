using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Core;
using Syroot.BinaryData;
using Syroot.BinaryData.Memory;

namespace GTAdhocTools.UI.Fields
{
    public class mColorName : mTypeBase
    {
        public string ColorName { get; set; }

        public override void Read(MBinaryIO io)
        {
            if (io.Version == 0)
                ColorName = io.Stream.Read7BitString();
            else
                ColorName = (io.ReadNext() as mString).String;
        }

        public override void Read(MTextIO io)
        {
            ColorName = io.GetString();

            string end = io.GetToken();
            if (end != MTextIO.SCOPE_END.ToString())
                throw new UISyntaxError($"Expected color name scope end ({MTextIO.SCOPE_END}), got {end}");
        }

        public override void WriteText(MTextWriter writer)
        {
            if (Name != null)
            {
                writer.WriteString(Name);
                writer.WriteSpace();
            }

            writer.WriteString("color_name");
            writer.WriteString("{\""); writer.WriteString(ColorName); writer.WriteString("\"}");
            writer.SetNeedNewLine();
        }
    }
}
