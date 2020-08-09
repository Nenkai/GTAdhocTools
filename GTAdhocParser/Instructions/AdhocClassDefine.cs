using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocParser.Instructions
{
    public class OpClassDefine : IAdhocInstruction
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.CLASS_DEFINE;
        public uint Unknown { get; set; }

        public string ClassName { get; set; }
        public List<string> ExtendsFrom { get; set; }
        public void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            ClassName = Utils.ReadADCString(parent, ref sr);
            ExtendsFrom = Utils.ReadADCStringTable(parent, ref sr);
        }

        public override string ToString()
            => $"{Unknown, 4}| {CallType}: {ClassName} extends {ExtendsFrom[^1]}";

        public void GetHumanCode(CodeBuilder builder)
        {
            builder.AppendLine($"class {ClassName} : {ExtendsFrom[^1]}");
            builder.AppendLine("{");
            builder.IndentationDepth++;
        }
    }
}
