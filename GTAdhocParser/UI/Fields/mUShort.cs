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
    [DebuggerDisplay("mUShort: {Name} ({Value})")]
    public class mUShort : mTypeBase
    {
        public ushort Value { get; set; }

        public override void Read(MBinaryIO io)
        {
            Value = io.Stream.ReadUInt16();
        }

        public override void Read(MTextIO io)
        {
            throw new NotImplementedException();
        }

        public override void WriteText(MTextWriter writer)
        {
            writer.WriteString(Name);
            writer.WriteSpace();
            writer.WriteString("digit");
            writer.WriteString("{"); writer.WriteString(Value.ToString()); writer.WriteString("}");

            if (writer.Debug)
                writer.WriteString(" // mUShort");

            writer.SetNeedNewLine();
        }
    }
}
