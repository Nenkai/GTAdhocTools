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
    public class UIUInt : UIFieldBase
    {
        public uint Value { get; set; }

        public override void Read(ref SpanReader sr, byte version)
        {
            Value = sr.ReadUInt32();
        }
    }
}
