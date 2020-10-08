using System;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Adapters
{
    public class DisposableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue> :
        ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>, IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IDisposableReadOnlyDictionary<TKey, TValue> _source;

        public DisposableReadOnlyDictionaryWithBuiltInKeyAdapter(IDisposableReadOnlyDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
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
    }
}