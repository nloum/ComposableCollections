using System;

namespace DebuggableSourceGenerators
{
    public record TypeIdentifier
    {
        public string Namespace { get; init; }
        public string Name { get; init; }
        public int Arity { get; init; }
    }
}