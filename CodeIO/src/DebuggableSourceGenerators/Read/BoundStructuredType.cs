using System.Collections.Immutable;

namespace DebuggableSourceGenerators.Read
{
    public interface IBoundStructuredType
    {
        StructuredType UnboundForm { get; }
        ImmutableList<TypeParameterValue> TypeParameterValues { get; }
    }
    
    public record BoundClass : TypeBase, IBoundStructuredType
    {
        public StructuredType UnboundForm { get; init; }
        public ImmutableList<TypeParameterValue> TypeParameterValues { get; init; }
    }
    
    public record BoundInterface : TypeBase, IBoundStructuredType
    {
        public StructuredType UnboundForm { get; init; }
        public ImmutableList<TypeParameterValue> TypeParameterValues { get; init; }
    }
}