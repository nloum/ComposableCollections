using ComposableCollections.Common;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class TransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> : ITransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly INonQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _nonQueryableReadOnlyTransformations;
        private readonly INonQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _nonQueryableReadWriteTransformations;
        private readonly IQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _queryableReadOnlyTransformations;
        private readonly IQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _queryableReadWriteTransformations;

        public TransactionalTransformations(INonQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> nonQueryableReadOnlyTransformations, INonQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> nonQueryableReadWriteTransformations, IQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> queryableReadOnlyTransformations, IQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> queryableReadWriteTransformations)
        {
            _nonQueryableReadOnlyTransformations = nonQueryableReadOnlyTransformations;
            _nonQueryableReadWriteTransformations = nonQueryableReadWriteTransformations;
            _queryableReadOnlyTransformations = queryableReadOnlyTransformations;
            _queryableReadWriteTransformations = queryableReadWriteTransformations;
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

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, IDisposableQueryableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>, IDisposableQueryableDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, IDisposableQueryableDictionary<TKey2, TValue2>>(
                () => _queryableReadOnlyTransformations.Transform(source.BeginRead(), parameter),
                () => _queryableReadWriteTransformations.Transform(source.BeginWrite(), parameter));
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableQueryableDictionary<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey1, TValue1>, ICachedDisposableQueryableDictionary<TKey1, TValue1>> source, TParameter parameter)
        {
            return new AnonymousTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey2, TValue2>, ICachedDisposableQueryableDictionary<TKey2, TValue2>>(
                () => _queryableReadOnlyTransformations.Transform(source.BeginRead(), parameter),
                () => _queryableReadWriteTransformations.Transform(source.BeginWrite(), parameter));
        }
    }
}