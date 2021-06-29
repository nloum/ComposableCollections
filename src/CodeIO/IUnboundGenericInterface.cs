using System.Collections.Generic;

namespace CodeIO
{
    public interface IUnboundGenericInterface : IDefinedInterface
    {
        IReadOnlyList<IGenericParameter> Parameters { get; }
    }
}