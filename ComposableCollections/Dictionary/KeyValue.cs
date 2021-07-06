using System;
using System.Collections.Generic;
using GenericNumbers.Relational;

namespace ComposableCollections.Dictionary
{
    public class KeyValue<TKey, TValue> : IKeyValue<TKey, TValue>, IComparable<IKeyValue<TKey, TValue>>
    {
        public TKey Key { get; }

        public TValue Value { get; }

        public KeyValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public int CompareTo(IKeyValue<TKey, TValue> other)
        {
            var comparison = Key.CompareTo(other.Key);
            if (comparison != 0)
                return comparison;
            comparison = Value.CompareTo(other.Value);
            return comparison;
        }

        protected bool Equals(KeyValue<TKey, TValue> other)
        {
            return EqualityComparer<TKey>.Default.Equals(Key, other.Key) && EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KeyValue<TKey, TValue>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<TKey>.Default.GetHashCode(Key)*397) ^ EqualityComparer<TValue>.Default.GetHashCode(Value);
            }
        }

        public override string ToString()
        {
            return $"[{Key}, {Value}]";
        }
    }
}
