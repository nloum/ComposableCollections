using System;

namespace DebuggableSourceGenerators.Read
{
    public record TypeBase
    {
        public virtual bool IsGenericParameter => false;
        public TypeIdentifier Identifier { get; init; }
        public Lazy<CodeIndex> CodeIndex { get; init; }
    }
}