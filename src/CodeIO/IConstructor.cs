using System.Collections.Generic;

namespace CodeIO
{
    public interface IConstructor
    {
        IReadOnlyList<IConstructorParameter> Parameters { get; }
        Visibility Visibility { get; }
    }
}