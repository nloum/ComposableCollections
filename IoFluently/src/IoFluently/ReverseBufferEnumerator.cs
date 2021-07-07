using System;
using System.IO;

namespace IoFluently
{
    public ref struct ReverseBufferEnumerator
    {
        private Stream _stream;
        private readonly int _paddingAtStart;
        private readonly int _paddingAtEnd;
        private ulong _streamLength;
        public ulong ByteOffset { get; private set; }
        public int BufferSize { get; set; }
        private readonly IDisposable _disposable;

        public ReverseBufferEnumerator(BufferEnumeratorState state) : this(state.Stream, state.ByteOffset, state.BufferSize,
            state.PaddingAtStart, state.PaddingAtEnd)
        {
            
        }

        public ReverseBufferEnumerator(Stream stream, ulong byteOffsetOfStart, int bufferSize, int paddingAtStart, int paddingAtEnd)
        {
            _stream = stream;
            _paddingAtStart = paddingAtStart;
            _paddingAtEnd = paddingAtEnd;
            _streamLength = _stream.Length > 0 ? (ulong) _stream.Length : ulong.MaxValue;
            ByteOffset = byteOffsetOfStart;
            Current = default;
            BufferSize = bufferSize;
            _disposable = _stream;
            _stream.Seek((long) ByteOffset, SeekOrigin.Begin);
        }

        public BufferEnumeratorState State => new BufferEnumeratorState()
        {
            Stream = _stream,
            BufferSize = BufferSize,
            ByteOffset = ByteOffset,
            PaddingAtEnd = _paddingAtEnd,
            PaddingAtStart = _paddingAtStart
        };

        // Needed to be compatible with the foreach operator
        public ReverseBufferEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (_stream.Position == 0)
            {
                return false;
            }
            
            var unpaddedBufferLength = Math.Min(BufferSize, _stream.Position);
            var buffer = new byte[_paddingAtStart + unpaddedBufferLength + _paddingAtEnd];
            if (_stream.Position < unpaddedBufferLength)
            {
                _stream.Seek(0, SeekOrigin.Begin);
            }
            else
            {
                _stream.Seek(-unpaddedBufferLength, SeekOrigin.Current);
            }
            var length = _stream.Read(buffer, _paddingAtStart, (int) unpaddedBufferLength);
            if (length != unpaddedBufferLength)
            {
                throw new InvalidOperationException();
            }
            
            Current = new Buffer(buffer.AsSpan(0, _paddingAtStart + length + _paddingAtEnd), ByteOffset, _paddingAtStart, _paddingAtEnd);
            ByteOffset -= (ulong) length;
            return true;
        }

        public Buffer Current { get; private set; }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}