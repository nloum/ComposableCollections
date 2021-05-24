using System.Collections.Generic;

namespace Typewriter
{
    public interface IConstructor
    {
        IReadOnlyList<IConstructorParameter> Parameters { get; }
        Visibility Visibility { get; }
    }
}