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

        protected override bool TryGetValueInsideLock(TKey key, out TValue value)
        {
            if (!State.TryGetValue(key, out value))
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

                return false;
            }

            return true;
        }

        protected override bool TryGetValueOutsideLock(TKey key, out TValue value)
        {
            lock (Lock)
            {
                return TryGetValueInsideLock(key, out value);
            }
        }
    }
}