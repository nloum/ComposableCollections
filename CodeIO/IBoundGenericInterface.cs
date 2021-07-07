using System.Collections.Generic;

namespace CodeIO
{
    public interface IBoundGenericInterface : IInterface, IBoundComplexType
    {
        IUnboundGenericInterface Unbound { get; }
    }
}