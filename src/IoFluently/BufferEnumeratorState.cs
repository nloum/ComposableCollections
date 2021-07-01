using System.IO;

namespace IoFluently
{
    public class BufferEnumeratorState
    {
        public Stream Stream { get; init; }
        public ulong ByteOffset { get; init; }
        public int BufferSize { get; init; }
        public int PaddingAtStart { get; init; }
        public int PaddingAtEnd { get; init; }
    }
}