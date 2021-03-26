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
        public FieldTypeOld TypeOld { get; set; }
        public FieldType TypeNew { get; set; }
        public UIFieldBase Field { get; set; }

        public void Read(ref SpanReader sr, byte version, bool arrayElement = false)
        {
            if (version == 0)
                ReadOld(ref sr, arrayElement);
            else
                ReadNew(ref sr);
        }

        public void ReadOld(ref SpanReader sr, bool arrayElement)
        {
            if (!arrayElement)
            {
                FieldName = sr.ReadString1();
                OffsetScopeEnd = sr.ReadUInt32();
            }

            Unk = sr.ReadByte();
            TypeOld = (FieldTypeOld)sr.ReadByte();

            switch (TypeOld)
            {
                case FieldTypeOld.Bool:
                    Field = new UIBool();
                    break;
                case FieldTypeOld.Float:
                    Field = new UIFloat();
                    break;
                case FieldTypeOld.Int:
                    Field = new UIInt();
                    break;
                case FieldTypeOld.String:
                    Field = new UIString();
                    break;
                case FieldTypeOld.ScopeStart:
                    Field = new UIArray();
                    break;
                case FieldTypeOld.ScopeEnd:
                    Field = new Scope();
                    break;
                default:
                    throw new Exception($"Type: {TypeOld} not supported");
            }

            Field.Read(ref sr, 0);
        }

        public void ReadNew(ref SpanReader sr)
        {
            TypeNew = (FieldType)sr.ReadByte();

            switch (TypeNew)
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
                case FieldType.UInt:
                    Field = new UIInt();
                    break;
                case FieldType.Color:
                    Field = new UIColor();
                    break;
                case FieldType.String:
                    Field = new UIString();
                    break;
                case FieldType.ScopeStart:
                    Field = new Scope();
                    break;
                case FieldType.ScopeEnd:
                    Field = new Scope();
                    break;
                case FieldType.ArrayMaybe:
                    Field = new UIArray();
                    break;
                default:
                    throw new Exception($"Type: {TypeNew} not supported");
            }

            if (TypeNew != FieldType.ScopeEnd)
                Field.Read(ref sr, 1);
        }
    }

    public enum FieldType
    {
        Bool = 1,
        Int = 4,
        Color = 6,
        UInt = 8,
        Float = 10,
        String = 12,
        ArrayMaybe = 13,
        ScopeStart = 14,
        ScopeEnd = 15,
    }

    public enum FieldTypeOld
    {
        Bool = 0x80,
        Int = 0x83,
        Float = 0x89,
        String = 0x8B,
        ScopeStart = 0x8C,
        ScopeEnd = 0x8D,
    }
}
