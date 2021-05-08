namespace DebuggableSourceGenerators.Read
{
    public record Type
    {
        public virtual bool IsGenericParameter => false;
        public TypeIdentifier Identifier { get; init; }
    }
}