using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericNumbers.Relational;

namespace MoreCollections
{
    internal class KeyValuePairImpl<TKey, TValue> : IKeyValuePair<TKey, TValue>, IComparable<IKeyValuePair<TKey, TValue>>
    {
        public TKey Key { get; }

        public TValue Value { get; }

        public KeyValuePairImpl(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public int CompareTo(IKeyValuePair<TKey, TValue> other)
        {
            var comparison = Key.CompareTo(other.Key);
            if (comparison != 0)
                return comparison;
            comparison = Value.CompareTo(other.Value);
            return comparison;
        }

        protected bool Equals(KeyValuePairImpl<TKey, TValue> other)
        {
            return EqualityComparer<TKey>.Default.Equals(Key, other.Key) && EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KeyValuePairImpl<TKey, TValue>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<TKey>.Default.GetHashCode(Key)*397) ^ EqualityComparer<TValue>.Default.GetHashCode(Value);
            }
        }
    }
}
