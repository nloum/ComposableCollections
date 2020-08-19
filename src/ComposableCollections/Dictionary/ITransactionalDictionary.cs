namespace ComposableCollections.Dictionary
{
    public interface ITransactionalDictionary<TKey, TValue> : IReadOnlyTransactionalDictionary<TKey, TValue>
    {
        IDisposableDictionary<TKey, TValue> BeginWrite();
    }
}