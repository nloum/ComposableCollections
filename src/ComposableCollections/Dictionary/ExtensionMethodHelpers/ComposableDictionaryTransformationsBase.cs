using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Mutations;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers
{
    public abstract class ComposableDictionaryTransformationsBase<TKey, TValue, TParameter> : IComposableDictionaryTransformations<TKey, TValue, TKey, TValue, TParameter>, IDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, TParameter>
    {
        public abstract IComposableDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, TParameter p);

        public ICachedDictionary<TKey, TValue> Transform(ICachedDictionary<TKey, TValue> source, TParameter p)
        {
            return new AnonymousCachedDictionary<TKey, TValue>(
                Transform((IComposableDictionary<TKey, TValue>)source, p), source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetMutations);
        }

        public IQueryableDictionary<TKey, TValue> Transform(IQueryableDictionary<TKey, TValue> source, TParameter p)
        {
            return new AnonymousQueryableDictionary<TKey, TValue>(Transform((IComposableDictionary<TKey, TValue>)source, p), source.Values);
        }

        public ICachedDisposableQueryableDictionary<TKey, TValue> Transform(ICachedDisposableQueryableDictionary<TKey, TValue> source, TParameter p)
        {
            return new AnonymousCachedDisposableQueryableDictionary<TKey, TValue>(Transform((IComposableDictionary<TKey, TValue>)source, p), source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetMutations, source, source.Values);
        }

        public IDisposableQueryableDictionary<TKey, TValue> Transform(IDisposableQueryableDictionary<TKey, TValue> source, TParameter p)
        {
            return new AnonymousDisposableQueryableDictionary<TKey, TValue>(
                Transform((IComposableDictionary<TKey, TValue>) source, p), source, source.Values);
        }

        public ICachedQueryableDictionary<TKey, TValue> Transform(ICachedQueryableDictionary<TKey, TValue> source, TParameter p)
        {
            return new AnonymousCachedQueryableDictionary<TKey, TValue>(
                Transform((IComposableDictionary<TKey, TValue>) source, p), source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetMutations, source.Values);
        }

        public ICachedDisposableDictionary<TKey, TValue> Transform(ICachedDisposableDictionary<TKey, TValue> source, TParameter p)
        {
            return new AnonymousCachedDisposableDictionary<TKey, TValue>(Transform((IComposableDictionary<TKey, TValue>)source, p), source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetMutations, source);
        }

        public IDisposableDictionary<TKey, TValue> Transform(IDisposableDictionary<TKey, TValue> source, TParameter p)
        {
            return new AnonymousDisposableDictionary<TKey, TValue>(Transform((IComposableDictionary<TKey, TValue>)source, p), source);
        }
        
        public ICachedDictionaryWithBuiltInKey<TKey, TValue> Transform(ICachedDictionaryWithBuiltInKey<TKey, TValue> source, TParameter p)
        {
            return new CachedDictionaryWithBuiltInKeyAdapter<TKey, TValue>(Transform(source.AsCachedDictionary(), p), source.GetKey);
        }

        public ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> Transform(ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> source,
            TParameter p)
        {
            return new CachedDisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(Transform(source.AsCachedDisposableQueryableDictionary(), p), source.GetKey);
        }

        public IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> Transform(IDisposableQueryableDictionaryWithBuiltInKey<TKey, TValue> source,
            TParameter p)
        {
            return new DisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(Transform(source.AsDisposableQueryableDictionary(), p), source.GetKey);
        }

        public IQueryableDictionaryWithBuiltInKey<TKey, TValue> Transform(IQueryableDictionaryWithBuiltInKey<TKey, TValue> source, TParameter p)
        {
            return new QueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(Transform(source.AsQueryableDictionary(), p), source.GetKey);
        }

        public ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> Transform(ICachedQueryableDictionaryWithBuiltInKey<TKey, TValue> source, TParameter p)
        {
            return new CachedQueryableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(Transform(source.AsCachedQueryableDictionary(), p), source.GetKey);
        }

        public ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> Transform(ICachedDisposableDictionaryWithBuiltInKey<TKey, TValue> source, TParameter p)
        {
            return new CachedDisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(Transform(source.AsCachedDisposableDictionary(), p), source.GetKey);
        }

        public IDisposableDictionaryWithBuiltInKey<TKey, TValue> Transform(IDisposableDictionaryWithBuiltInKey<TKey, TValue> source, TParameter p)
        {
            return new DisposableDictionaryWithBuiltInKeyAdapter<TKey, TValue>(Transform(source.AsDisposableDictionary(), p), source.GetKey);
        }

        public IDictionaryWithBuiltInKey<TKey, TValue> Transform(IDictionaryWithBuiltInKey<TKey, TValue> source, TParameter p)
        {
            return new DictionaryWithBuiltInKeyAdapter<TKey, TValue>(Transform(source.AsComposableDictionary(), p), source.GetKey);
        }
    }
    
    public abstract class ComposableDictionaryTransformationsBase<TKey1, TValue1, TKey2, TValue2, TParameter> :
        IComposableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>,
        IDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, Tuple<TParameter, Func<TValue2, TKey2>>>
    {
        public abstract IComposableDictionary<TKey2, TValue2> Transform(IComposableDictionary<TKey1, TValue1> source, TParameter p);
        protected abstract IComposableReadOnlyDictionary<TKey2, TValue2> Transform(IComposableReadOnlyDictionary<TKey1, TValue1> source, TParameter p);
        protected abstract IQueryable<TValue2> MapQuery(IQueryable<TValue1> query, TParameter parameter);
        protected abstract IEnumerable<DictionaryMutation<TKey2, TValue2>> MapMutations(
            IEnumerable<DictionaryMutation<TKey1, TValue1>> mutations, TParameter p);
        
        public virtual IQueryableDictionary<TKey2, TValue2> Transform(IQueryableDictionary<TKey1, TValue1> source,
            TParameter p)
        {
            return new AnonymousQueryableDictionary<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), MapQuery(source.Values, p));
        }

        public virtual ICachedDictionary<TKey2, TValue2> Transform(ICachedDictionary<TKey1, TValue1> source,
            TParameter p)
        {
            return new AnonymousCachedDictionary<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), 
                () => Transform(source.AsBypassCache(), p),
                () => Transform(source.AsNeverFlush(), p),
                source.FlushCache,
                clear => MapMutations(source.GetMutations(clear), p));
        }

        public virtual ICachedDisposableQueryableDictionary<TKey2, TValue2> Transform(
            ICachedDisposableQueryableDictionary<TKey1, TValue1> source, TParameter p)
        {
            return new AnonymousCachedDisposableQueryableDictionary<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), 
                () => Transform(source.AsBypassCache(), p),
                () => Transform(source.AsNeverFlush(), p),
                source.FlushCache,
                clear => MapMutations(source.GetMutations(clear), p),
                source, MapQuery(source.Values, p));
        }

        public virtual IDisposableQueryableDictionary<TKey2, TValue2> Transform(
            IDisposableQueryableDictionary<TKey1, TValue1> source, TParameter p)
        {
            return new AnonymousDisposableQueryableDictionary<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), 
                source, MapQuery(source.Values, p));
        }

        public virtual ICachedQueryableDictionary<TKey2, TValue2> Transform(
            ICachedQueryableDictionary<TKey1, TValue1> source, TParameter p)
        {
            return new AnonymousCachedQueryableDictionary<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), 
                () => Transform(source.AsBypassCache(), p),
                () => Transform(source.AsNeverFlush(), p),
                source.FlushCache,
                clear => MapMutations(source.GetMutations(clear), p),
                MapQuery(source.Values, p));
        }
        
        public virtual ICachedDisposableDictionary<TKey2, TValue2> Transform(
            ICachedDisposableDictionary<TKey1, TValue1> source, TParameter p)
        {
            return new AnonymousCachedDisposableDictionary<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), 
                () => Transform(source.AsBypassCache(), p),
                () => Transform(source.AsNeverFlush(), p),
                source.FlushCache,
                clear => MapMutations(source.GetMutations(clear), p),
                source);
        }

        public virtual IDisposableDictionary<TKey2, TValue2> Transform(IDisposableDictionary<TKey1, TValue1> source,
            TParameter p)
        {
            return new AnonymousDisposableDictionary<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), source);
        }

        public ICachedDictionaryWithBuiltInKey<TKey2, TValue2> Transform(ICachedDictionaryWithBuiltInKey<TKey1, TValue1> source, Tuple<TParameter, Func<TValue2, TKey2>> p)
        {
            return new CachedDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(Transform(source.AsCachedDictionary(), p.Item1), p.Item2);
        }

        public ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(ICachedDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Tuple<TParameter, Func<TValue2, TKey2>> p)
        {
            return new CachedDisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(Transform(source.AsCachedDisposableQueryableDictionary(), p.Item1), p.Item2);
        }

        public IDisposableQueryableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source,
            Tuple<TParameter, Func<TValue2, TKey2>> p)
        {
            return new DisposableQueryableDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(Transform(source.AsDisposableQueryableDictionary(), p.Item1), p.Item2);
        }

        public IQueryableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source, Tuple<TParameter, Func<TValue2, TKey2>> p)
        {
            return new QueryableDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(Transform(source.AsQueryableDictionary(), p.Item1), p.Item2);
        }

        public ICachedQueryableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(ICachedQueryableDictionaryWithBuiltInKey<TKey1, TValue1> source, Tuple<TParameter, Func<TValue2, TKey2>> p)
        {
            return new CachedQueryableDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(Transform(source.AsCachedQueryableDictionary(), p.Item1), p.Item2);
        }

        public ICachedDisposableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(ICachedDisposableDictionaryWithBuiltInKey<TKey1, TValue1> source, Tuple<TParameter, Func<TValue2, TKey2>> p)
        {
            return new CachedDisposableDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(Transform(source.AsCachedDisposableDictionary(), p.Item1), p.Item2);
        }

        public IDisposableDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDisposableDictionaryWithBuiltInKey<TKey1, TValue1> source, Tuple<TParameter, Func<TValue2, TKey2>> p)
        {
            return new DisposableDictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(Transform(source.AsDisposableDictionary(), p.Item1), p.Item2);
        }

        public IDictionaryWithBuiltInKey<TKey2, TValue2> Transform(IDictionaryWithBuiltInKey<TKey1, TValue1> source, Tuple<TParameter, Func<TValue2, TKey2>> p)
        {
            return new DictionaryWithBuiltInKeyAdapter<TKey2, TValue2>(Transform(source.AsComposableDictionary(), p.Item1), p.Item2);
        }
    }
}