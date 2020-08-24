using System;
using System.Linq;

namespace ComposableCollections.Dictionary
{
    public interface IDisposableQueryableReadOnlyDictionary<TKey, out TValue> : IDisposableReadOnlyDictionary<TKey, TValue>, IQueryableReadOnlyDictionary<TKey, TValue> { }
    public interface IQueryableReadOnlyDictionary<TKey, out TValue> : IComposableReadOnlyDictionary<TKey, TValue>
    {
        new IQueryable<TValue> Values { get; }
    }
    public interface ICachedDisposableQueryableDictionary<TKey, TValue> : ICachedDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>, IQueryableDictionary<TKey, TValue> { }
    public interface IDisposableQueryableDictionary<TKey, TValue> : IDisposableDictionary<TKey, TValue>, IQueryableDictionary<TKey, TValue> { }
    public interface ICachedQueryableDictionary<TKey, TValue> : ICachedDictionary<TKey, TValue>, IQueryableDictionary<TKey, TValue> { }
    public interface ICachedDisposableDictionary<TKey, TValue> : ICachedDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue> { }
    public interface IDisposableReadOnlyDictionary<TKey, out TValue> : IComposableReadOnlyDictionary<TKey, TValue>, IDisposable { }
}