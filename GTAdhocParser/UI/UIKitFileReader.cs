using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Syroot.BinaryData.Core;
using Syroot.BinaryData;
using Syroot.BinaryData.Memory;
namespace GTAdhocTools.UI
{
    public class UIKitFileReader
    {
        public static Scope Read(string fileName)
        {
            var file = File.ReadAllBytes(fileName);

            SpanReader sr = new SpanReader(file, Endian.Big);
            string magic = sr.ReadStringRaw(4);
            if (magic != "MPRJ")
                throw new Exception("Invalid magic, expected MPRJ");

            sr.ReadByte();
            var component = new Scope();
            component.Read(ref sr);
            return component;
        }
    }
}
