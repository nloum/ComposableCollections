using System.Collections.Generic;

namespace CodeIO
{
    public interface IGenericParameter : IType
    {
        bool MustHaveDefaultConstructor { get; }
        bool MustBeReferenceType { get; }
        bool IsCovariant { get; }
        bool IsContravariant { get; }
        bool MustBeNonNullable { get; }
        IReadOnlyList<IType> MustExtend { get; }
    }
}