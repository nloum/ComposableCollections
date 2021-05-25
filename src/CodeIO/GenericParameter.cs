using System.Collections.Generic;

namespace CodeIO
{
    public class GenericParameter : IGenericParameter
    {
        public TypeIdentifier Identifier { get; init; }
        public Visibility Visibility => Visibility.Public;
        public bool MustHaveEmptyConstructor { get; init; }
        public bool MustBeReferenceType { get; init; }
        public IReadOnlyList<IType> MustExtend { get; init; }
    }
}