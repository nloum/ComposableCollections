namespace Typewriter
{
    public record Primitive : IValueType
    {
        public TypeIdentifier Identifier { get; init; }
        public Visibility Visibility => Visibility.Public;
        public PrimitiveType Type { get; init; }
    }
}