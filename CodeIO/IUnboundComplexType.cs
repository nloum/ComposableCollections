using System.Collections.Generic;

namespace CodeIO
{
    public interface IUnboundComplexType : IComplexType
    {
        IReadOnlyList<IGenericParameter> Parameters { get; }
    }
}