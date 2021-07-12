using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GTAdhocTools.UI
{
    public class MTextIO
    {
        public string FileName { get; set; }

        public StreamReader Stream { get; set; }

        public bool ReadingArray { get; set; }

        public const char ARRAY_START = '[';
        public const char ARRAY_END = ']';
        public const char SCOPE_START = '{';
        public const char SCOPE_END = '}';
        public const char STRING_SCOPE = '"';

        private StringBuilder _sb = new StringBuilder();

        public MTextIO(string fileName)
        {
            FileName = fileName;
        }

        public mNode Read()
        {
            using var file = File.Open(FileName, FileMode.Open);
            Stream = new StreamReader(file);

            var rootNode = new mNode();
            rootNode.IsRoot = true;
            rootNode.Read(this);
            
            return rootNode;
        }

        public string GetToken()
        {
            _sb.Clear();

            while (!Stream.EndOfStream)
            {
                var nextChar = Stream.Peek();
                if (nextChar == -1)
                    return null;

                var c = (char)nextChar;
                if (_sb.Length != 0) // Check for potential termination
                {
                    if (IsDiscardableChar(c))
                    {
                        return _sb.ToString();
                    }
                    else if (c == SCOPE_START || c == ARRAY_START)
                    {
                        return _sb.ToString();
                    }
                    else if (c == ARRAY_END)
                    {
                        if (_sb.Length == 1) // Only start
                            throw new UISyntaxError($"Unexpected array length definition end, no number specified.");

                        _sb.Append((char)Stream.Read());
                        return _sb.ToString();
                    }
                }

                if (IsDiscardableChar(c))
                {
                    Advance();
                    continue;
                }

                if (ReadingArray)
                {
                    if (!char.IsDigit(c))
                        throw new UISyntaxError($"Expected number in array length definition, got {c}.");

                    _sb.Append((char)Stream.Read());
                }
                else
                {
                    if (char.IsLetterOrDigit(c) || c == '_') // Valid identifier?
                    {
                        _sb.Append((char)Stream.Read());
                    }
                    else if (c == ARRAY_START)
                    {
                        ReadingArray = true;
                        _sb.Append((char)Stream.Read());
                    }
                    else if (c == SCOPE_START || c == SCOPE_END)
                    {
                        Advance();
                        return c.ToString();
                    }
                }
            }

            return _sb.ToString();
        }

        public string GetString()
        {
            _sb.Clear();

            bool started = false;
            while (true)
            {
                var nextChar = Stream.Peek();
                if (nextChar == -1)
                {
                    throw new UISyntaxError($"Unexpected EOF.");
                }

                var c = (char)nextChar;

                if (!started)
                {
                    if (IsDiscardableChar(c))
                    {
                        Advance();
                    }
                    else if (c == STRING_SCOPE)
                    {
                        Advance();
                        started = true;
                    }
                    else
                    {
                        throw new UISyntaxError($"Unexpected string token '{c}'.");
                    }
                }
                else
                {
                    if (c == STRING_SCOPE)
                    {
                        Advance();
                        return _sb.ToString();
                    }
                    else
                    {
                        _sb.Append((char)Stream.Read());
                    }
                }
            }
        }

        private bool IsDiscardableChar(char c)
            => c == ' ' || c == '\n' || c == '\t' || c == '\r';

        private void Advance()
            => Stream.Read();
    }
}
