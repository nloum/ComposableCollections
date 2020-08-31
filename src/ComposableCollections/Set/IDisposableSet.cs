namespace ComposableCollections.Set
{
    public interface IDisposableSet<TValue> : IComposableSet<TValue>, IDisposableReadOnlySet<TValue> {
    }
}