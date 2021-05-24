using System;
using System.Collections.Immutable;

namespace DebuggableSourceGenerators.Read
{
    public record UnboundStructuredType : TypeBase
    {
        public ImmutableList<Lazy<TypeParameter>> TypeParameters { get; init; }
    }
}