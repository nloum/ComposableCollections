namespace SimpleMonads.HotChocolate
{
    public class EitherObjectTypeParameters
    {
        public string InterfaceTypeName { get; init; }
        public string UnionTypeName { get; init; }
        public bool UseTypeNamesForProperties { get; init; }
        public bool IncludeInterface { get; init; }
    }
}