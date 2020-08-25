using System;

namespace ComposableCollections.Dictionary.WithBuiltInKey
{
    public interface
        IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>,
            IDisposable
    {
        IDisposableReadOnlyDictionary<TKey, TValue> AsDisposableReadOnlyDictionary();
    }
}