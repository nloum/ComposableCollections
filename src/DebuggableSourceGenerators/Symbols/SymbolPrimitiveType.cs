namespace DebuggableSourceGenerators
{
    public class SymbolPrimitiveType : IPrimitiveType
    {
        public SymbolPrimitiveType(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}