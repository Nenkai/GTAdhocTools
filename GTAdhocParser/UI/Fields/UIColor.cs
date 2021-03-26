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
    public class UIColor : UIFieldBase
    {
        public ushort R;
        public ushort G;
        public ushort B;
        public byte A;
        public override void Read(ref SpanReader sr, byte version)
        {
            R = sr.ReadUInt16();
            G = sr.ReadUInt16();
            B = sr.ReadUInt16();
            A = sr.ReadByte();
        }
    }
}
