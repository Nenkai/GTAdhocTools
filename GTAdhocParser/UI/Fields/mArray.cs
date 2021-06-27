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
    public class mArray : mTypeBase
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
                    int endScope = io.Stream.ReadInt32();
                    byte[] typeName = io.Stream.Read7BitStringBytes();

                    mTypeBase element;
                    if (typeName.Length == 1)
                    {
                        element = (FieldTypeOld)typeName[0] switch
                        {
                            FieldTypeOld.String => new mString(),
                            FieldTypeOld.Array => new mArray(),
                            FieldTypeOld.Bool => new mBool(),
                            FieldTypeOld.Byte => new mUByte(),
                            FieldTypeOld.Double => new mDouble(),
                            FieldTypeOld.Float => new mFloat(),
                            FieldTypeOld.Long => new mLong(),
                            FieldTypeOld.SByte => new mSByte(),
                            FieldTypeOld.Short => new mShort(),
                            FieldTypeOld.UShort => new mUShort(),
                            FieldTypeOld.ULong => new mULong(),
                            FieldTypeOld.Int => new mInt(),
                            FieldTypeOld.UInt => new mUInt(),
                            _ => throw new NotSupportedException($"Received unsupported array element type {(FieldTypeOld)typeName[0]}"),
                        };
                    }
                    else
                    {
                        string customFieldType = Encoding.UTF8.GetString(typeName);

                        element = customFieldType switch
                        {
                            "rectangle" => new mRectangle(),
                            "RGBA" => new mColor(),
                            "color_name" => new mColorName(),
                            "vector" => new mVector(),
                            "vector3" => new mVector3(),
                            "region" => new mRegion(),
                            _ => null,
                        };

                        if (element is null)
                        {
                            element = new mNode();
                            ((mNode)element).EndScopeOffset = endScope;
                            ((mNode)element).TypeName = customFieldType;
                        }
                    }

                    element.Read(io);
                    Elements.Add(element);
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
                            field = new mUInt();
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
                            field = new mArray();
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
                            else if (str.String == "region")
                            {
                                field = new mRegion();
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
