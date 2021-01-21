using System.Collections.Generic;

namespace DebuggableSourceGenerators
{
    public interface IStructuredType : IType
    {
        IReadOnlyList<TypeParameter> TypeParameters { get; }
        IReadOnlyList<Property> Properties { get; }
        IReadOnlyList<Method> Methods { get; }
        IReadOnlyList<Indexer> Indexers { get; }
    }
}