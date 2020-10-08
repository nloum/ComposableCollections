using System;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Adapters
{
    public class DisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
        DictionaryWithBuiltInKeyAdapter<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IDisposableQueryableDictionary<TKey, TValue> _source;

        public DisposableQueryableDictionaryWithBuiltInKeyAdapter(IDisposableQueryableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        public void Dispose()
        {
            _source.Dispose();
        }

        public IDisposableDictionary<TKey, TValue> AsDisposableDictionary()
        {
            return _source;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> AsDisposableReadOnlyDictionary()
        {
            return _source;
        }

        public IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary()
        {
            return _source;
        }

        public IQueryableDictionary<TKey, TValue> AsQueryableDictionary()
        {
            return _source;
        }

        public IDisposableQueryableDictionary<TKey, TValue> AsDisposableQueryableDictionary()
        {
            return _source;
        }

        public IDisposableQueryableReadOnlyDictionary<TKey, TValue> AsDisposableQueryableReadOnlyDictionary()
        {
            return _source;
        }

        public new IQueryable<TValue> Values => _source.Values;
    }
}