using System;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;

namespace ComposableCollections.DictionaryWithBuiltInKey.Adapters
{
    public class DisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue> : DictionaryWithBuiltInKeyAdapter<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly IDisposableDictionary<TKey, TValue> _source;

        public DisposableDictionaryWithBuiltInKeyAdapter(IDisposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
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

        public IDisposableDictionary<TKey, TValue> AsDisposableDictionary()
        {
            return _source;
        }
    }
}