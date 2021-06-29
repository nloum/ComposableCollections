using System.Collections.Generic;

namespace CodeIO
{
    public interface IBoundGenericInterface : IInterface
    {
        IReadOnlyList<IType> Arguments { get; }
        IUnboundGenericInterface Unbound { get; }
    }
}