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
    public class UIString : UIFieldBase
    {
        public string String { get; set; }

        public override void Read(ref SpanReader sr)
        {
            String = sr.ReadString1();
        }
    }
}
