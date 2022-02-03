using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTAdhocTools.Decompiler;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    /// <summary>
    /// Evaluates a variable value from the variable storage.
    /// </summary>
    public class OpVariableEval : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.VARIABLE_EVAL;

        /// <summary>
        /// One name = local, path = static
        /// </summary>
        public List<string> Names = new List<string>();

        public int StorageIndex;

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Names = Utils.ReadADCStringTable(parent, ref sr);
            StorageIndex = sr.ReadInt32();
        }

        public override string ToString()
            => $"{CallType}: {string.Join(',', Names)}, Index:{StorageIndex}";

        public override void Decompile(CodeBuilder builder)
        {
            if (builder.CurrentFunction != null)
            {
                builder.Variables.Add(new HObject(Names[^1], HObjectType.Variable));
                var argExists = builder.CurrentFunction.Code.Arguments.Exists(e => e.argumentIndex == StorageIndex); // Evaluating function argument?
                if (argExists)
                    return; // Then we don't need it, we already keep track of it
            }
        }
    }
}
