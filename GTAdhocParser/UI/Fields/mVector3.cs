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
    [DebuggerDisplay("mVector3: {Name} ({X} {Y} {Z})")]
    public class mVector3 : mTypeBase
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public override void Read(MBinaryIO io)
        {
            X = (io.ReadNext() as mFloat).Value;
            Y = (io.ReadNext() as mFloat).Value;
            Z = (io.ReadNext() as mFloat).Value;
        }

        public override void WriteText(MTextWriter writer)
        {
            writer.WriteString(Name);
            writer.WriteSpace();
            writer.WriteString("vector3");
            writer.WriteString("{"); writer.Write(X); writer.WriteSpace(); writer.Write(Y); writer.WriteSpace(); writer.Write(Z); writer.WriteString("}");
            writer.SetNeedNewLine();
        }
    }
}
