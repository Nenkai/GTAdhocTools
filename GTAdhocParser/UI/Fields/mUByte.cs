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
    [DebuggerDisplay("mUByte: {Name} ({Value})")]
    public class mUByte : mTypeBase
    {
        public byte Value { get; set; }

        public override void Read(MBinaryIO io)
        {
            Value = io.Stream.Read1Byte();
        }

        public override void WriteText(MTextWriter writer)
        {
            throw new NotImplementedException();

        }
    }
}
