using System;
using System.Linq;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class
        ReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> :
            IReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>,
            IQueryableReadOnlyDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>
    {
        private readonly Func<IComposableReadOnlyDictionary<TKey1, TValue1>, TParameter,
            IComposableReadOnlyDictionary<TKey2, TValue2>> _transform1;
        private readonly Func<Func<TValue1, TKey1>, TParameter, Func<TValue2, TKey2>> _convertGetKey;
        private readonly Func<IQueryable<TValue1>, TParameter, IQueryable<TValue2>> _convertQueryable;

        public ReadOnlyDictionaryWithBuiltInKeyTransformations(Func<IComposableReadOnlyDictionary<TKey1, TValue1>, TParameter, IComposableReadOnlyDictionary<TKey2, TValue2>> transform, Func<Func<TValue1, TKey1>, TParameter, Func<TValue2, TKey2>> convertGetKey, Func<IQueryable<TValue1>, TParameter, IQueryable<TValue2>> convertQueryable)
        {
            _transform1 = transform;
            _convertGetKey = convertGetKey;
            _convertQueryable = convertQueryable;
        }

        protected Func<TValue2, TKey2> Convert(Func<TValue1, TKey1> getKey1, TParameter parameter)
        {
            return _convertGetKey(getKey1, parameter);
        }

        public IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(
            IDisposableQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter parameter)
        {
            return new DisposableQueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(new DisposableQueryableReadOnlyDictionaryAdapter<TKey2, TValue2>(_transform1(source, parameter), source, _convertQueryable(source.Values, parameter)), Convert(source.GetKey, parameter));
        }

        public IQueryableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IQueryableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source,
            TParameter parameter)
        {
            return new QueryableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(new QueryableReadOnlyDictionaryAdapter<TKey2, TValue2>(_transform1(source, parameter), _convertQueryable(source.Values, parameter)), Convert(source.GetKey, parameter));
        }

        public IDisposableReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source,
            TParameter parameter)
        {
            return new DisposableReadOnlyDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(new DisposableReadOnlyDictionaryAdapter<TKey2, TValue2>(_transform1(source.AsDisposableReadOnlyDictionary(), parameter), source), Convert(source.GetKey, parameter));
        }

        public IReadOnlyDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IReadOnlyDictionaryWithBuiltInKey<TKey1, TValue1> source, TParameter parameter)
        {
            return new ReadOnlyDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(_transform1(source.AsComposableReadOnlyDictionary(), parameter), Convert(source.GetKey, parameter));
        }
    }
}