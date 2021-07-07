using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IoFluently
{
    public class ReverseStreamStringEnumerator : IEnumerator<StringFromStream>
    {
        public ulong ByteOffset { get; private set; }
        public ulong CharOffset { get; private set; }
        private readonly Encoding _encoding;
        private BufferEnumeratorState _bufferEnumeratorState;
        private byte[] _previousBuffer;
        private int _numBytesNeededFromPreviousBuffer;
        private readonly BufferEnumeratorState _initialState;

        public ReverseStreamStringEnumerator(Stream stream, ulong byteOffsetOfStart, ulong charOffsetOfStart, int bufferSize, Encoding encoding)
        {
            _bufferEnumeratorState = new BufferEnumeratorState
            {
                Stream = stream,
                ByteOffset = byteOffsetOfStart,
                BufferSize = bufferSize,
                PaddingAtStart = encoding.GetMaxByteCount(1),
                PaddingAtEnd = 0,
            };
            _initialState = _bufferEnumeratorState;
            ByteOffset = byteOffsetOfStart;
            CharOffset = charOffsetOfStart;
            _encoding = encoding;
            Current = default;
            _numBytesNeededFromPreviousBuffer = 0;
            _previousBuffer = default;
        }

        // Needed to be compatible with the foreach operator
        public ReverseStreamStringEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            var bufferEnumerator = new ReverseBufferEnumerator(_bufferEnumeratorState);
            if (!bufferEnumerator.MoveNext())
            {
                return false;
            }

            var buffer = bufferEnumerator.Current;

            if (_numBytesNeededFromPreviousBuffer > 0)
            {
                _previousBuffer.AsSpan()
                    .CopyTo(buffer.Data.Slice(buffer.Data.Length - buffer.PaddingAtEnd, _numBytesNeededFromPreviousBuffer));
            }
            
            string str = _encoding.GetString(buffer.Data.Slice(buffer.PaddingAtStart - _numBytesNeededFromPreviousBuffer, buffer.Data.Length - buffer.PaddingAtStart - buffer.PaddingAtEnd + _numBytesNeededFromPreviousBuffer));

            _previousBuffer = buffer.Data.Slice(buffer.PaddingAtStart - _numBytesNeededFromPreviousBuffer, _numBytesNeededFromPreviousBuffer).ToArray();
            _numBytesNeededFromPreviousBuffer = 0;
            int byteCount;
            for (var i = 1; (byteCount = _encoding.GetByteCount(str)) != buffer.Data.Length - buffer.PaddingAtStart - buffer.PaddingAtEnd && i < buffer.Data.Length; i++)
            {
                // If the for loop condition is true, that means that a character that takes up more than one byte
                // (such as a non-English character in UTF-8) was split by the buffer. To fix this, 

                str = _encoding.GetString(buffer.Data.Slice(i, buffer.Data.Length - i));
                _numBytesNeededFromPreviousBuffer++;
            }

            Current = new StringFromStream(str, ByteOffset, CharOffset, byteCount);
            ByteOffset += (ulong) byteCount;
            CharOffset += (ulong) str.Length;

            _bufferEnumeratorState = bufferEnumerator.State;
            
            return true;
        }

        public StringFromStream Current { get; private set; }

        public void Reset()
        {
            _bufferEnumeratorState = _initialState;
        }

        object IEnumerator.Current => Current;
        
        public void Dispose()
        {
            _bufferEnumeratorState?.Stream?.Dispose();
        }
    }
}