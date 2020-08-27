using ComposableCollections.Common;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class NonQueryableTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> : INonQueryableTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly INonQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _nonQueryableReadOnlyTransformations;
        private readonly INonQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _nonQueryableReadWriteTransformations;

        public NonQueryableTransactionalTransformations(INonQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> nonQueryableReadOnlyTransformations, INonQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> nonQueryableReadWriteTransformations)
        {
            _nonQueryableReadOnlyTransformations = nonQueryableReadOnlyTransformations;
            _nonQueryableReadWriteTransformations = nonQueryableReadWriteTransformations;
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, IDisposableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>, IDisposableDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, IDisposableDictionary<TKey2, TValue2>>(
                () => _nonQueryableReadOnlyTransformations.Transform(source.BeginRead(), parameter),
                () => _nonQueryableReadWriteTransformations.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableReadOnlyDictionary<TKey1, TValue1>, ICachedDisposableDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableDictionary<TKey2, TValue2>>(
                () => _nonQueryableReadOnlyTransformations.Transform(source.BeginRead(), parameter),
                () => _nonQueryableReadWriteTransformations.Transform(source.BeginWrite(), parameter));
        }
    }
}