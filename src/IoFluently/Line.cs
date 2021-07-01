using System.Text;

namespace IoFluently
{
    public class Line
    {
        public Line(string line, string separator, ulong byteOffsetOfStart, ulong charOffsetOfStart, ulong lineNumber, Encoding encoding)
        {
            Value = line;
            Separator = separator;
            ByteOffsetOfStart = byteOffsetOfStart;
            CharOffsetOfStart = charOffsetOfStart;
            ByteOffsetOfEnd = byteOffsetOfStart + (ulong) encoding.GetByteCount(Value) + (ulong) encoding.GetByteCount(Separator);
            CharOffsetOfEnd = charOffsetOfStart + (ulong) Value.Length + (ulong) Separator.Length;
            LineNumber = lineNumber;
            Encoding = encoding;
        }

        public ulong ByteOffsetOfStart { get; }
        public ulong CharOffsetOfStart { get; }
        public ulong ByteOffsetOfEnd { get; }
        public ulong CharOffsetOfEnd { get; }
        public ulong LineNumber { get; }
        public Encoding Encoding { get; }
        public string Value { get; }
        public string Separator { get; }
    }
}