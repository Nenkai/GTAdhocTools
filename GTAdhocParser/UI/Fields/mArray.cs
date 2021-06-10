using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Core;
using Syroot.BinaryData;
using Syroot.BinaryData.Memory;
using System.Diagnostics;

namespace GTAdhocTools.UI.Fields
{
    [DebuggerDisplay("mNodeArray: {Name} ({Length} elements)")]
    public class UIArray : mTypeBase
    {
        public byte Length { get; set; }
        public List<mTypeBase> Elements { get; set; }

        public override void Read(MBinaryIO io)
        {
            Length = io.Stream.Read1Byte();
            Elements = new List<mTypeBase>(Length);
            for (int i = 0; i < Length; i++)
            {
                if (io.Version == 0)
                {
                    mNode scope = new mNode();
                    scope.Read(io);
                    Elements.Add(scope);
                }
                else
                {
                    mTypeBase field = null;
                    var type = (FieldType)io.Stream.ReadByte();
                    switch (type)
                    {
                        case FieldType.Bool:
                            field = new mBool();
                            break;
                        case FieldType.Float:
                            field = new mFloat();
                            break;
                        case FieldType.Int:
                            field = new mInt();
                            break;
                        case FieldType.Short:
                            field = new mShort();
                            break;
                        case FieldType.UInt:
                            field = new UIUInt();
                            break;
                        case FieldType.UByte:
                            field = new mUByte();
                            break;
                        case FieldType.String:
                            field = new mString();
                            break;
                        case FieldType.ScopeStart:
                            field = new mNode();
                            break;
                        case FieldType.ScopeEnd:
                            break;
                        case FieldType.ArrayMaybe:
                            field = new UIArray();
                            break;
                        default:
                            throw new Exception($"Type: {type} not supported");
                    }

                    if (type != FieldType.ScopeEnd)
                    {
                        field.Read(io);
                        if (field is mString str)
                        {
                            if (str.String == "RGBA")
                            {
                                // Array of colors
                                field = new mColor();
                                field.Read(io);
                            }
                            else if (str.String == "color_name")
                            {
                                field = new mColorName();
                                field.Read(io);
                            }
                            else if (str.String == "vector")
                            {
                                field = new mVector();
                                field.Read(io);
                            }
                            else if (str.String == "vector3")
                            {
                                field = new mVector3();
                                field.Read(io);
                            }
                        }

                        Elements.Add(field);
                    }
                }
            }
        }

        public override void WriteText(MTextWriter writer)
        {
            writer.WriteString(Name);
            writer.WriteString($"[{Length}]");
            writer.WriteOpenScope();

            foreach (var elem in Elements)
            {
                elem.WriteText(writer);
            }

            writer.WriteEndScope();
        }

    }
}
