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
    public class UIArray : UIFieldBase
    {
        public byte Length { get; set; }
        public List<UIFieldBase> Elements { get; set; }

        public override void Read(ref SpanReader sr)
        {
            Length = sr.ReadByte();
            Elements = new List<UIFieldBase>(Length);
            for (int i = 0; i < Length; i++)
            {
                Scope scope = new Scope();
                scope.Read(ref sr);
                Elements.Add(scope);
            }
        }

    }
}
