using System.Collections.Generic;
using System.Collections.Immutable;

namespace DebuggableSourceGenerators
{
    public record BoundGenericStructuredType : Type
    {
        public StructuredType UnboundForm { get; init; }
        public ImmutableList<Type> TypeParameterValues { get; init; }
    }
}