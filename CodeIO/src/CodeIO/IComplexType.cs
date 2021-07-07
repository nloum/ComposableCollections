using System.Collections.Generic;

namespace CodeIO
{
    public interface IComplexType : IReferenceType
    {
        IReadOnlyList<IType> Interfaces { get; }
    }
}