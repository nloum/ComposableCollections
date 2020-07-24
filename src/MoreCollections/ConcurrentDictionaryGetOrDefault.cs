namespace MoreCollections
{
    public class ConcurrentDictionaryGetOrDefault<TKey, TValue> : ConcurrentDictionaryEx<TKey, TValue>
    {
        private readonly GetDefaultValue<TKey, TValue> _getDefaultValue;

        public ConcurrentDictionaryGetOrDefault(GetDefaultValue<TKey, TValue> getDefaultValue)
        {
            _getDefaultValue = getDefaultValue;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            lock (Lock)
            {
                if (!base.TryGetValue(key, out value))
                {
                    _getDefaultValue(key, out var maybeValue, out var persist);
                
                    if (maybeValue.HasValue)
                    {
                        if (persist)
                        {
                            State = State.Add(key, maybeValue.Value);
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
        }

        protected override bool ContainsKeyInsideLock(TKey key)
        {
            if (!State.ContainsKey(key))
            {
                _getDefaultValue(key, out var maybeValue, out var persist);
                
                if (maybeValue.HasValue)
                {
                    if (persist)
                    {
                        State = State.Add(key, maybeValue.Value);
                    }

                    return true;
                }
            }

            return true;
        }

        protected override bool ContainsKeyOutsideLock(TKey key)
        {
            lock (Lock)
            {
                return ContainsKeyInsideLock(key);
            }
        }
    }
}