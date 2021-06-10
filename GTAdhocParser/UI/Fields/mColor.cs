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
    public class mColor : mTypeBase
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public override void Read(MBinaryIO io)
        {
            R = (io.ReadNext() as mUByte).Value;
            G = (io.ReadNext() as mUByte).Value;
            B = (io.ReadNext() as mUByte).Value;
            A = (io.ReadNext() as mUByte).Value;
        }

        public override void WriteText(MTextWriter writer)
        {
            if (Name != null)
            {
                writer.WriteString(Name);
                writer.WriteSpace();
            }

            writer.WriteString("RGBA");
            writer.WriteString("{"); writer.WriteString($"{R} {G} {B} {A}"); writer.WriteString("}");
            writer.SetNeedNewLine();
        }
    }
}
