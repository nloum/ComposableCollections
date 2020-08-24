using System;

namespace ComposableCollections.Dictionary.WithBuiltInKey
{
    public class DisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue> : DictionaryWithBuiltInKeyAdapter<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>
    {
        private IDisposableDictionary<TKey, TValue> _source;

        public DisposableDictionaryWithBuiltInKeyAdapter(IDisposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey) : base(source, getKey)
        {
            _source = source;
        }

        protected DisposableDictionaryWithBuiltInKeyAdapter()
        {
        }

        protected void Initialize(IDisposableDictionary<TKey, TValue> source, Func<TValue, TKey> getKey)
        {
            _source = source;
            base.Initialize(source, getKey);
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