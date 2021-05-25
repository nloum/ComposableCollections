using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Sources
{
    public delegate bool RefreshValueWithOptionalPersistence<TKey, TValue>(TKey key, TValue previousValue, out TValue refreshedValue, out bool persist);
    public delegate bool RefreshValueWithOptionalPersistenceWithPossibleRecursion<TKey, TValue>(TKey key, TValue previousValue, IComposableReadOnlyDictionary<TKey, TValue> withRefreshedValues, out TValue refreshedValue, out bool persist);
    public delegate bool RefreshValue<TKey, TValue>(TKey key, TValue previousValue, out TValue refreshedValue);
    public delegate bool RefreshValueWithPossibleRecursion<TKey, TValue>(TKey key, TValue previousValue, IComposableReadOnlyDictionary<TKey, TValue> withRefreshedValues, out TValue refreshedValue);
    public delegate TValue AlwaysRefreshValue<TKey, TValue>(TKey key, TValue previousValue);
    public delegate TValue AlwaysRefreshValueWithPossibleRecursion<TKey, TValue>(TKey key, TValue previousValue, IComposableReadOnlyDictionary<TKey, TValue> withRefreshedValues);
    
    public class DictionaryGetOrRefresh<TKey, TValue> : ComposableDictionary<TKey, TValue>
    {
        private readonly RefreshValueWithOptionalPersistenceWithPossibleRecursion<TKey, TValue> _refreshValue;

        public DictionaryGetOrRefresh(RefreshValueWithOptionalPersistenceWithPossibleRecursion<TKey, TValue> refreshValue)
        {
            _refreshValue = refreshValue;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            if (base.TryGetValue(key, out value))
            {
                var hasValue = _refreshValue(key, value, this, out value, out var persist);
                
                if (hasValue)
                {
                    if (persist)
                    {
                        State[key] = value;
                    }

                    return true;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public override bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out var value);
        }
    }
}