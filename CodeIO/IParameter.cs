namespace CodeIO
{
    public interface IParameter
    {
        string Name { get; }
        IType Type { get; }
        bool IsOut { get; }
        bool HasDefaultValue { get; }
        object DefaultValue { get; }
    }
}