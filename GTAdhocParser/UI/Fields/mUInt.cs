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
    [DebuggerDisplay("mUInt: {Name} ({Value})")]
    public class UIUInt : mTypeBase
    {
        public uint Value { get; set; }

        public override void Read(MBinaryIO io)
        {
            Value = io.Stream.ReadUInt32();
        }

        public override void WriteText(MTextWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
