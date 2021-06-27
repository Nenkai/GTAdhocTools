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

        public override void Read(MBinaryIO io)
        {
            if (io.Version == 0)
            {
                EndScopeOffset = io.Stream.ReadInt32();
                Name = io.Stream.Read7BitString();

                while (io.Stream.Position < EndScopeOffset - 2)
                {
                    var field = io.ReadNext();
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
                        
                        if (field is mString str2)
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
