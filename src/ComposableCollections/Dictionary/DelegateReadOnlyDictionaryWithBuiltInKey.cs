namespace ComposableCollections.Dictionary
{
    public class DelegateReadOnlyDictionaryWithBuiltInKey<TKey, TValue> :
        DelegateReadOnlyDictionary<TKey, TValue>, IReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        public DelegateReadOnlyDictionaryWithBuiltInKey(IComposableReadOnlyDictionary<TKey, TValue> wrapped) : base(wrapped)
        {
        }
        
        protected DelegateReadOnlyDictionaryWithBuiltInKey()
        {
        }
    }
}