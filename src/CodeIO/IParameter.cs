namespace CodeIO
{
    public interface IParameter
    {
        string Name { get; }
        IType Type { get; }
        bool IsOut { get; }
    }
}