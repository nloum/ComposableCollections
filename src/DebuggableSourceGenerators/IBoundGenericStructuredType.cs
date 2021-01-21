using System.Collections.Generic;

namespace DebuggableSourceGenerators
{
    public interface IBoundGenericStructuredType : IType
    {
        public IStructuredType UnboundForm { get; }
        public IReadOnlyList<IType> TypeParameterValues { get; }
    }
}