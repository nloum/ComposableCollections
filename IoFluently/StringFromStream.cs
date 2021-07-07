namespace IoFluently
{
    public class StringFromStream
    {
        public StringFromStream(string value, ulong byteOffsetOfStart, ulong charOffsetOfStart, int byteCount)
        {
            Value = value;
            ByteOffsetOfStart = byteOffsetOfStart;
            CharOffsetOfStart = charOffsetOfStart;
            ByteOffsetOfEnd = byteOffsetOfStart + (ulong) byteCount;
            CharOffsetOfEnd = charOffsetOfStart + (ulong) Value.Length;
        }

        public ulong ByteOffsetOfStart { get; }
        public ulong CharOffsetOfStart { get; }
        public ulong ByteOffsetOfEnd { get; }
        public ulong CharOffsetOfEnd { get; }
        public string Value { get; }
    }
}