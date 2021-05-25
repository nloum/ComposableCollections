namespace CodeIO
{
    public interface IType
    {
        TypeIdentifier Identifier { get; }
        Visibility Visibility { get; }
    }
}