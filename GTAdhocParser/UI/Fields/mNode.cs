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
    [DebuggerDisplay("mNode: {Name} ({TypeName})")]
    public class mNode : mTypeBase
    {
        public string TypeName { get; set; }

        public int EndScopeOffset { get; set; }

        public List<mTypeBase> Child { get; set; } = new List<mTypeBase>();

        // For old version reading
        public bool IsRoot { get; set; }

        public override void Read(MBinaryIO io)
        {
            if (io.Version == 0)
            {
                if (IsRoot)
                {
                    EndScopeOffset = io.Stream.ReadInt32();
                    TypeName = io.Stream.Read7BitString();
                }

                while (io.Stream.Position + 2 < EndScopeOffset)
                {
                    string fieldName = io.Stream.Read7BitString();

                    int endOffset = io.Stream.ReadInt32();

                    mTypeBase field;
                    byte[] typeName = io.Stream.Read7BitStringBytes();
                    if (typeName.Length == 1)
                    {
                        FieldTypeOld old = (FieldTypeOld)typeName[0];
                        field = old switch
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
                            _ => throw new NotSupportedException($"Received unsupported node type {old}"),
                        };
                    }
                    else
                    {
                        string customFieldType = Encoding.UTF8.GetString(typeName);

                        field = customFieldType switch
                        {
                            "rectangle" => new mRectangle(),
                            "RGBA" => new mColor(),
                            "color_name" => new mColorName(),
                            "vector" => new mVector(),
                            "vector3" => new mVector3(),
                            "region" => new mRegion(),
                            _ => null,
                        };

                        if (field is null)
                        {
                            field = new mNode();
                            ((mNode)field).EndScopeOffset = endOffset;
                            ((mNode)field).TypeName = customFieldType;
                        }
                    }

                    field.Name = fieldName;
                    //Console.WriteLine($"Reading: {field.Name} ({field})");
                    field.Read(io);

                    Child.Add(field);
                }

                Debug.Assert(io.Stream.ReadInt16() == 0x18d, "Scope terminator did not match");
            }
            else if (io.Version == 1)
            {
                TypeName = io.Stream.Read7BitString();
                mTypeBase field = null;
                do
                {
                    field = io.ReadNext();
                    if (field is mString str) // Key Name
                    {
                        io.CurrentKeyName = str.String;
                        field = io.ReadNext();
                        
                        if (field is mString str2 && io.CurrentKeyName != "name")
                        {
                            // Specific types, kind of hardcoded
                            if (str2.String == "rectangle")
                            {
                                field = new mRectangle();
                                field.Read(io);
                            }
                            else if (str2.String == "RGBA")
                            {
                                field = new mColor();
                                field.Read(io);
                            }
                            else if (str2.String == "color_name")
                            {
                                field = new mColorName();
                                field.Read(io);
                            }
                            else if (str2.String == "vector")
                            {
                                field = new mVector();
                                field.Read(io);
                            }
                            else if (str2.String == "vector3")
                            {
                                field = new mVector3();
                                field.Read(io);
                            }
                            else if (str2.String == "region")
                            {
                                field = new mRegion();
                                field.Read(io);
                            }
                        }
                        
                        field.Name = str.String;
                    }

                    if (field != null)
                        Child.Add(field);

                } while (field != null);
            }
        }

        public override void WriteText(MTextWriter writer)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                writer.WriteString(Name);
                writer.WriteSpace();
            }

            if (TypeName.Contains("::"))
                writer.WriteString($"\'{TypeName}\'");
            else
                writer.WriteString(TypeName);

            writer.WriteOpenScope();

            foreach (var node in Child)
                node.WriteText(writer);

            writer.WriteEndScope();
        }
    }
}
