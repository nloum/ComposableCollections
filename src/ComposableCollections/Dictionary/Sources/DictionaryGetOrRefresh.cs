using SimpleMonads;

namespace ComposableCollections.Dictionary.Sources
{
    public delegate void RefreshValue<TKey, TValue>(TKey key, TValue previousValue, out IMaybe<TValue> maybeValue, out bool persist);
    
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
                _refreshValue(key, value, out var maybeValue, out var persist);
                
                if (maybeValue.HasValue)
                {
                    if (persist)
                    {
                        State[key] = maybeValue.Value;
                    }

                    value = maybeValue.Value;
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