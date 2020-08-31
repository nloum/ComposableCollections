namespace ComposableCollections.Set
{
    public interface IDisposableQueryableReadOnlySet<TValue> : IDisposableReadOnlySet<TValue>, IQueryableReadOnlySet<TValue>
    {
    }
}