using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTAdhocParser.Instructions;
using GTAdhocParser.Decompiler;

namespace GTAdhocParser
{
    public class CodeBuilder
    {
        public int _depth;

        public HModule CurrentModule { get; set; }
        public HFunction CurrentFunction { get; set; }

        public List<HObject> Variables { get; set; } = new List<HObject>();

        private StringBuilder sb = new StringBuilder();

        public HObject GetTopVariable()
            => Variables[^1];

        public void AppendRaw(string code)
        {
            sb.Append(code);
        }

        public void Append(string code)
        {
            sb.Append(new string('\t', _depth)).Append(code);
        }

        public void AppendLine(string code)
        {
            sb.Append(new string('\t', _depth)).AppendLine(code);
        }

        public void AppendWithScope(string code)
        {
            AppendLine(code);
            AppendLine("{");
        }

        public void SetModule(OpModule module)
        {
            AppendWithScope($"module {module.Names[^1]}");
            CurrentModule = new HModule(module.Names[^1]);
            _depth++;
        }

        public void SetCurrentFunction(OpMethod method)
        {
            CurrentFunction = new HFunction() { Code = method.Code };
            Append($"function {method.MethodName}");

            if (method.Code.Arguments.Count != 0)
            {
                AppendRaw("(");
                for (int i = 0; i < method.Code.Arguments.Count; i++)
                {
                    var currentArg = method.Code.Arguments[i];
                    AppendRaw(currentArg.Item1);
                    if (i != method.Code.Arguments.Count - 1)
                        AppendRaw(", ");
                }

                AppendRaw(")");
            }

            AppendRaw("\n");
            AppendLine("{");
            _depth++;
        }

        public void AddCondition(bool ifTrue)
        {
            Append("if (");

            AppendRaw(Variables[Variables.Count - 3].ToString());
            AppendRaw(" ");

            var op = Variables[Variables.Count - 1];
            if (op.Type == HObjectType.Equals)
                AppendRaw("== ");

            AppendRaw(Variables[Variables.Count - 2].ToString());
            AppendRaw(")");

            AppendWithScope("");

        }
    }
}
