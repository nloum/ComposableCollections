namespace CodeIO
{
    public interface IField
    {
        string Name { get; }
        IType Type { get; }
        Visibility Visibility { get; }
        bool IsStatic { get; }
    }
}