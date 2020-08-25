using SimpleMonads;

namespace ComposableCollections.Dictionary.Sources
{
    public delegate bool RefreshValue<TKey, TValue>(TKey key, TValue previousValue, out TValue refreshedValue, out bool persist);
    
    public class DictionaryGetOrRefresh<TKey, TValue> : ComposableDictionary<TKey, TValue>
    {
        private readonly RefreshValue<TKey, TValue> _refreshValue;

        public DictionaryGetOrRefresh(RefreshValue<TKey, TValue> refreshValue)
        {
            _refreshValue = refreshValue;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            if (base.TryGetValue(key, out value))
            {
                var hasValue = _refreshValue(key, value, out value, out var persist);
                
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