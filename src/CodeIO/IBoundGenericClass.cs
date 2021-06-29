using System.Collections.Generic;

namespace CodeIO
{
    public interface IBoundGenericClass : IClass
    {
        IReadOnlyList<IType> Arguments { get; }
        IUnboundGenericClass Unbound { get; }
    }
}