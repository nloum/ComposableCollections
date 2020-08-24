using System;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public interface IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> :
        IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>,
        IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IDisposableQueryableReadOnlyDictionary<TKey, TValue> AsDisposableQueryableReadOnlyDictionary();
    }
    public interface IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        new IQueryable<TValue> Values { get; }
        IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary();
    }

    public interface ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>,
        IQueryableDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableReadOnlyDictionary<TKey, TValue>,
        IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        ICachedDisposableQueryableDictionary<TKey, TValue> AsCachedDisposableQueryableDictionary();
    }

    public interface IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        IDisposableDictionaryWithBuiltInKey<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>,
        IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IDisposableQueryableDictionary<TKey, TValue> AsDisposableQueryableDictionary();
    }

    public interface IQueryableDictionaryWithBuiltInKey<TKey, TValue> : IDictionaryWithBuiltInKey<TKey, TValue>,
        IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        IQueryableDictionary<TKey, TValue> AsQueryableDictionary();
    }

    public interface ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedDictionaryWithBuiltInKey<TKey, TValue>, IQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        ICachedQueryableDictionary<TKey, TValue> AsCachedQueryableDictionary();
    }

    public interface ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> :
        ICachedDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        ICachedDisposableDictionary<TKey, TValue> AsCachedDisposableDictionary();
    }

    public interface
        IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>,
            IDisposable
    {
        IDisposableReadOnlyDictionary<TKey, TValue> AsDisposableReadOnlyDictionary();
    }
}