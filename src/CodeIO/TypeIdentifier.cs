namespace CodeIO
{
    public record TypeIdentifier
    {
        public string Name { get; init; }
        public string Namespace { get; init; }
        public int Arity { get; init; }
    }
}