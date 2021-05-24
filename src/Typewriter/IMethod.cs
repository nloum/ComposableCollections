using System.Collections.Generic;

namespace Typewriter
{
    public interface IMethod
    {
        string Name { get; }
        IType ReturnType { get; }
        IReadOnlyList<IParameter> Parameters { get; }
        Visibility Visibility { get; }
    }
}