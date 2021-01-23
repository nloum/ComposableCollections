namespace DebuggableSourceGenerators
{
    public class SymbolPrimitiveType : IPrimitiveType
    {
        public SymbolPrimitiveType(TypeIdentifier identifier)
        {
            Identifier = identifier;
        }

        public TypeIdentifier Identifier { get; }
    }
}