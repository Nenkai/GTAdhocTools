using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Syroot.BinaryData.Memory;

namespace GTAdhocTools.Instructions
{
    public class OpSetState : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.SET_STATE;
        

        public AdhocRunState State { get; set; }
        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            State = (AdhocRunState)sr.ReadByte();
        }

        public override string ToString()
            => $"{CallType}: State={State} ({(byte)State})";

        public override void Decompile(CodeBuilder builder)
        {
            throw new NotImplementedException();
        }

        public enum AdhocRunState : byte
        {
            /// <summary>
            /// Script is terminating
            /// </summary>
            EXIT = 0,

            /// <summary>
            /// Script scope is over
            /// </summary>
            RETURN = 1,

            YIELD = 2,

            /// <summary>
            /// Script exception
            /// </summary>
            EXCEPTION = 3,

            CALL = 4,

            RUN = 5,
        }
    }
}
