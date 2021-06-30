using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using Syroot.BinaryData.Core;
using Syroot.BinaryData;
using Syroot.BinaryData.Memory;
using System.Diagnostics;

namespace GTAdhocTools.UI.Fields
{
    [DebuggerDisplay("mDouble: {Name} ({Value})")]
    public class mDouble : mTypeBase
    {
        public double Value { get; set; }

        public override void Read(MBinaryIO io)
        {
            Value = io.Stream.ReadDouble();
        }

        public override void WriteText(MTextWriter writer)
        {
            writer.WriteString(Name);
            writer.WriteSpace();
            writer.WriteString("digit");
            writer.WriteString("{"); writer.WriteString(Value.ToString(CultureInfo.InvariantCulture)); writer.WriteString("}");

            if (writer.Debug)
                writer.WriteString(" // mDouble");

            writer.SetNeedNewLine();
        }
    }
}
