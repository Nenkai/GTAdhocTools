using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    /// <summary>
    /// Pushes a new variable into the variable storage.
    /// </summary>
    public class OpVariablePush : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.VARIABLE_PUSH;
        
        /// <summary>
        /// One name = local, path = static
        /// </summary>
        public List<string> Names { get; set; }

        public uint StorageIndex { get; set; }

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            Names = Utils.ReadADCStringTable(parent, ref sr);
            StorageIndex = sr.ReadUInt32();
        }

        public override string ToString()
            => $"{CallType}: {string.Join(',', Names)}, PushAt:{StorageIndex}";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
