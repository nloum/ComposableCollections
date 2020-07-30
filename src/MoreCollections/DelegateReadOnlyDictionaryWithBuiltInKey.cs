namespace MoreCollections
{
    public class DelegateReadOnlyDictionaryWithBuiltInKey<TKey, TValue> :
        DelegateReadOnlyDictionaryEx<TKey, TValue>, IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        public DelegateReadOnlyDictionaryWithBuiltInKey(IReadOnlyDictionaryEx<TKey, TValue> wrapped) : base(wrapped)
        {
        }
    }
}