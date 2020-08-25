using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public abstract class
        ComposableReadOnlyDictionaryTransformationsBase<TKey, TValue, TParameter> :
            IComposableReadOnlyDictionaryTransformations<TKey, TValue, TKey, TValue, TParameter>,
            IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, TParameter>
    {
        public abstract IComposableReadOnlyDictionary<TKey, TValue> Transform(IComposableReadOnlyDictionary<TKey, TValue> source, TParameter parameter);

        public IQueryableReadOnlyDictionary<TKey, TValue> Transform(IQueryableReadOnlyDictionary<TKey, TValue> source,
            TParameter p1)
        {
            return new QueryableReadOnlyDictionaryAdapter<TKey, TValue>(
                Transform((IComposableReadOnlyDictionary<TKey, TValue>) source, p1), source.Values);
        }

        public IDisposableQueryableReadOnlyDictionary<TKey, TValue> Transform(
            IDisposableQueryableReadOnlyDictionary<TKey, TValue> source, TParameter parameter)
        {
            return new DisposableQueryableReadOnlyDictionaryAdapter<TKey, TValue>(Transform((IComposableReadOnlyDictionary<TKey, TValue>)source, parameter), source, source.Values);
        }

        public IDisposableReadOnlyDictionary<TKey, TValue> Transform(IDisposableReadOnlyDictionary<TKey, TValue> source,
            TParameter parameter)
        {
            return new DisposableReadOnlyDictionaryAdapter<TKey, TValue>(
                Transform((IComposableReadOnlyDictionary<TKey, TValue>) source, parameter), source);
        }

        public IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> Transform(
            IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> source, TParameter parameter)
        {
            return new DisposableQueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(Transform(source.AsDisposableQueryableReadOnlyDictionary(), parameter), source.GetKey);
        }

        public IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> Transform(IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> source,
            TParameter parameter)
        {
            return new QueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(Transform(source.AsQueryableReadOnlyDictionary(), parameter), source.GetKey);
        }

        public IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> Transform(IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue> source,
            TParameter parameter)
        {
            return new DisposableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(Transform(source.AsDisposableReadOnlyDictionary(), parameter), source.GetKey);
        }

        public IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> Transform(IReadOnlyDictionaryWithBuiltInKey<TKey, TValue> source, TParameter parameter)
        {
            return new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey, TValue>(Transform(source.AsComposableReadOnlyDictionary(), parameter), source.GetKey);
        }
    }
}