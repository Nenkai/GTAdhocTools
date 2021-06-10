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
    [DebuggerDisplay("mFloat: {Name} ({Value})")]
    public class mFloat : mTypeBase
    {
        public float Value { get; set; }

        public override void Read(MBinaryIO io)
        {
            Value = io.Stream.ReadSingle();
        }

        public override void WriteText(MTextWriter writer)
        {
            writer.WriteString(Name);
            writer.WriteSpace();
            writer.WriteString("digit");
            writer.WriteString("{"); writer.WriteString(Value.ToString(CultureInfo.InvariantCulture)); writer.WriteString("}");
            writer.SetNeedNewLine();
        }
    }
}
