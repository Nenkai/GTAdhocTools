﻿using System;
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
    [DebuggerDisplay("mSByte: {Name} ({Value})")]
    public class mSByte : mTypeBase
    {
        public sbyte Value { get; set; }

        public override void Read(MBinaryIO io)
        {
            Value = io.Stream.ReadSByte();
        }

        public override void WriteText(MTextWriter writer)
        {
            writer.WriteString(Name);
            writer.WriteSpace();
            writer.WriteString("digit");
            writer.WriteString("{"); writer.WriteString(Value.ToString()); writer.WriteString("}");

            if (writer.Debug)
                writer.WriteString(" // mSByte");

            writer.SetNeedNewLine();
        }
    }
}
