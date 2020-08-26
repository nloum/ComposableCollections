using System;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class CachedDictionaryTransformations<TKey, TValue, TParameter> : ICachedDictionaryTransformations<TKey, TValue, TKey, TValue, TParameter>, ICachedDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, TParameter>
    {
        private readonly Func<IComposableDictionary<TKey, TValue>, TParameter, ICachedDictionary<TKey, TValue>>
            _transform;

        public CachedDictionaryTransformations(Func<IComposableDictionary<TKey, TValue>, TParameter, ICachedDictionary<TKey, TValue>> transform)
        {
            _transform = transform;
        }

        public ICachedDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, TParameter p)
        {
            return _transform(source, p);
        }

        public ICachedQueryableDictionary<TKey, TValue> Transform(IQueryableDictionary<TKey, TValue> source, TParameter p)
        {
            var result = Transform((IComposableDictionary<TKey, TValue>) source, p);
            return new CachedQueryableDictionaryAdapter<TKey, TValue>(
                result,
                result.AsBypassCache,
                result.AsNeverFlush,
                result.FlushCache,
                result.GetWrites,
                source.Values );
        }

        public ICachedDisposableQueryableDictionary<TKey, TValue> Transform(IDisposableQueryableDictionary<TKey, TValue> source, TParameter p)
        {
            var result = Transform((IComposableDictionary<TKey, TValue>) source, p);
            return new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(
                result,
                result.AsBypassCache,
                result.AsNeverFlush,
                result.FlushCache,
                result.GetWrites,
                source,
                source.Values );
        }

        public ICachedDisposableDictionary<TKey, TValue> Transform(IDisposableDictionary<TKey, TValue> source, TParameter p)
        {
            var result = Transform((IComposableDictionary<TKey, TValue>) source, p);
            return new CachedDisposableDictionaryAdapter<TKey, TValue>(
                result,
                result.AsBypassCache,
                result.AsNeverFlush,
                result.FlushCache,
                result.GetWrites,
                source );
        }

        public ICachedDictionaryWithBuiltInKey<TKey, TValue> Transform(IDictionaryWithBuiltInKey<TKey, TValue> source,
            TParameter p)
        {
            var result=Transform(source.AsComposableDictionary(), p);
            return new CachedDictionaryWithBuiltInKeyAdapter<TKey, TValue>(result, source.GetKey);
        }

        public ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> Transform(IQueryableDictionaryWithBuiltInKey<TKey, TValue> source, TParameter p)
        {
            var x = Transform(source.AsComposableDictionary(), p);
            var result = new CachedQueryableDictionaryAdapter<TKey, TValue>(x, x.AsBypassCache, x.AsNeverFlush, x.FlushCache, x.GetWrites, source.Values);
            return new CachedQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(result, source.GetKey);
        }

        public ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> Transform(IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> source, TParameter p)
        {
            var x = Transform(source.AsComposableDictionary(), p);
            var result = new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(x, x.AsBypassCache, x.AsNeverFlush, x.FlushCache, x.GetWrites, source, source.Values);
            return new CachedDisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(result, source.GetKey);
        }

        public ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> Transform(IDisposableDictionaryWithBuiltInKey<TKey, TValue> source, TParameter p)
        {
            var x = Transform(source.AsComposableDictionary(), p);
            var result = new CachedDisposableDictionaryAdapter<TKey, TValue>(x, x.AsBypassCache, x.AsNeverFlush, x.FlushCache, x.GetWrites, source);
            return new CachedDisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(result, source.GetKey);
        }
    }
}