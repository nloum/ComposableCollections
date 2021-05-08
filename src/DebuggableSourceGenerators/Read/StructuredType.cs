using System;
using System.Collections.Immutable;

namespace DebuggableSourceGenerators.Read
{
    public record StructuredType : Type
    {
        public ImmutableList<Lazy<TypeParameter>> TypeParameters { get; init; }
        public ImmutableList<Lazy<Type>> TypeArguments { get; init; }
        public ImmutableList<Property> Properties { get; init; }
        public ImmutableList<Method> Methods { get; init; }
        public ImmutableList<Indexer> Indexers { get; init; }
        public ImmutableList<Lazy<Interface>> Interfaces { get; init; }
    }
}