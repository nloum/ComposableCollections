namespace CodeIO
{
    public interface IProperty
    {
        string Name { get; }
        IType Type { get; }
        Visibility Visibility { get; }
    }
}