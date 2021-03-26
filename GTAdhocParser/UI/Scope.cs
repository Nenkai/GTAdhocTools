using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Syroot.BinaryData.Core;
using Syroot.BinaryData;
using Syroot.BinaryData.Memory;

using GTAdhocTools.UI.Fields;

namespace GTAdhocTools.UI
{
    [DebuggerDisplay("{Name}")]
    public class Scope : UIFieldBase
    {
        public byte Unk { get; set; }
        public int EndScopeOffset { get; set; }
        public string Name { get; set; }

        public List<UIComponent> Child { get; set; } = new List<UIComponent>();

        public override void Read(ref SpanReader sr, byte version)
        {
            if (version == 0)
            {
                EndScopeOffset = sr.ReadInt32();
                Name = sr.ReadString1();

                while (sr.Position < EndScopeOffset - 2)
                {
                    var comp = new UIComponent();
                    comp.Read(ref sr, version);
                    Child.Add(comp);
                }

                Debug.Assert(sr.ReadInt16() == 0x18d, "Scope terminator did not match");
            }
            else if (version == 1)
            {
                Name = sr.ReadString1();
                UIComponent comp;
                do
                {
                    comp = new UIComponent();
                    comp.Read(ref sr, version);
                    
                    if (comp.TypeNew != FieldType.ScopeEnd)
                        Child.Add(comp);

                } while (comp.TypeNew != FieldType.ScopeEnd);

            }
        }
    }
}
