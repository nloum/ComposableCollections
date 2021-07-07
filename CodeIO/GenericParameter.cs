using System.Collections.Generic;

namespace CodeIO
{
    public class GenericParameter : IGenericParameter
    {
        public TypeIdentifier Identifier { get; init; }
        public Visibility Visibility => Visibility.Public;
        public bool MustHaveDefaultConstructor { get; init; }
        public bool MustBeReferenceType { get; init; }
        public bool IsCovariant { get; init; }
        public bool IsContravariant { get; init; }
        public bool MustBeNonNullable { get; init; }
        public IReadOnlyList<IType> MustExtend { get; init; }
    }
}