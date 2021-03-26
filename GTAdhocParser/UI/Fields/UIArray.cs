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

        public override void Read(ref SpanReader sr, byte version)
        {
            Length = sr.ReadByte();
            Elements = new List<UIFieldBase>(Length);
            for (int i = 0; i < Length; i++)
            {
                if (version == 0)
                {
                    Scope scope = new Scope();
                    scope.Read(ref sr, version);
                    Elements.Add(scope);
                }
                else
                {
                    UIFieldBase field;
                    var type = (FieldType)sr.ReadByte();

                    switch (type)
                    {
                        case FieldType.Bool:
                            field = new UIBool();
                            break;
                        case FieldType.Float:
                            field = new UIFloat();
                            break;
                        case FieldType.Int:
                            field = new UIInt();
                            break;
                        case FieldType.UInt:
                            field = new UIUInt();
                            break;
                        case FieldType.Color:
                            field = new UIColor();
                            break;
                        case FieldType.String:
                            field = new UIString();
                            break;
                        case FieldType.ScopeStart:
                            field = new Scope();
                            break;
                        case FieldType.ScopeEnd:
                            field = new Scope();
                            break;
                        case FieldType.ArrayMaybe:
                            field = new UIArray();
                            break;
                        default:
                            throw new Exception($"Type: {type} not supported");
                    }

                    if (type != FieldType.ScopeEnd)
                    {
                        field.Read(ref sr, version);
                        Elements.Add(field);
                    }
                }
            }
        }

    }
}
