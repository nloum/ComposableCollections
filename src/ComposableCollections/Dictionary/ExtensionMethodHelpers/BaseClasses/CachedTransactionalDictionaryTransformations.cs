using ComposableCollections.Common;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class CachedTransactionalDictionaryTransformations<TKey, TValue, TParameter> : ICachedTransactionalDictionaryTransformations<TKey, TValue, TKey, TValue, TParameter>, ICachedTransactionalDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, TParameter>
    {
        private readonly ICachedDictionaryTransformations<TKey, TValue, TKey, TValue, TParameter>
            _cachedDictionaryTransformations;
        private readonly ICachedDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, TParameter>
            _cachedDictionaryWithBuiltInKeyTransformations;

        public CachedTransactionalDictionaryTransformations(ICachedDictionaryTransformations<TKey, TValue, TKey, TValue, TParameter> cachedDictionaryTransformations, ICachedDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, TParameter> cachedDictionaryWithBuiltInKeyTransformations)
        {
            _cachedDictionaryTransformations = cachedDictionaryTransformations;
            _cachedDictionaryWithBuiltInKeyTransformations = cachedDictionaryWithBuiltInKeyTransformations;
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, IDisposableDictionary<TKey, TValue>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey, TValue>, ICachedDisposableDictionary<TKey, TValue>>(
                () => source.BeginRead(),
                () => _cachedDictionaryTransformations.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, ICachedDisposableQueryableDictionary<TKey, TValue>>(
                () => source.BeginRead(),
                () => _cachedDictionaryTransformations.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableDictionaryWithBuiltInKey<TKey, TValue>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue>>(
                () => source.BeginRead(),
                () => _cachedDictionaryWithBuiltInKeyTransformations.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue>>(
                () => source.BeginRead(),
                () => _cachedDictionaryWithBuiltInKeyTransformations.Transform(source.BeginWrite(), parameter));
        }
    }
}