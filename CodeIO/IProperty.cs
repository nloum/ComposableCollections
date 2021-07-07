namespace CodeIO
{
    public interface IProperty
    {
        string Name { get; }
        IType Type { get; }
        Visibility? GetterVisibility { get; }
        Visibility? SetterVisibility { get; }
        bool IsStatic { get; }
    }
}