using System.Collections.Generic;

namespace CodeIO
{
    public class Constructor : IConstructor
    {
        public IReadOnlyList<IConstructorParameter> Parameters { get; init; }
        public Visibility Visibility { get; init; }
    }
}