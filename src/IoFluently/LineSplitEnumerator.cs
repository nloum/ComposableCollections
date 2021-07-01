using System;
using System.Text;

namespace IoFluently
{
    public ref struct LineSplitEnumerator
    {
        private ReadOnlySpan<char> _str;
        public ulong ByteOffset { get; private set; }
        public ulong CharOffset { get; private set; }
        public ulong LineNumber { get; private set; }
        private readonly Encoding _encoding;

        public LineSplitEnumerator(string str, ulong byteOffsetOfStart, ulong charOffsetOfStart, ulong lineNumber, Encoding encoding)
        {
            _str = str;
            ByteOffset = byteOffsetOfStart;
            CharOffset = charOffsetOfStart;
            LineNumber = lineNumber;
            _encoding = encoding;
            Current = default;
        }

        // Needed to be compatible with the foreach operator
        public LineSplitEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            var span = _str;
            if (span.Length == 0) // Reach the end of the string
                return false;

            var index = span.IndexOfAny('\r', '\n');
            if (index == -1) // The string is composed of only one line
            {
                _str = ReadOnlySpan<char>.Empty; // The remaining string is an empty string
                Current = new LineSplitEntry(span, ReadOnlySpan<char>.Empty, ByteOffset, CharOffset, LineNumber, _encoding)
                    .ToLine(_encoding);
                LineNumber++;
                ByteOffset = Current.ByteOffsetOfEnd + 1;
                CharOffset = Current.CharOffsetOfEnd + 1;
                return true;
            }

            if (index < span.Length - 1 && span[index] == '\r')
            {
                // Try to consume the '\n' associated to the '\r'
                var next = span[index + 1];
                if (next == '\n')
                {
                    Current = new LineSplitEntry(span.Slice(0, index), span.Slice(index, 2), ByteOffset, CharOffset, LineNumber, _encoding)
                        .ToLine(_encoding);
                    LineNumber++;
                    ByteOffset = Current.ByteOffsetOfEnd + 1;
                    CharOffset = Current.CharOffsetOfEnd + 1;
                    _str = span.Slice(index + 2);
                    return true;
                }
            }

            Current = new LineSplitEntry(span.Slice(0, index), span.Slice(index, 1), ByteOffset, CharOffset, LineNumber, _encoding)
                .ToLine(_encoding);
            LineNumber++;
            ByteOffset = Current.ByteOffsetOfEnd + 1;
            CharOffset = Current.CharOffsetOfEnd + 1;
            _str = span.Slice(index + 1);
            return true;
        }

        public Line Current { get; private set; }
    }
}