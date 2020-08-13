using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

using GTAdhocParser.Decompiler;

namespace GTAdhocParser.Instructions
{
    public class OpAttributeEval : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.ATTRIBUTE_EVAL;
        

        public List<string> Names = new List<string>();

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Names = Utils.ReadADCStringTable(parent, ref sr);
        }

        public override string ToString()
            => $"{CallType}: {string.Join(',', Names)}";

        public override void Decompile(CodeBuilder builder)
        {
            var newVariable = new HObject(Names[^1], HObjectType.Variable);
            var top = builder.GetTopVariable();

            // Update the old's parent with old
            var old = top;

            // Old is new
            top = newVariable;
            top.Parent = old;

            top.Name = $"{top.Parent}.{top.Name}";
            builder.Variables[^1] = top;
            // Now new's parent points to old
        }
    }
}
