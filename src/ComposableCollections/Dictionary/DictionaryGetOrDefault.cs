using SimpleMonads;

namespace ComposableCollections.Dictionary
{
    public delegate void GetDefaultValue<TKey, TValue>(TKey key, out IMaybe<TValue> maybeValue, out bool persist);
    
    public class DictionaryGetOrDefault<TKey, TValue> : ComposableDictionary<TKey, TValue>
    {
        private readonly GetDefaultValue<TKey, TValue> _getDefaultValue;

        public DictionaryGetOrDefault(GetDefaultValue<TKey, TValue> getDefaultValue)
        {
            _getDefaultValue = getDefaultValue;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            if (!base.TryGetValue(key, out value))
            {
                _getDefaultValue(key, out var maybeValue, out var persist);
                
                if (maybeValue.HasValue)
                {
                    if (persist)
                    {
                        State.Add(key, maybeValue.Value);
                    }

                    value = maybeValue.Value;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public override bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out var value);
        }
    }
}