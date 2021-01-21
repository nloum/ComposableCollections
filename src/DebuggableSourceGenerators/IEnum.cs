using System.Collections.Generic;

namespace DebuggableSourceGenerators
{
    public interface IEnum : IType
    {
        IReadOnlyList<string> Values { get; }
    }
}