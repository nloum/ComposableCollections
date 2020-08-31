namespace ComposableCollections.Set
{
    public interface IDisposableQueryableSet<TValue> : IQueryableSet<TValue>, IDisposableSet<TValue>,
        IDisposableQueryableReadOnlySet<TValue>
    {
        
    }
}