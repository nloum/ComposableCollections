using ComposableCollections.Common;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class NonQueryableTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter> : INonQueryableTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly INonQueryableDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _nonQueryableTransformations;
        private readonly INonQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _nonQueryableReadOnlyTransformations;

        public NonQueryableTransactionalTransformationsWithBuiltInKey(INonQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> nonQueryableReadOnlyTransformations, INonQueryableDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> nonQueryableTransformations)
        {
            _nonQueryableTransformations = nonQueryableTransformations;
            _nonQueryableReadOnlyTransformations = nonQueryableReadOnlyTransformations;
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, IDisposableDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return source.Select(readOnly => _nonQueryableReadOnlyTransformations.Transform(readOnly, parameter),
                readWrite => _nonQueryableTransformations.Transform(readWrite, parameter));
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, ICachedDisposableDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return source.Select(readOnly => _nonQueryableReadOnlyTransformations.Transform(readOnly, parameter),
                readWrite => _nonQueryableTransformations.Transform(readWrite, parameter));
        }
    }
}