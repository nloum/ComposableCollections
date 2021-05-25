using System.Collections.Generic;

namespace CodeIO
{
    public interface IGenericParameter : IType
    {
        bool MustHaveEmptyConstructor { get; }
        bool MustBeReferenceType { get; }
        IReadOnlyList<IType> MustExtend { get; }
    }
}