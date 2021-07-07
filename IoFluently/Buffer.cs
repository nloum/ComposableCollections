using System;

namespace IoFluently
{
    public readonly ref struct Buffer
    {
        public Buffer(Span<byte> data, ulong byteOffsetOfStart, int paddingAtStart, int paddingAtEnd)
        {
            Data = data;
            ByteOffsetOfStart = byteOffsetOfStart;
            PaddingAtStart = paddingAtStart;
            PaddingAtEnd = paddingAtEnd;
            ByteOffsetOfEnd = byteOffsetOfStart + (ulong) data.Length;
        }

        public int PaddingAtStart { get; }
        public int PaddingAtEnd { get; }
        public ulong ByteOffsetOfStart { get; }
        public ulong ByteOffsetOfEnd { get; }
        public Span<byte> Data { get; }
    }
}