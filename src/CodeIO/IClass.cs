using System.Collections.Generic;

namespace CodeIO
{
    public interface IClass : IComplexType
    {
        IClass? BaseClass { get; }
        IReadOnlyList<IConstructor> Constructors { get; }
        IReadOnlyList<IMethod> Methods { get; }
        IReadOnlyList<IIndexer> Indexers { get; }
        IReadOnlyList<IProperty> Properties { get; }
    }
}