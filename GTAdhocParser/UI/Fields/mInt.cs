using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Core;
using Syroot.BinaryData;
using Syroot.BinaryData.Memory;
using System.Diagnostics;

namespace GTAdhocTools.UI.Fields
{
    [DebuggerDisplay("mInt: {Name} ({Value})")]
    public class mInt : mTypeBase
    {
        public int Value { get; set; }

        public override void Read(MBinaryIO io)
        {
            Value = io.Stream.ReadInt32();
        }

        public override void Read(MTextIO io)
        {
            var numbToken = io.GetNumberToken();
            if (int.TryParse(numbToken, out int val))
                Value = val;
            else
                throw new UISyntaxError($"Unexpected int token for mInt. Got {numbToken}.");

            string end = io.GetToken();
            if (end != MTextIO.SCOPE_END.ToString())
                throw new UISyntaxError($"Expected mInt scope end ({MTextIO.SCOPE_END}), got {end}");
        }

        public override void WriteText(MTextWriter writer)
        {
            writer.WriteString(Name);
            writer.WriteSpace();
            writer.WriteString("digit");
            writer.WriteString("{"); writer.WriteString(Value.ToString()); writer.WriteString("}");

            if (writer.Debug)
                writer.WriteString(" // mInt");

            writer.SetNeedNewLine();
        }
    }
}
