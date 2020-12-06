using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Syroot.BinaryData.Memory;

using GTAdhocParser.Instructions;

namespace GTAdhocParser
{
    public class AdhocCode : InstructionBase
    {
        public AdhocCallType CallType { get; set; } = AdhocCallType.METHOD_DEFINE;

        /// <summary>
        /// Instructions for this code.
        /// </summary>
        public List<InstructionBase> Components = new List<InstructionBase>();

        /// <summary>
        /// Arguments if the current component is a function call.
        /// The first argument is always "self", if declaring a class method.
        /// </summary>
        public List<(string argumentName, uint argumentIndex)> Arguments = new List<(string, uint)>();


        public List<string> unkStr2 = new List<string>();

        /// <summary>
        /// Source File Name & Line Numbers
        /// </summary>
        public bool HasDebuggingInformation { get; set; }

        /// <summary>
        /// Source File Name for this code.
        /// </summary>
        public string OriginalSourceFile { get; set; }
        public byte CodeVersion { get; set; }

        public int Unk1;
        public int Unk2;
        public int Unk3;

        public uint InstructionCount { get; set; }
        public uint InstructionCountOffset { get; set; }

        public override void Deserialize(AdhocFile parent, ref SpanReader sr)
        {
            if (parent.Version < 8)
            {
                OriginalSourceFile = Utils.ReadADCString(parent, ref sr);

                if (parent.Version > 3)
                {
                    uint argCount = sr.ReadUInt32();
                    for (int i = 0; i < argCount; i++)
                        Arguments.Add( (Utils.ReadADCString(parent, ref sr), 0));
                }
            }
            else
            {
                HasDebuggingInformation = sr.ReadBoolean();
                CodeVersion = sr.ReadByte();

                if (parent.Version != 8) // Why PDI? Changed your mind after 8?
                {
                    if (HasDebuggingInformation)
                        OriginalSourceFile = Utils.ReadADCString(parent, ref sr);
                }
                

                if (parent.Version >= 12)
                {
                    byte unk = sr.ReadByte();
                }

                uint argCount = sr.ReadUInt32();

                if (argCount > 0)
                {
                    for (int i = 0; i < argCount; i++)
                    {
                        string argName = Utils.ReadADCString(parent, ref sr);
                        Arguments.Add((argName, sr.ReadUInt32()));
                    }
                }

                uint unkCount2 = sr.ReadUInt32();
                if (unkCount2 > 0)
                {
                    for (int i = 0; i < unkCount2; i++)
                    {
                        string u = Utils.ReadADCString(parent, ref sr);
                        unkStr2.Add(u);
                        sr.ReadUInt32();
                    }
                }

                uint unkCount3 = sr.ReadUInt32();
            }

            if (parent.Version <= 10)
            {
                Unk2 = sr.ReadInt32();
                Unk1 = sr.ReadInt32();
                Unk3 = Unk2;
            }
            else
            {

                Unk1 = sr.ReadInt32();
                Unk2 = sr.ReadInt32();
                Unk3 = sr.ReadInt32();
            }

            InstructionCountOffset = (uint)sr.Position;
            InstructionCount = sr.ReadUInt32();
            if (InstructionCount < 0x40000000)
            {
                for (int i = 0; i < InstructionCount; i++)
                {
                    uint originalLineNumber = 0;
                    if (HasDebuggingInformation) 
                        originalLineNumber = sr.ReadUInt32();

                    AdhocCallType type = (AdhocCallType)sr.ReadByte();

                    ReadComponent(parent, originalLineNumber, type, ref sr);
                }
            }
        }

        public void ReadComponent(AdhocFile parent, uint lineNumber, AdhocCallType type, ref SpanReader sr)
        {
            InstructionBase component = GetByType(type);
            if (component != null)
            {
                component.InstructionOffset = (uint)sr.Position + 4;
                component.SourceLineNumber = lineNumber;
                component.Deserialize(parent, ref sr);
                Components.Add(component);
            }
        }

        public static InstructionBase GetByType(AdhocCallType type)
        {
            switch (type)
            {
                case AdhocCallType.MODULE_DEFINE:
                    return new OpModule();
                case AdhocCallType.METHOD_DEFINE:
                case AdhocCallType.FUNCTION_DEFINE:
                case AdhocCallType.FUNCTION_CONST:
                case AdhocCallType.METHOD_CONST:
                    return new OpMethod(type);
                case AdhocCallType.VARIABLE_EVAL:
                    return new OpVariableEval();
                case AdhocCallType.CALL:
                    return new OpCall();
                case AdhocCallType.CALL_OLD:
                    return new OpCall() { CallType = type };
                case AdhocCallType.JUMP_IF_FALSE:
                    return new OpJumpIfFalse();
                case AdhocCallType.FLOAT_CONST:
                    return new OpFloatConst();
                case AdhocCallType.ATTRIBUTE_PUSH:
                    return new OpAttributePush();
                case AdhocCallType.ASSIGN_POP:
                    return new OpAssignPop(); 
                case AdhocCallType.LEAVE:
                    return new OpLeave();
                case AdhocCallType.VOID_CONST:
                    return new OpVoidConst();
                case AdhocCallType.SET_STATE:
                    return new OpSetState();
                case AdhocCallType.SET_STATE_OLD:
                    return new OpSetState() { CallType = type };
                case AdhocCallType.NIL_CONST:
                    return new OpNilConst();
                case AdhocCallType.ATTRIBUTE_DEFINE:
                    return new OpAttributeDefine();
                case AdhocCallType.BOOL_CONST:
                    return new OpBoolConst();
                case AdhocCallType.SOURCE_FILE:
                    return new OpSourceFile();
                case AdhocCallType.IMPORT:
                    return new OpImport();
                case AdhocCallType.STRING_CONST:
                    return new OpStringConst();
                case AdhocCallType.POP:
                    return new OpPop();
                case AdhocCallType.POP_OLD:
                    return new OpPop() { CallType = type };
                case AdhocCallType.CLASS_DEFINE:
                    return new OpClassDefine();
                case AdhocCallType.ATTRIBUTE_EVAL:
                    return new OpAttributeEval();
                case AdhocCallType.INT_CONST:
                    return new OpIntConst();
                case AdhocCallType.STATIC_DEFINE:
                    return new OpStaticDefine();
                case AdhocCallType.VARIABLE_PUSH:
                    return new OpVariablePush();
                case AdhocCallType.BINARY_OPERATOR:
                    return new OpBinaryOperator();
                case AdhocCallType.JUMP:
                    return new OpJump();
                case AdhocCallType.ELEMENT_EVAL:
                    return new OpElementEval();
                case AdhocCallType.STRING_PUSH:
                    return new OpStringPush();
                case AdhocCallType.JUMP_IF_TRUE:
                    return new OpJumpIfTrue();
                case AdhocCallType.EVAL:
                    return new OpEval();
                case AdhocCallType.BINARY_ASSIGN_OPERATOR:
                    return new OpBinaryAssignOperator();
                case AdhocCallType.LOGICAL_OR_OLD:
                    return new OpLogicalOr() { CallType = type };
                case AdhocCallType.LOGICAL_OR:
                    return new OpLogicalOr();
                case AdhocCallType.LIST_ASSIGN:
                    return new OpListAssign();
                case AdhocCallType.LIST_ASSIGN_OLD:
                    return new OpListAssignOld();
                case AdhocCallType.ELEMENT_PUSH:
                    return new OpElementPush();
                case AdhocCallType.MAP_CONST:
                    return new OpMapConst();
                case AdhocCallType.MAP_CONST_OLD:
                    return new OpMapConstOld();
                case AdhocCallType.MAP_INSERT:
                    return new OpMapInsert();
                case AdhocCallType.UNARY_OPERATOR:
                    return new OpUnaryOperator();
                case AdhocCallType.LOGICAL_AND_OLD:
                    return new OpLogicalAnd() { CallType = type };
                case AdhocCallType.LOGICAL_AND:
                    return new OpLogicalAnd();
                case AdhocCallType.ARRAY_CONST:
                    return new OpArrayConst();
                case AdhocCallType.ARRAY_CONST_OLD:
                    return new OpArrayConst() { CallType = type };
                case AdhocCallType.ARRAY_PUSH:
                    return new OpArrayPush();
                case AdhocCallType.UNARY_ASSIGN_OPERATOR:
                    return new OpUnaryAssignOperator();
                case AdhocCallType.SYMBOL_CONST:
                    return new OpSymbolConst();
                case AdhocCallType.OBJECT_SELECTOR:
                    return new OpObjectSelector();
                case AdhocCallType.LONG_CONST:
                    return new OpLongConst();
                case AdhocCallType.UNDEF:
                    return new OpUndef();
                case AdhocCallType.TRY_CATCH:
                    return new OpTryCatch();
                case AdhocCallType.THROW:
                    return new OpThrow();
                case AdhocCallType.ASSIGN:
                    return new OpAssign();
                case AdhocCallType.ASSIGN_OLD:
                    return new OpAssign() { CallType = type };
                case AdhocCallType.U_INT_CONST:
                    return new OpUIntConst();
                case AdhocCallType.REQUIRE:
                    return new OpRequire();
                case AdhocCallType.U_LONG_CONST:
                    return new OpULongConst();
                case AdhocCallType.PRINT:
                    return new OpPrint();
                case AdhocCallType.MODULE_CONSTRUCTOR:
                    return new OpModuleCtor();
                case AdhocCallType.VA_CALL:
                    return new OpVaCall();
                case AdhocCallType.NOP:
                    return new OpNop();
                case AdhocCallType.DOUBLE_CONST:
                    return new OpDoubleConst();
                case AdhocCallType.UNK_69:
                    return new Op69();
                case AdhocCallType.UNK_70:
                    return new Op70();
                case AdhocCallType.UNK_71:
                    return new Op71();
                default:
                    throw new Exception($"Encountered unimplemented {type} instruction.");
            }
        }

        public override void Decompile(CodeBuilder builder)
        {
            builder.AppendLine(string.Empty);
        }
    }

    public enum AdhocCallType
    {
        ARRAY_CONST_OLD,
        ASSIGN_OLD,
        ATTRIBUTE_DEFINE,
        ATTRIBUTE_PUSH,
        BINARY_ASSIGN_OPERATOR,
        BINARY_OPERATOR,
        CALL,
        CLASS_DEFINE,
        EVAL,
        FLOAT_CONST,
        FUNCTION_DEFINE,
        IMPORT,
        INT_CONST,
        JUMP,
        JUMP_IF_TRUE,
        JUMP_IF_FALSE,
        LIST_ASSIGN_OLD,
        LOCAL_DEFINE,
        LOGICAL_AND_OLD,
        LOGICAL_OR_OLD,
        METHOD_DEFINE,
        MODULE_DEFINE,
        NIL_CONST,
        NOP,
        POP_OLD,
        PRINT,
        REQUIRE,
        SET_STATE_OLD,
        STATIC_DEFINE,
        STRING_CONST,
        STRING_PUSH,
        THROW,
        TRY_CATCH,
        UNARY_ASSIGN_OPERATOR,
        UNARY_OPERATOR,
        UNDEF,
        VARIABLE_PUSH,
        ATTRIBUTE_EVAL,
        VARIABLE_EVAL,
        SOURCE_FILE,
        FUNCTION_CONST,
        METHOD_CONST,
        MAP_CONST_OLD,
        LONG_CONST,
        ASSIGN,
        LIST_ASSIGN,
        CALL_OLD,
        OBJECT_SELECTOR,
        SYMBOL_CONST,
        LEAVE,
        ARRAY_CONST,
        ARRAY_PUSH,
        MAP_CONST,
        MAP_INSERT,
        POP,
        SET_STATE,
        VOID_CONST,
        ASSIGN_POP,
        U_INT_CONST,
        U_LONG_CONST,
        DOUBLE_CONST,
        ELEMENT_PUSH,
        ELEMENT_EVAL,
        LOGICAL_AND,
        LOGICAL_OR,
        BOOL_CONST,
        MODULE_CONSTRUCTOR,
        VA_CALL,
        CODE_EVAL,

        // GT Sport
        UNK_69,
        UNK_70,
        UNK_71,

    }
}
