using System.Collections.Immutable;

namespace DebuggableSourceGenerators.Read
{
    public record BoundGenericStructuredType : Type
    {
        public StructuredType UnboundForm { get; init; }
        public ImmutableList<Type> TypeParameterValues { get; init; }
    }
}