using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> :
        IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>,
        IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IDisposableQueryableReadOnlyDictionary<TKey, TValue> AsDisposableQueryableReadOnlyDictionary();
    }
}