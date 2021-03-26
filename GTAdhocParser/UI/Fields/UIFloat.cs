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
    public class UIFloat : UIFieldBase
    {
        public float Value { get; set; }

        public override void Read(ref SpanReader sr, byte version)
        {
            Value = sr.ReadInt32();
        }
    }
}
