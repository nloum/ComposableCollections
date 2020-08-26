using ComposableCollections.Common;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class TransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter> :
            ITransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _readOnlyTransformationsWithBuiltInKey;
        private readonly IDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _readWriteTransformationsWithBuiltInKey;

        public TransactionalTransformationsWithBuiltInKey(IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> readOnlyTransformationsWithBuiltInKey, IDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> readWriteTransformationsWithBuiltInKey)
        {
            _readOnlyTransformationsWithBuiltInKey = readOnlyTransformationsWithBuiltInKey;
            _readWriteTransformationsWithBuiltInKey = readWriteTransformationsWithBuiltInKey;
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, IDisposableDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableDictionaryWithBuiltInKey<TKey2, TValue2>>(
                () => _readOnlyTransformationsWithBuiltInKey.Transform(source.BeginRead(), parameter),
                () => _readWriteTransformationsWithBuiltInKey.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, ICachedDisposableDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableDictionaryWithBuiltInKey<TKey2, TValue2>>(
                () => _readOnlyTransformationsWithBuiltInKey.Transform(source.BeginRead(), parameter),
                () => _readWriteTransformationsWithBuiltInKey.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, IDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>>(
                () => _readOnlyTransformationsWithBuiltInKey.Transform(source.BeginRead(), parameter),
                () => _readWriteTransformationsWithBuiltInKey.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>>(
                () => _readOnlyTransformationsWithBuiltInKey.Transform(source.BeginRead(), parameter),
                () => _readWriteTransformationsWithBuiltInKey.Transform(source.BeginWrite(), parameter));
        }
    }
}