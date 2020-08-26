using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class ComposableReadOnlyDictionaryPassThroughTransformations<TKey, TValue, TParameter> : IComposableReadOnlyDictionaryTransformations<TKey, TValue, TKey, TValue, TParameter>
    {
        public IQueryableReadOnlyDictionary<TKey, TValue> Transform(IQueryableReadOnlyDictionary<TKey, TValue> source, TParameter parameter)
        {
            return source;
        }

        public IComposableReadOnlyDictionary<TKey, TValue> Transform(IComposableReadOnlyDictionary<TKey, TValue> source, TParameter parameter)
        {
            return source;
        }

        public IDisposableQueryableReadOnlyDictionary<TKey, TValue> Transform(IDisposableQueryableReadOnlyDictionary<TKey, TValue> source, TParameter parameter)
        {
            return source;
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> Transform(IDisposableReadOnlyDictionary<TKey, TValue> source, TParameter parameter)
        {
            return source;
        }
    }
}