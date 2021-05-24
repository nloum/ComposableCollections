using System.Collections.Generic;

namespace Typewriter
{
    public interface IClass : IComplexType
    {
        IReadOnlyList<IConstructor> Constructors { get; }
        IReadOnlyList<IMethod> Methods { get; }
        IReadOnlyList<IIndexer> Indexers { get; }
        IReadOnlyList<IProperty> Properties { get; }
    }
}