using System;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.Sources
{
    public interface IMultiTypeDictionary<TValue> : IReadOnlyMultiTypeDictionary<TValue>
    {
        bool TryAdd<T>(Func<T> value, out T result, out T previousValue) where T : TValue;
        bool TryUpdate<T>(Func<TValue, TValue> value, out TValue previousValue, out TValue newValue) where T : TValue;
        DictionaryItemAddOrUpdateResult AddOrUpdate<T>(Func<T> valueIfAdding,
            Func<T, T> valueIfUpdating, out T previousValue, out T newValue) where T : TValue;
        bool TryRemove<T>(out T removedItem) where T : TValue;
    }
}