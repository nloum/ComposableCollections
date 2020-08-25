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
                    var hasValue = _refreshValue(key, value, out value, out var persist);
                
                    if (hasValue)
                    {
                        if (persist)
                        {
                            State = State.SetItem(key, value);
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
        }

        protected override bool TryGetValueInsideLock(TKey key, out TValue value)
        {
            if (base.TryGetValue(key, out value))
            {
                var hasValue = _refreshValue(key, value, out value, out var persist);
                
                if (hasValue)
                {
                    if (persist)
                    {
                        State = State.SetItem(key, value);
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

        protected override bool TryGetValueOutsideLock(TKey key, out TValue value)
        {
            lock (Lock)
            {
                return TryGetValueInsideLock(key, out value);
            }
        }
    }
}