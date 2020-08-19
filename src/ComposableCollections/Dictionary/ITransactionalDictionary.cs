namespace ComposableCollections.Dictionary
{
    public interface ITransactionalDictionary<TKey, TValue>
    {
        IDisposableDictionary<TKey, TValue> BeginTransaction();
    }

    public interface IReadOnlyTransactionalDictionary<TKey, TValue>
    {
        IDisposableReadOnlyDictionary<TKey, TValue> BeginTransaction();
    }
}