using ComposableCollections.Common;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class QueryableTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> : IQueryableTransactionalTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly IQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _queryableReadOnlyTransformations;
        private readonly IQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _queryableReadWriteTransformations;

        public QueryableTransactionalTransformations(IQueryableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> queryableReadOnlyTransformations, IQueryableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> queryableReadWriteTransformations)
        {
            _queryableReadOnlyTransformations = queryableReadOnlyTransformations;
            _queryableReadWriteTransformations = queryableReadWriteTransformations;
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