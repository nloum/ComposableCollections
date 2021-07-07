using System.Collections.Generic;

namespace CodeIO
{
    public interface IBoundComplexType : IComplexType
    {
        IReadOnlyList<IType> Arguments { get; }
    }
}