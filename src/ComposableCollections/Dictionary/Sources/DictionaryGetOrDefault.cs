using SimpleMonads;

namespace ComposableCollections.Dictionary.Sources
{
    public delegate bool GetDefaultValueWithOptionalPersistence<TKey, TValue>(TKey key, out TValue value, out bool persist);
    public delegate bool GetDefaultValue<TKey, TValue>(TKey key, out TValue value);
    public delegate TValue AlwaysGetDefaultValue<TKey, TValue>(TKey key);
    
    public class DictionaryGetOrDefault<TKey, TValue> : ComposableDictionary<TKey, TValue>
    {
        private readonly GetDefaultValueWithOptionalPersistence<TKey, TValue> _getDefaultValue;

        public DictionaryGetOrDefault(GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue)
        {
            _getDefaultValue = getDefaultValue;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            if (!base.TryGetValue(key, out value))
            {
                var hasValue = _getDefaultValue(key, out value, out var persist);
                
                if (hasValue)
                {
                    if (persist)
                    {
                        State.Add(key, value);
                    }

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