using System;
using System.Linq;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class
        ComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> :
            IComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly Func<IComposableReadOnlyDictionary<TKey1, TValue1>, TParameter,
            IComposableReadOnlyDictionary<TKey2, TValue2>> _transform;
        private readonly Func<IQueryable<TValue1>, TParameter, IQueryable<TValue2>> _convertQueryable;

        public ComposableReadOnlyDictionaryTransformations(Func<IComposableReadOnlyDictionary<TKey1, TValue1>, TParameter, IComposableReadOnlyDictionary<TKey2, TValue2>> transform, Func<IQueryable<TValue1>, TParameter, IQueryable<TValue2>> convertQueryable)
        {
            _transform = transform;
            _convertQueryable = convertQueryable;
        }

        public IComposableReadOnlyDictionary<TKey2, TValue2> Transform(
            IComposableReadOnlyDictionary<TKey1, TValue1> source, TParameter parameter)
        {
            return _transform(source, parameter);
        }

        protected IQueryable<TValue2> Convert(IQueryable<TValue1> queryable, TParameter parameter)
        {
            return _convertQueryable(queryable, parameter);
        }
        
        public IQueryableReadOnlyDictionary<TKey2, TValue2> Transform(IQueryableReadOnlyDictionary<TKey1, TValue1> source,
            TParameter parameter)
        {
            return new QueryableReadOnlyDictionaryAdapter<TKey2, TValue2>(
                Transform((IComposableReadOnlyDictionary<TKey1, TValue1>) source, parameter), Convert(source.Values, parameter));
        }

        public IDisposableQueryableReadOnlyDictionary<TKey2, TValue2> Transform(
            IDisposableQueryableReadOnlyDictionary<TKey1, TValue1> source, TParameter parameter)
        {
            return new DisposableQueryableReadOnlyDictionaryAdapter<TKey2, TValue2>(Transform((IComposableReadOnlyDictionary<TKey1, TValue1>)source, parameter), source, Convert(source.Values, parameter));
        }

        public IDisposableReadOnlyDictionary<TKey2, TValue2> Transform(IDisposableReadOnlyDictionary<TKey1, TValue1> source,
            TParameter parameter)
        {
            return new DisposableReadOnlyDictionaryAdapter<TKey2, TValue2>(
                Transform((IComposableReadOnlyDictionary<TKey1, TValue1>) source, parameter), source);
        }
    }
}