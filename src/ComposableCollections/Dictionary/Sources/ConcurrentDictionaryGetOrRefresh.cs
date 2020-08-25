namespace ComposableCollections.Dictionary.Sources
{
    public class ConcurrentDictionaryGetOrRefresh<TKey, TValue> : ConcurrentDictionary<TKey, TValue>
    {
        private readonly RefreshValue<TKey, TValue> _refreshValue;

        public ConcurrentDictionaryGetOrRefresh(RefreshValue<TKey, TValue> refreshValue)
        {
            _refreshValue = refreshValue;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            lock (Lock)
            {
                if (base.TryGetValue(key, out value))
                {
                    _refreshValue(key, value, out var maybeValue, out var persist);
                
                    if (maybeValue.HasValue)
                    {
                        if (persist)
                        {
                            State = State.SetItem(key, maybeValue.Value);
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
        }

        protected override bool TryGetValueInsideLock(TKey key, out TValue value)
        {
            if (base.TryGetValue(key, out value))
            {
                _refreshValue(key, value, out var maybeValue, out var persist);
                
                if (maybeValue.HasValue)
                {
                    if (persist)
                    {
                        State = State.SetItem(key, maybeValue.Value);
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

        protected override bool TryGetValueOutsideLock(TKey key, out TValue value)
        {
            lock (Lock)
            {
                return TryGetValueInsideLock(key, out value);
            }
        }
    }
}