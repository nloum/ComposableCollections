using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IoFluently
{
    public class ReverseLineSplitEnumerator : IEnumerator<Line>
    {
        private string _str;
        public ulong ByteOffset { get; private set; }
        public ulong CharOffset { get; private set; }
        public ulong LineNumber { get; private set; }
        private readonly Encoding _encoding;
        private int _separatorLength = 0;

        public ReverseLineSplitEnumerator(string str, ulong byteOffsetOfStart, ulong charOffsetOfStart, ulong lineNumber, Encoding encoding)
        {
            _str = str;
            ByteOffset = byteOffsetOfStart;
            CharOffset = charOffsetOfStart;
            LineNumber = lineNumber;
            _encoding = encoding;
            Current = default;
        }

        // Needed to be compatible with the foreach operator
        public ReverseLineSplitEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            var span = _str.AsSpan();
            if (span.Length == 0) // Reach the end of the string
                return false;

            var index = span.Slice(0, span.Length - _separatorLength).LastIndexOfAny('\r', '\n');
            if (index == -1) // The string is composed of only one line
            {
                _str = string.Empty; // The remaining string is an empty string
                Current = new LineSplitEntry(span.Slice(0, span.Length - _separatorLength), span.Slice(span.Length - _separatorLength), ByteOffset, CharOffset, LineNumber, _encoding)
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
                    Current = new LineSplitEntry(span.Slice(index + 2, span.Length - (index + 2) - _separatorLength), span.Slice(span.Length - _separatorLength), ByteOffset, CharOffset, LineNumber, _encoding)
                        .ToLine(_encoding);
                    LineNumber++;
                    ByteOffset = Current.ByteOffsetOfEnd + 1;
                    CharOffset = Current.CharOffsetOfEnd + 1;
                    _str = new string(span.Slice(index + 2));
                    _separatorLength = 2;
                    return true;
                }
            }

            Current = new LineSplitEntry(span.Slice(index + 1, span.Length - index - 1 - _separatorLength), span.Slice(span.Length - _separatorLength), ByteOffset, CharOffset, LineNumber, _encoding)
                .ToLine(_encoding);
            _separatorLength = 1;
            LineNumber++;
            ByteOffset = Current.ByteOffsetOfEnd + 1;
            CharOffset = Current.CharOffsetOfEnd + 1;
            // TODO - does this string constructor cause an extra allocation? if so, it would be more performant to make _str be a ReadOnlySpan<char>. But, that comes with its own potential problems: this class will have to be a ref struct.
            _str = new string(span.Slice(0, index + 1));
            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        object IEnumerator.Current => Current;
        
        public void Dispose()
        {
            
        }

        public Line Current { get; private set; }
    }
}