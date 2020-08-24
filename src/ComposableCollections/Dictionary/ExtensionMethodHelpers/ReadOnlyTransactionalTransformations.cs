namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public class ReadOnlyTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> : IReadOnlyTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly IComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _readOnlyTransformations;
        private readonly IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _readOnlyTransformationsWithBuiltInKey;

        public ReadOnlyTransactionalTransformations(IComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> readOnlyTransformations, IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> readOnlyTransformationsWithBuiltInKey)
        {
            _readOnlyTransformations = readOnlyTransformations;
            _readOnlyTransformationsWithBuiltInKey = readOnlyTransformationsWithBuiltInKey;
        }

        public IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>> Transform(IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousReadOnlyTransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>>(() => _readOnlyTransformations.Transform(source.BeginRead(), parameter));
        }

        public IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>> Transform(IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>>(() => _readOnlyTransformations.Transform(source.BeginRead(), parameter));
        }

        public IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(IReadOnlyTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousReadOnlyTransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>>(() => _readOnlyTransformationsWithBuiltInKey.Transform(source.BeginRead(), parameter));
        }

        public IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>>(() => _readOnlyTransformationsWithBuiltInKey.Transform(source.BeginRead(), parameter));
        }
    }
}