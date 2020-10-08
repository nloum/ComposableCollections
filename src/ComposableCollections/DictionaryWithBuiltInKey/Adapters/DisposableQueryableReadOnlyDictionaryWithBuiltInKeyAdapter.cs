using System;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Adapters
{
    public class DisposableQueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
        ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>,
        IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IDisposableQueryableReadOnlyDictionary<TKey, TValue> _source;
        
        public DisposableQueryableReadOnlyDictionaryWithBuiltInKeyAdapter(IDisposableQueryableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        public void Dispose()
        {
            _source.Dispose();
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> AsDisposableReadOnlyDictionary()
        {
            return _source;
        }

        public IQueryableReadOnlyDictionary<TKey, TValue> AsQueryableReadOnlyDictionary()
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