namespace MoreCollections
{
    public interface ICachingDictionary<TKey, TValue> : IDictionaryEx<TKey, TValue>
    {
        IDictionaryEx<TKey, TValue> BypassCache();
        void FlushCache();
    }
}