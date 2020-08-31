namespace ComposableCollections.Set
{
    public interface IDisposableSet<TValue> : ISet<TValue>, IDisposableReadOnlySet<TValue> {
    }
}