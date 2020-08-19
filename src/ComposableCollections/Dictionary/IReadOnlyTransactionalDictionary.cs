namespace ComposableCollections.Dictionary
{
    public interface IReadOnlyTransactionalDictionary<TKey, out TValue>
    {
        IDisposableReadOnlyDictionary<TKey, TValue> BeginRead();
    }
}