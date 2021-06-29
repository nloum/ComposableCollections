using System.Collections.Generic;

namespace CodeIO
{
    public interface IUnboundGenericClass : IDefinedClass
    {
        IReadOnlyList<IGenericParameter> Parameters { get; }
    }
}