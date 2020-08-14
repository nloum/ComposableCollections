using System;
using SimpleMonads;

namespace MoreCollections
{
    public class DictionaryMutation<TKey, TValue>
    {
        public static DictionaryMutation<TKey, TValue> CreateAdd(TKey key, Func<TValue> value)
        {
            return new DictionaryMutation<TKey, TValue>(DictionaryMutationType.Add, key, value.ToMaybe(), Maybe<Func<TValue, TValue>>.Nothing());
        }

        public static DictionaryMutation<TKey, TValue> CreateTryAdd(TKey key, Func<TValue> value)
        {
            return new DictionaryMutation<TKey, TValue>(DictionaryMutationType.TryAdd, key, value.ToMaybe(), Maybe<Func<TValue, TValue>>.Nothing());
        }

        public static DictionaryMutation<TKey, TValue> CreateUpdate(TKey key, Func<TValue, TValue> value)
        {
            return new DictionaryMutation<TKey, TValue>(DictionaryMutationType.Update, key, Maybe<Func<TValue>>.Nothing(), value.ToMaybe());
        }

        public static DictionaryMutation<TKey, TValue> CreateTryUpdate(TKey key, Func<TValue, TValue> value)
        {
            return new DictionaryMutation<TKey, TValue>(DictionaryMutationType.TryUpdate, key, Maybe<Func<TValue>>.Nothing(), value.ToMaybe());
        }

        public static DictionaryMutation<TKey, TValue> CreateAddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            return new DictionaryMutation<TKey, TValue>(DictionaryMutationType.AddOrUpdate, key, valueIfAdding.ToMaybe(), valueIfUpdating.ToMaybe());
        }

        public static DictionaryMutation<TKey, TValue> CreateRemove(TKey key)
        {
            return new DictionaryMutation<TKey, TValue>(DictionaryMutationType.Remove, key, Maybe<Func<TValue>>.Nothing(), Maybe<Func<TValue, TValue>>.Nothing());
        }

        public static DictionaryMutation<TKey, TValue> CreateTryRemove(TKey key)
        {
            return new DictionaryMutation<TKey, TValue>(DictionaryMutationType.TryRemove, key, Maybe<Func<TValue>>.Nothing(), Maybe<Func<TValue, TValue>>.Nothing());
        }

        internal DictionaryMutation(DictionaryMutationType type, TKey key, IMaybe<Func<TValue>> valueIfAdding, IMaybe<Func<TValue, TValue>> valueIfUpdating)
        {
            Type = type;
            Key = key;
            ValueIfAdding = valueIfAdding;
            ValueIfUpdating = valueIfUpdating;
        }

        public DictionaryMutationType Type { get; }
        public TKey Key { get; }
        public IMaybe<Func<TValue>> ValueIfAdding { get; }
        public IMaybe<Func<TValue, TValue>> ValueIfUpdating { get; }
    }
}