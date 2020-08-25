using System;
using System.Linq;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public abstract class
        ComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> :
            IComposableReadOnlyDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>,
            IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly Func<IComposableReadOnlyDictionary<TKey1, TValue1>, TParameter,
            IComposableReadOnlyDictionary<TKey2, TValue2>> _transform;
        private readonly Func<Func<TValue1, TKey1>, TParameter, Func<TValue2, TKey2>> _convertGetKey;
        private readonly Func<IQueryable<TValue1>, TParameter, IQueryable<TValue2>> _convertQueryable;

        protected ComposableReadOnlyDictionaryTransformations(Func<IComposableReadOnlyDictionary<TKey1, TValue1>, TParameter, IComposableReadOnlyDictionary<TKey2, TValue2>> transform, Func<Func<TValue1, TKey1>, TParameter, Func<TValue2, TKey2>> convertGetKey, Func<IQueryable<TValue1>, TParameter, IQueryable<TValue2>> convertQueryable)
        {
            _transform = transform;
            _convertGetKey = convertGetKey;
            _convertQueryable = convertQueryable;
        }

        public IComposableReadOnlyDictionary<TKey2, TValue2> Transform(
            IComposableReadOnlyDictionary<TKey1, TValue1> source, TParameter parameter)
        {
            return _transform(source, parameter);
        }

        protected Func<TValue2, TKey2> Convert(Func<TValue1, TKey1> getKey1, TParameter parameter)
        {
            return _convertGetKey(getKey1, parameter);
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

        public IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(
            IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter parameter)
        {
            return new DisposableQueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(Transform(source.AsDisposableQueryableReadOnlyDictionary(), parameter), Convert(source.GetKey, parameter));
        }

        public IQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source,
            TParameter parameter)
        {
            return new QueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(Transform(source.AsQueryableReadOnlyDictionary(), parameter), Convert(source.GetKey, parameter));
        }

        public IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source,
            TParameter parameter)
        {
            return new DisposableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(Transform(source.AsDisposableReadOnlyDictionary(), parameter), Convert(source.GetKey, parameter));
        }

        public IReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter parameter)
        {
            return new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(Transform(source.AsComposableReadOnlyDictionary(), parameter), Convert(source.GetKey, parameter));
        }
    }
}