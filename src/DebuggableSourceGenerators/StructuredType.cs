using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace DebuggableSourceGenerators
{
    public record StructuredType : Type
    {
        public ImmutableList<Lazy<TypeParameter>> TypeParameters { get; init; }
        public ImmutableList<Property> Properties { get; init; }
        public ImmutableList<Method> Methods { get; init; }
        public ImmutableList<Indexer> Indexers { get; init; }
    }
}