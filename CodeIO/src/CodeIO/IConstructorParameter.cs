namespace CodeIO
{
    public interface IConstructorParameter
    {
        string Name { get; }
        IType Type { get; }
        bool HasDefaultValue { get; }
        object DefaultValue { get; }
    }
}