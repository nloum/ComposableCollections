using System;
using ComposableCollections.Common;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public class DictionaryWrite<TKey, TValue>
    {
        public static DictionaryWrite<TKey, TValue> CreateAdd(TKey key, Func<TValue> value)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.Add, key, value, null);
        }

        public static DictionaryWrite<TKey, TValue> CreateTryAdd(TKey key, Func<TValue> value)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.TryAdd, key, value, null);
        }

        public static DictionaryWrite<TKey, TValue> CreateUpdate(TKey key, Func<TValue, TValue> value)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.Update, key, null, value);
        }

        public static DictionaryWrite<TKey, TValue> CreateTryUpdate(TKey key, Func<TValue, TValue> value)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.TryUpdate, key, null, value);
        }

        public static DictionaryWrite<TKey, TValue> CreateAddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.AddOrUpdate, key, valueIfAdding, valueIfUpdating);
        }

        public static DictionaryWrite<TKey, TValue> CreateRemove(TKey key)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.Remove, key, null, null);
        }

        public static DictionaryWrite<TKey, TValue> CreateTryRemove(TKey key)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.TryRemove, key, null, null);
        }

        public DictionaryWrite(DictionaryWriteType type, TKey key, Func<TValue>? valueIfAdding, Func<TValue, TValue>? valueIfUpdating)
        {
            Type = type;
            Key = key;
            ValueIfAdding = valueIfAdding;
            ValueIfUpdating = valueIfUpdating;
        }

        public DictionaryWriteType Type { get; }
        public TKey Key { get; }
        public Func<TValue>? ValueIfAdding { get; }
        public Func<TValue, TValue>? ValueIfUpdating { get; }

        public override string ToString()
        {
            var withWithoutValueIfAdding =  ValueIfAdding != default ? "with" : "without";
            var withWithoutValueIfUpdating =  ValueIfUpdating != default ? "with" : "without";
            return
                $"{Type} {Key} {withWithoutValueIfAdding} ValueIfAdding and {withWithoutValueIfUpdating} ValueIfUpdating";
        }
    }
}