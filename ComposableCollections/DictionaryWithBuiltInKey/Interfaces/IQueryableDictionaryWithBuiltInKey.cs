using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Interfaces
{
    public interface IQueryableDictionaryWithBuiltInKey<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue>,
        IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IQueryableDictionary<TKey, TValue> AsQueryableDictionary();
    }
}