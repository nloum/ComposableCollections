using System.Collections.Generic;

namespace CodeIO
{
    public interface IBoundGenericClass : IClass, IBoundComplexType
    {
        IUnboundGenericClass Unbound { get; }
    }
}