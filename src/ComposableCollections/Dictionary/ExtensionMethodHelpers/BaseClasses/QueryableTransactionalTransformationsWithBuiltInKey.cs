using ComposableCollections.Common;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class QueryableTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter> : IQueryableTransactionalTransformationsWithBuiltInKey<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly IQueryableDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _queryableTransformations;
        private readonly IQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
            _queryableReadOnlyTransformations;

        public QueryableTransactionalTransformationsWithBuiltInKey(IQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> queryableReadOnlyTransformations, IQueryableDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> queryableTransformations)
        {
            _queryableTransformations = queryableTransformations;
            _queryableReadOnlyTransformations = queryableReadOnlyTransformations;
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, IDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, IDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return source
                .Select(readOnly => _queryableReadOnlyTransformations.Transform(readOnly, parameter),
                    readWrite => _queryableTransformations.Transform(readWrite, parameter));
        }

        public ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2>> Transform(ITransactionalCollection<IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1>, ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1>> source, TParameter parameter)
        {
            return source
                .Select(readOnly => _queryableReadOnlyTransformations.Transform(readOnly, parameter),
                    readWrite => _queryableTransformations.Transform(readWrite, parameter));
        }
    }
}