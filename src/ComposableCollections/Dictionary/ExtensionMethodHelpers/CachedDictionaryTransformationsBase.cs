namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public abstract class CachedDictionaryTransformationsBase<TKey, TValue, TParameter> : ICachedDictionaryTransformations<TKey, TValue, TKey, TValue, TParameter>
    {
        public abstract ICachedDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, TParameter p);

        public ICachedQueryableDictionary<TKey, TValue> Transform(IQueryableDictionary<TKey, TValue> source, TParameter p)
        {
            var result = Transform((IComposableDictionary<TKey, TValue>) source, p);
            return new AnonymousCachedQueryableDictionary<TKey, TValue>(
                result,
                result.AsBypassCache,
                result.AsNeverFlush,
                result.FlushCache,
                result.GetMutations,
                source.Values );
        }

        public ICachedDisposableQueryableDictionary<TKey, TValue> Transform(IDisposableQueryableDictionary<TKey, TValue> source, TParameter p)
        {
            var result = Transform((IComposableDictionary<TKey, TValue>) source, p);
            return new AnonymousCachedDisposableQueryableDictionary<TKey, TValue>(
                result,
                result.AsBypassCache,
                result.AsNeverFlush,
                result.FlushCache,
                result.GetMutations,
                source,
                source.Values );
        }

        public ICachedDisposableDictionary<TKey, TValue> Transform(IDisposableDictionary<TKey, TValue> source, TParameter p)
        {
            var result = Transform((IComposableDictionary<TKey, TValue>) source, p);
            return new AnonymousCachedDisposableDictionary<TKey, TValue>(
                result,
                result.AsBypassCache,
                result.AsNeverFlush,
                result.FlushCache,
                result.GetMutations,
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
            var result = new AnonymousCachedQueryableDictionary<TKey, TValue>(x, x.AsBypassCache, x.AsNeverFlush, x.FlushCache, x.GetMutations, source.Values);
            return new CachedQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(result, source.GetKey);
        }

        public ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> Transform(IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> source, TParameter p)
        {
            var x = Transform(source.AsComposableDictionary(), p);
            var result = new AnonymousCachedDisposableQueryableDictionary<TKey, TValue>(x, x.AsBypassCache, x.AsNeverFlush, x.FlushCache, x.GetMutations, source, source.Values);
            return new CachedDisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(result, source.GetKey);
        }

        public ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> Transform(IDisposableDictionaryWithBuiltInKey<TKey, TValue> source, TParameter p)
        {
            var x = Transform(source.AsComposableDictionary(), p);
            var result = new AnonymousCachedDisposableDictionary<TKey, TValue>(x, x.AsBypassCache, x.AsNeverFlush, x.FlushCache, x.GetMutations, source);
            return new CachedDisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(result, source.GetKey);
        }
    }
}