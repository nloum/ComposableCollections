using System.Collections.Generic;

namespace CodeIO
{
    public interface IIndexer
    {
        IType ReturnType { get; }
        IReadOnlyList<IParameter> Parameters { get; }
        Visibility? GetterVisibility { get; }
        Visibility? SetterVisibility { get; }
        bool IsStatic { get; }
    }
}