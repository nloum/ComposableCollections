using System;
using System.Text;

namespace IoFluently
{
    public readonly ref struct LineSplitEntry
    {
        public LineSplitEntry(ReadOnlySpan<char> line, ReadOnlySpan<char> separator, ulong byteOffsetOfStart, ulong charOffsetOfStart, ulong lineNumber, Encoding encoding)
        {
            Line = line;
            Separator = separator;
            ByteOffsetOfStart = byteOffsetOfStart;
            CharOffsetOfStart = charOffsetOfStart;
            ByteOffsetOfEnd = byteOffsetOfStart + (ulong) encoding.GetByteCount(Line) + (ulong) encoding.GetByteCount(Separator);
            CharOffsetOfEnd = charOffsetOfStart + (ulong) Line.Length + (ulong) Separator.Length;
            LineNumber = lineNumber;
        }

        public ulong ByteOffsetOfStart { get; }
        public ulong CharOffsetOfStart { get; }
        public ulong ByteOffsetOfEnd { get; }
        public ulong CharOffsetOfEnd { get; }
        public ulong LineNumber { get; }
        public ReadOnlySpan<char> Line { get; }
        public ReadOnlySpan<char> Separator { get; }
    }
}