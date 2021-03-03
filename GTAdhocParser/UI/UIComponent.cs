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
    [DebuggerDisplay("{FieldName} ({Type})")]
    public class UIComponent
    {
        public string FieldName { get; set; }
        public uint OffsetScopeEnd { get; set; }
        public byte Unk { get; set; }
        public FieldType Type { get; set; }
        public UIFieldBase Field { get; set; }

        public void Read(ref SpanReader sr, bool arrayElement = false)
        {
            if (!arrayElement)
            {
                FieldName = sr.ReadString1();
                OffsetScopeEnd = sr.ReadUInt32();
            }

            Unk = sr.ReadByte();
            Type = (FieldType)sr.ReadByte();

            switch (Type)
            {
                case FieldType.Bool:
                    Field = new UIBool();
                    break;
                case FieldType.Float:
                    Field = new UIFloat();
                    break;
                case FieldType.Int:
                    Field = new UIInt();
                    break;
                case FieldType.String:
                    Field = new UIString();
                    break;
                case FieldType.ScopeStart:
                    Field = new UIArray();
                    break;
                case FieldType.ScopeEnd:
                    Field = new Scope();
                    break;
                default:
                    throw new Exception($"Type: {Type} not supported");
            }

            Field.Read(ref sr);
        }

        public enum FieldType
        {
            Bool = 0x80,
            Int = 0x83,
            Float = 0x89,
            String = 0x8B,
            ScopeStart = 0x8C,
            ScopeEnd = 0x8D,
        }
    }
}
