namespace Typewriter
{
    public interface IConstructorParameter
    {
        string Name { get; }
        IType Type { get; }
    }
}