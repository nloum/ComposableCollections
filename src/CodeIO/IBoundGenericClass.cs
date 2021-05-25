namespace CodeIO
{
    public interface IBoundGenericClass : IClass
    {
        IUnboundGenericClass Unbound { get; }
    }
}