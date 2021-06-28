using System.Collections.Generic;

namespace CodeIO
{
    public interface IMethod
    {
        string Name { get; }
        IType ReturnType { get; }
        IReadOnlyList<IParameter> Parameters { get; }
        Visibility Visibility { get; }
        bool IsStatic { get; }
    }
}