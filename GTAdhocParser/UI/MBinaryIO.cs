using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Syroot.BinaryData.Core;
using Syroot.BinaryData;
using Syroot.BinaryData.Memory;

using GTAdhocTools.UI.Fields;

namespace GTAdhocTools.UI
{
    public class MBinaryIO
    {
        public string FileName { get; set; }
        public BinaryStream Stream { get; set; }
        public byte Version { get; set; }

        public string CurrentKeyName { get; set; }

        public MBinaryIO(string fileName)
        {
            FileName = fileName;
        }

        public mNode Read()
        {
            using var file = File.Open(FileName, FileMode.Open);
            Stream = new BinaryStream(file, ByteConverter.Big);

            string magic = Stream.ReadString(4);
            if (magic != "MPRJ")
                throw new Exception("Invalid magic, expected MPRJ");

            Version = (byte)Stream.DecodeBitsAndAdvance();
            var rootPrjNode = new mNode();
            if (Version == 1)
                Stream.Position += 1; // Skip scope type
            rootPrjNode.Read(this);
            return rootPrjNode;
        }

        public mTypeBase ReadNext()
            => Read(false);

        public FieldType PeekType()
        {
            FieldType type = (FieldType)Stream.Read1Byte();
            Stream.Position -= 1;
            return type;
        }

        public mTypeBase Read(bool arrayElement = false)
        {
            if (Version == 0)
                return ReadOld(arrayElement);
            else
                return ReadNew();
        }

        private mTypeBase ReadOld(bool arrayElement)
        {
            mTypeBase field = null;

            string fieldName = null;
            uint offsetScopeEnd;

            if (!arrayElement)
            {
                fieldName = Stream.Read7BitString();
                offsetScopeEnd = Stream.ReadUInt32();
            }

            FieldTypeOld typeOld = (FieldTypeOld)Stream.DecodeBitsAndAdvance();

            switch (typeOld)
            {
                case FieldTypeOld.Bool:
                    field = new mBool();
                    break;
                case FieldTypeOld.Float:
                    field = new mFloat();
                    break;
                case FieldTypeOld.Int:
                    field = new mInt();
                    break;
                case FieldTypeOld.String:
                    field = new mString();
                    break;
                case FieldTypeOld.ScopeStart:
                    field = new UIArray();
                    break;
                case FieldTypeOld.ScopeEnd:
                    break;
                default:
                    throw new Exception($"Type: {typeOld} not supported");
            }

            field.Read(this);
            return field;
        }

        private mTypeBase ReadNew()
        {
            mTypeBase field = null;
            FieldType typeNew = (FieldType)Stream.DecodeBitsAndAdvance();


            switch (typeNew)
            {
                case FieldType.UByte:
                    field = new mUByte();
                    break;
                case FieldType.Bool:
                    field = new mBool();
                    break;
                case FieldType.Short:
                    field = new mShort();
                    break;
                case FieldType.Float:
                    field = new mFloat();
                    break;
                case FieldType.Int:
                    field = new mInt();
                    break;
                case FieldType.UInt:
                    field = new mInt();
                    break;
                case FieldType.String:
                    field = new mString();
                    break;
                case FieldType.ScopeStart:
                    field = new mNode();
                    break;
                case FieldType.ScopeEnd:
                    break;
                case FieldType.ArrayMaybe:
                    field = new UIArray();
                    break;
                default:
                    throw new Exception($"Type: {typeNew} not supported");
            }

            if (typeNew != FieldType.ScopeEnd)
                field.Read(this);

            return field;
        }
    }

    public enum FieldType
    {
        Bool = 1,
        Int = 4,
        UByte = 6,
        Short = 7,
        UInt = 8,
        Float = 10,
        String = 12,
        ArrayMaybe = 13,
        ScopeStart = 14,
        ScopeEnd = 15,
    }

    public enum FieldTypeOld
    {
        Bool = 0x80,
        Int = 0x83,
        Float = 0x89,
        String = 0x8B,
        ScopeStart = 0x8C,
        ScopeEnd = 0x8D,
    }
}
