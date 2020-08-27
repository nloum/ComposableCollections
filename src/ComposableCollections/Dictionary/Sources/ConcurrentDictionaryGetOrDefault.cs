namespace ComposableCollections.Dictionary.Sources
{
    public class ConcurrentDictionaryGetOrDefault<TKey, TValue> : ConcurrentDictionary<TKey, TValue>
    {
        private readonly GetDefaultValueWithOptionalPersistence<TKey, TValue> _getDefaultValue;

        public ConcurrentDictionaryGetOrDefault(GetDefaultValueWithOptionalPersistence<TKey, TValue> getDefaultValue)
        {
            _getDefaultValue = getDefaultValue;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            lock (Lock)
            {
                if (!base.TryGetValue(key, out value))
                {
                    var hasValue = _getDefaultValue(key, out value, out var persist);
                
                    if (hasValue)
                    {
                        if (persist)
                        {
                            State = State.Add(key, value);
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
        }

        protected override bool TryGetValueInsideLock(TKey key, out TValue value)
        {
            if (!State.TryGetValue(key, out value))
            {
                var hasValue = _getDefaultValue(key, out value, out var persist);

                if (hasValue)
                {
                    if (persist)
                    {
                        State = State.Add(key, value);
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