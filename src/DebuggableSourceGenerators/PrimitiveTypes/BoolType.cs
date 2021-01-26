namespace DebuggableSourceGenerators.PrimitiveTypes
{
    public class BoolType : IPrimitiveType
    {
        public TypeIdentifier Identifier { get; } = new TypeIdentifier("System.Boolean", 0);
    }
}