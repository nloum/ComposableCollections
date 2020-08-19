namespace ComposableCollections.Dictionary
{
    public interface IReadOnlyTransactionalDictionary<TKey, TValue>
    {
        IDisposableReadOnlyDictionary<TKey, TValue> BeginRead();
    }
}