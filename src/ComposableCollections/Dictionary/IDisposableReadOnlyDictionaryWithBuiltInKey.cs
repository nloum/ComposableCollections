using System;

namespace ComposableCollections.Dictionary
{
    public interface IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, out TValue> : IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>,
        IDisposable
    {
    }
}