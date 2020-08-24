using System;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Write
{
    public class DictionaryWrite<TKey, TValue>
    {
        public static DictionaryWrite<TKey, TValue> CreateAdd(TKey key, Func<TValue> value)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.Add, key, value.ToMaybe(), Maybe<Func<TValue, TValue>>.Nothing());
        }

        public static DictionaryWrite<TKey, TValue> CreateTryAdd(TKey key, Func<TValue> value)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.TryAdd, key, value.ToMaybe(), Maybe<Func<TValue, TValue>>.Nothing());
        }

        public static DictionaryWrite<TKey, TValue> CreateUpdate(TKey key, Func<TValue, TValue> value)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.Update, key, Maybe<Func<TValue>>.Nothing(), value.ToMaybe());
        }

        public static DictionaryWrite<TKey, TValue> CreateTryUpdate(TKey key, Func<TValue, TValue> value)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.TryUpdate, key, Maybe<Func<TValue>>.Nothing(), value.ToMaybe());
        }

        public static DictionaryWrite<TKey, TValue> CreateAddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.AddOrUpdate, key, valueIfAdding.ToMaybe(), valueIfUpdating.ToMaybe());
        }

        public static DictionaryWrite<TKey, TValue> CreateRemove(TKey key)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.Remove, key, Maybe<Func<TValue>>.Nothing(), Maybe<Func<TValue, TValue>>.Nothing());
        }

        public static DictionaryWrite<TKey, TValue> CreateTryRemove(TKey key)
        {
            return new DictionaryWrite<TKey, TValue>(DictionaryWriteType.TryRemove, key, Maybe<Func<TValue>>.Nothing(), Maybe<Func<TValue, TValue>>.Nothing());
        }

        public DictionaryWrite(DictionaryWriteType type, TKey key, IMaybe<Func<TValue>> valueIfAdding, IMaybe<Func<TValue, TValue>> valueIfUpdating)
        {
            Type = type;
            Key = key;
            ValueIfAdding = valueIfAdding;
            ValueIfUpdating = valueIfUpdating;
        }

        public DictionaryWriteType Type { get; }
        public TKey Key { get; }
        public IMaybe<Func<TValue>> ValueIfAdding { get; }
        public IMaybe<Func<TValue, TValue>> ValueIfUpdating { get; }

        public override string ToString()
        {
            var withWithoutValueIfAdding = ValueIfAdding.HasValue ? "with" : "without";
            var withWithoutValueIfUpdating = ValueIfUpdating.HasValue ? "with" : "without";
            return
                $"{Type} {Key} {withWithoutValueIfAdding} ValueIfAdding and {withWithoutValueIfUpdating} ValueIfUpdating";
        }
    }
}