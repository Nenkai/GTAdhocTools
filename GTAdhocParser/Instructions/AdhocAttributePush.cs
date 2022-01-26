using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTAdhocTools.Decompiler;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpAttributePush : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.ATTRIBUTE_PUSH;
        

        public List<string> Attributes;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            if (parent.Version <= 5)
            {
                string attrName = Utils.ReadADCString(parent, ref sr);
                Attributes = new List<string>(1);
                Attributes.Add(attrName);
            }
            else
                Attributes = Utils.ReadADCStringTable(parent, ref sr);
        }

        public override string ToString()
           => $"{CallType}: {string.Join(',', Attributes)}";

        public override void Decompile(CodeBuilder builder)
        {
            if (builder.Variables.Count != 0)
            {
                var newVariable = new HObject(Attributes[^1], HObjectType.Variable);
                var top = builder.GetTopVariable();

                // Update the old's parent with old
                top.Parent = top;

                // Old is new
                top = newVariable;

                // Now new's parent points to old
            }
        }
    }
}
