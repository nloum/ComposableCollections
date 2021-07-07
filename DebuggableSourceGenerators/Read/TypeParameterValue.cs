using System;

namespace DebuggableSourceGenerators.Read
{
    public record TypeParameterValue : TypeParameter
    {
        public Lazy<TypeBase> Value { get; init; }
    }
}