using System.IO;
using System.Text;

namespace IoFluently
{
    public ref struct StreamStringEnumerator
    {
        public ulong ByteOffset { get; private set; }
        public ulong CharOffset { get; private set; }
        private readonly Encoding _encoding;
        private readonly BufferEnumerator _bufferEnumerator;
        private Buffer _previousBuffer;
        private int _numBytesNeededFromPreviousBuffer;
        
        public StreamStringEnumerator(Stream stream, ulong byteOffsetOfStart, ulong charOffsetOfStart, int bufferSize, Encoding encoding)
        {
            _bufferEnumerator = new BufferEnumerator(stream, byteOffsetOfStart, bufferSize, encoding.GetMaxByteCount(1), 0);
            ByteOffset = byteOffsetOfStart;
            CharOffset = charOffsetOfStart;
            _encoding = encoding;
            Current = default;
            _numBytesNeededFromPreviousBuffer = 0;
            _previousBuffer = default;
        }

        // Needed to be compatible with the foreach operator
        public StreamStringEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (!_bufferEnumerator.MoveNext())
            {
                return false;
            }

            var buffer = _bufferEnumerator.Current;

            if (_numBytesNeededFromPreviousBuffer > 0)
            {
                _previousBuffer.Data.Slice(_previousBuffer.Data.Length - _previousBuffer.PaddingAtEnd - _numBytesNeededFromPreviousBuffer, _numBytesNeededFromPreviousBuffer)
                    .CopyTo(buffer.Data.Slice(buffer.PaddingAtStart - _numBytesNeededFromPreviousBuffer, _numBytesNeededFromPreviousBuffer));
            }
            
            string str = _encoding.GetString(buffer.Data.Slice(buffer.PaddingAtStart - _numBytesNeededFromPreviousBuffer, buffer.Data.Length - buffer.PaddingAtStart - buffer.PaddingAtEnd + _numBytesNeededFromPreviousBuffer));

            _previousBuffer = buffer;
            _numBytesNeededFromPreviousBuffer = 0;
            int byteCount;
            for (var i = 1; (byteCount = _encoding.GetByteCount(str)) != buffer.Data.Length && i < buffer.Data.Length; i++)
            {
                // If the for loop condition is true, that means that a character that takes up more than one byte
                // (such as a non-English character in UTF-8) was split by the buffer. To fix this, 

                str = _encoding.GetString(buffer.Data.Slice(0, buffer.Data.Length - i));
                _numBytesNeededFromPreviousBuffer++;
            }

            Current = new StringFromStream(str, ByteOffset, CharOffset, byteCount);
            ByteOffset += (ulong) byteCount;
            CharOffset += (ulong) str.Length;

            return true;
        }

        public StringFromStream Current { get; private set; }
    }
}