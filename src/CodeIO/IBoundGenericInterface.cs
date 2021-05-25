namespace CodeIO
{
    public interface IBoundGenericInterface : IInterface
    {
        IUnboundGenericInterface Unbound { get; }
    }
}