using System.Collections.Generic;

namespace Typewriter
{
    public interface IIndexer
    {
        IType ReturnType { get; }
        IReadOnlyList<IParameter> Parameters { get; }
        Visibility Visibility { get; }
    }
}