using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GTAdhocTools.UI.Fields
{
    [DebuggerDisplay("mRectangle: {Name} (X:{X1},Y:{Y1},W:{Width},H:{Height})")]
    public class mRectangle : mTypeBase
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public override void Read(MBinaryIO io)
        {
            if (io.Version == 0)
            {
                X = io.Stream.ReadSingle();
                Y = io.Stream.ReadSingle();
                Width = io.Stream.ReadSingle();
                Height = io.Stream.ReadSingle();
            }
            else
            {
                mFloat x = io.ReadNext() as mFloat;
                mFloat y = io.ReadNext() as mFloat;
                mFloat width = io.ReadNext() as mFloat;
                mFloat height = io.ReadNext() as mFloat;

                X = x.Value;
                Y = y.Value;
                Width = width.Value;
                Height = height.Value;
            }
        }

        public override void Read(MTextIO io)
        {
            throw new NotImplementedException();
        }

        public override void WriteText(MTextWriter writer)
        {
            writer.WriteString(Name);
            writer.WriteSpace();
            writer.WriteString("rectangle");
            writer.WriteString("{"); writer.WriteString($"{X} {Y} {Width} {Height}"); writer.WriteString("}");
            writer.SetNeedNewLine();
        }
    }
}
