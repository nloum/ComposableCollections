using System;
using ComposableCollections.Common;
using SimpleMonads;

namespace ComposableCollections.Set.Write
{
    public class SetWrite<TKey, TValue>
    {
        public static SetWrite<TKey, TValue> CreateAdd(TKey key, Func<TValue> value)
        {
            return new SetWrite<TKey, TValue>(CollectionWriteType.Add, key, value.ToMaybe(), Maybe<Func<TValue, TValue>>.Nothing());
        }

        public static SetWrite<TKey, TValue> CreateTryAdd(TKey key, Func<TValue> value)
        {
            return new SetWrite<TKey, TValue>(CollectionWriteType.TryAdd, key, value.ToMaybe(), Maybe<Func<TValue, TValue>>.Nothing());
        }

        public static SetWrite<TKey, TValue> CreateUpdate(TKey key, Func<TValue, TValue> value)
        {
            return new SetWrite<TKey, TValue>(CollectionWriteType.Update, key, Maybe<Func<TValue>>.Nothing(), value.ToMaybe());
        }

        public static SetWrite<TKey, TValue> CreateTryUpdate(TKey key, Func<TValue, TValue> value)
        {
            return new SetWrite<TKey, TValue>(CollectionWriteType.TryUpdate, key, Maybe<Func<TValue>>.Nothing(), value.ToMaybe());
        }

        public static SetWrite<TKey, TValue> CreateAddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            return new SetWrite<TKey, TValue>(CollectionWriteType.AddOrUpdate, key, valueIfAdding.ToMaybe(), valueIfUpdating.ToMaybe());
        }

        public static SetWrite<TKey, TValue> CreateRemove(TKey key)
        {
            return new SetWrite<TKey, TValue>(CollectionWriteType.Remove, key, Maybe<Func<TValue>>.Nothing(), Maybe<Func<TValue, TValue>>.Nothing());
        }

        public static SetWrite<TKey, TValue> CreateTryRemove(TKey key)
        {
            return new SetWrite<TKey, TValue>(CollectionWriteType.TryRemove, key, Maybe<Func<TValue>>.Nothing(), Maybe<Func<TValue, TValue>>.Nothing());
        }

        public SetWrite(CollectionWriteType type, TKey key, IMaybe<Func<TValue>> valueIfAdding, IMaybe<Func<TValue, TValue>> valueIfUpdating)
        {
            Type = type;
            Key = key;
            ValueIfAdding = valueIfAdding;
            ValueIfUpdating = valueIfUpdating;
        }

        public CollectionWriteType Type { get; }
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