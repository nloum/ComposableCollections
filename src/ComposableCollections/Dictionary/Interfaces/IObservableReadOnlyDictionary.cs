using LiveLinq.Dictionary;

namespace ComposableCollections.Dictionary.Interfaces
{
    public interface IObservableReadOnlyDictionary<TKey, out TValue> : IDisposableReadOnlyDictionary<TKey, TValue>
    {
        IDictionaryChangesStrict<TKey, TValue> ToLiveLinq();
    }
}