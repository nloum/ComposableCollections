using ComposableCollections.Dictionary.Interfaces;
using LiveLinq.Dictionary;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IObservableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IDictionaryChangesStrict<TKey, TValue> ToLiveLinq();
        IObservableReadOnlyDictionary<TKey, TValue> AsObservableReadOnlyDictionary();
    }
}