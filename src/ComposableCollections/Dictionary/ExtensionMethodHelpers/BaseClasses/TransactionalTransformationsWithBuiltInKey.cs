using ComposableCollections.Common;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class TransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter> :
            ITransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly IQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _queryableReadOnlyTransformationsWithBuiltInKey;
        private readonly IQueryableDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _queryableReadWriteTransformationsWithBuiltInKey;
        private readonly INonQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _nonQueryableReadOnlyTransformationsWithBuiltInKey;
        private readonly INonQueryableDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _nonQueryableReadWriteTransformationsWithBuiltInKey;

        public TransactionalTransformationsWithBuiltInKey(IQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> queryableReadOnlyTransformationsWithBuiltInKey, IQueryableDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> queryableReadWriteTransformationsWithBuiltInKey, INonQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> nonQueryableReadOnlyTransformationsWithBuiltInKey, INonQueryableDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> nonQueryableReadWriteTransformationsWithBuiltInKey)
        {
            _queryableReadOnlyTransformationsWithBuiltInKey = queryableReadOnlyTransformationsWithBuiltInKey;
            _queryableReadWriteTransformationsWithBuiltInKey = queryableReadWriteTransformationsWithBuiltInKey;
            _nonQueryableReadOnlyTransformationsWithBuiltInKey = nonQueryableReadOnlyTransformationsWithBuiltInKey;
            _nonQueryableReadWriteTransformationsWithBuiltInKey = nonQueryableReadWriteTransformationsWithBuiltInKey;
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, IDisposableDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableDictionaryWithBuiltInKey<TKey2, TValue2>>(
                () => _nonQueryableReadOnlyTransformationsWithBuiltInKey.Transform(source.BeginRead(), parameter),
                () => _nonQueryableReadWriteTransformationsWithBuiltInKey.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, ICachedDisposableDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableDictionaryWithBuiltInKey<TKey2, TValue2>>(
                () => _nonQueryableReadOnlyTransformationsWithBuiltInKey.Transform(source.BeginRead(), parameter),
                () => _nonQueryableReadWriteTransformationsWithBuiltInKey.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, IDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>>(
                () => _queryableReadOnlyTransformationsWithBuiltInKey.Transform(source.BeginRead(), parameter),
                () => _queryableReadWriteTransformationsWithBuiltInKey.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>>(
                () => _queryableReadOnlyTransformationsWithBuiltInKey.Transform(source.BeginRead(), parameter),
                () => _queryableReadWriteTransformationsWithBuiltInKey.Transform(source.BeginWrite(), parameter));
        }
    }
}