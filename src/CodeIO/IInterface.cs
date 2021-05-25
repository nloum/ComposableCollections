using System.Collections.Generic;

namespace CodeIO
{
    public interface IInterface : IComplexType
    {
        IReadOnlyList<IMethod> Methods { get; }
        IReadOnlyList<IIndexer> Indexers { get; }
        IReadOnlyList<IProperty> Properties { get; }
    }
}