namespace ComposableCollections.Set
{
    public interface ICachedDisposableQueryableSet<TValue> : IDisposableQueryableSet<TValue>, ICachedQueryableSet<TValue>, ICachedDisposableSet<TValue>
    {
        
    }
}