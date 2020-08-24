using ComposableCollections.Common;
using ComposableCollections.Dictionary.WithBuiltInKey;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class TransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> : ITransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly IComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _readOnlyTransformations;
        private readonly IComposableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _readWriteTransformations;
        private readonly IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _readOnlyTransformationsWithBuiltInKey;
        private readonly IDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _readWriteTransformationsWithBuiltInKey;

        public TransactionalTransformations(IComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> readOnlyTransformations, IComposableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> readWriteTransformations, IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> readOnlyTransformationsWithBuiltInKey, IDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> readWriteTransformationsWithBuiltInKey)
        {
            _readOnlyTransformations = readOnlyTransformations;
            _readWriteTransformations = readWriteTransformations;
            _readOnlyTransformationsWithBuiltInKey = readOnlyTransformationsWithBuiltInKey;
            _readWriteTransformationsWithBuiltInKey = readWriteTransformationsWithBuiltInKey;
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, IDisposableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>, IDisposableDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, IDisposableDictionary<TKey2, TValue2>>(
                () => _readOnlyTransformations.Transform(source.BeginRead(), parameter),
                () => _readWriteTransformations.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>, ICachedDisposableDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableDictionary<TKey2, TValue2>>(
                () => _readOnlyTransformations.Transform(source.BeginRead(), parameter),
                () => _readWriteTransformations.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, IDisposableQueryableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>, IDisposableQueryableDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, IDisposableQueryableDictionary<TKey2, TValue2>>(
                () => _readOnlyTransformations.Transform(source.BeginRead(), parameter),
                () => _readWriteTransformations.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableQueryableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>, ICachedDisposableQueryableDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableQueryableDictionary<TKey2, TValue2>>(
                () => _readOnlyTransformations.Transform(source.BeginRead(), parameter),
                () => _readWriteTransformations.Transform(source.BeginWrite(), parameter));
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