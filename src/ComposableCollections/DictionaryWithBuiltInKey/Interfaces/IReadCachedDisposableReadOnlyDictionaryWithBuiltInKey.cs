using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IReadCachedDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> :
        IReadCachedReadOnlyDictionaryWithBuiltInKey<TKey, TValue>,
        IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IReadCachedDisposableReadOnlyDictionary<TKey, TValue> AsReadCachedDisposableReadOnlyDictionary();
    }
}