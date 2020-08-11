using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpClassDefine : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.CLASS_DEFINE;
        

        public string ClassName { get; set; }
        public List<string> ExtendsFrom { get; set; }
        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            ClassName = Utils.ReadADCString(parent, ref sr);
            ExtendsFrom = Utils.ReadADCStringTable(parent, ref sr);
        }

        public override string ToString()
            => $"{CallType}: {ClassName} extends {ExtendsFrom[^1]}";

        public void Decompile(CodeBuilder builder)
        {
            builder.AppendLine($"class {ClassName} : {ExtendsFrom[^1]}");
            builder.AppendLine("{");
        }
    }
}
