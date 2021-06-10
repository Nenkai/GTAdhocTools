using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GTAdhocTools.UI.Fields
{
    [DebuggerDisplay("mRectangle: {Name} ({X1},{Y1},{X2},{Y2})")]
    public class mRectangle : mTypeBase
    {
        public override void Read(MBinaryIO io)
        {
            mFloat x1 = io.ReadNext() as mFloat;
            mFloat y1 = io.ReadNext() as mFloat;
            mFloat x2 = io.ReadNext() as mFloat;
            mFloat y2 = io.ReadNext() as mFloat;

            X1 = x1.Value;
            Y1 = y1.Value;
            X2 = x2.Value;
            Y2 = y2.Value;
        }

        public float X1 { get; set; }
        public float Y1 { get; set; }
        public float X2 { get; set; }
        public float Y2 { get; set; }

        public override void WriteText(MTextWriter writer)
        {
            writer.WriteString(Name);
            writer.WriteSpace();
            writer.WriteString("rectangle");
            writer.WriteString("{"); writer.WriteString($"{X1} {Y1} {X2} {Y2}"); writer.WriteString("}");
            writer.SetNeedNewLine();
        }
    }
}
