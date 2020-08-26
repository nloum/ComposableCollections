using System;
using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Adapters;
using ComposableCollections.Dictionary.ExtensionMethodHelpers.Interfaces;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.WithBuiltInKey;
using ComposableCollections.Dictionary.WithBuiltInKey.Interfaces;
using ComposableCollections.Dictionary.Write;

namespace ComposableCollections.Dictionary.ExtensionMethodHelpers.BaseClasses
{
    public class ComposableDictionaryTransformations<TKey, TValue, TParameter> : IComposableDictionaryTransformations<TKey, TValue, TKey, TValue, TParameter>, IDictionaryWithBuiltInKeyTransformations<TKey, TValue, TKey, TValue, TParameter>
    {
        private readonly Func<IComposableDictionary<TKey, TValue>, TParameter, IComposableDictionary<TKey, TValue>>
            _transform;

        public ComposableDictionaryTransformations(Func<IComposableDictionary<TKey, TValue>, TParameter, IComposableDictionary<TKey, TValue>> transform)
        {
            _transform = transform;
        }

        public IComposableDictionary<TKey, TValue> Transform(IComposableDictionary<TKey, TValue> source, TParameter p)
        {
            return _transform(source, p);
        }

        public ICachedDictionary<TKey, TValue> Transform(ICachedDictionary<TKey, TValue> source, TParameter p)
        {
            return new CachedDictionaryAdapter<TKey, TValue>(
                Transform((IComposableDictionary<TKey, TValue>)source, p), source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites);
        }

        public IQueryableDictionary<TKey, TValue> Transform(IQueryableDictionary<TKey, TValue> source, TParameter p)
        {
            return new QueryableDictionaryAdapter<TKey, TValue>(Transform((IComposableDictionary<TKey, TValue>)source, p), source.Values);
        }

        public ICachedDisposableQueryableDictionary<TKey, TValue> Transform(ICachedDisposableQueryableDictionary<TKey, TValue> source, TParameter p)
        {
            return new CachedDisposableQueryableDictionaryAdapter<TKey, TValue>(Transform((IComposableDictionary<TKey, TValue>)source, p), source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source, source.Values);
        }

        public IDisposableQueryableDictionary<TKey, TValue> Transform(IDisposableQueryableDictionary<TKey, TValue> source, TParameter p)
        {
            return new DisposableQueryableDictionaryAdapter<TKey, TValue>(
                Transform((IComposableDictionary<TKey, TValue>) source, p), source, source.Values);
        }

        public ICachedQueryableDictionary<TKey, TValue> Transform(ICachedQueryableDictionary<TKey, TValue> source, TParameter p)
        {
            return new CachedQueryableDictionaryAdapter<TKey, TValue>(
                Transform((IComposableDictionary<TKey, TValue>) source, p), source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source.Values);
        }

        public ICachedDisposableDictionary<TKey, TValue> Transform(ICachedDisposableDictionary<TKey, TValue> source, TParameter p)
        {
            return new CachedDisposableDictionaryAdapter<TKey, TValue>(Transform((IComposableDictionary<TKey, TValue>)source, p), source.AsBypassCache, source.AsNeverFlush, source.FlushCache, source.GetWrites, source);
        }

        public IDisposableDictionary<TKey, TValue> Transform(IDisposableDictionary<TKey, TValue> source, TParameter p)
        {
            return new DisposableDictionaryAdapter<TKey, TValue>(Transform((IComposableDictionary<TKey, TValue>)source, p), source);
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
    
    public class ComposableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter> :
        IComposableDictionaryTransformations<TKey1, TValue1, TKey2, TValue2, TParameter>,
        IDictionaryWithBuiltInKeyTransformations<TKey1, TValue1, TKey2, TValue2, Tuple<TParameter, Func<TValue2, TKey2>>>
    {
        private readonly Func<IComposableDictionary<TKey1, TValue1>, TParameter, IComposableDictionary<TKey2, TValue2>> _transform1;
        private readonly Func<IComposableReadOnlyDictionary<TKey1, TValue1>, TParameter, IComposableReadOnlyDictionary<TKey2, TValue2>> _transform2;
        private readonly Func<IQueryable<TValue1>, TParameter, IQueryable<TValue2>> _mapQuery;
        private readonly Func<IEnumerable<DictionaryWrite<TKey1, TValue1>>, TParameter, IEnumerable<DictionaryWrite<TKey2, TValue2>>> _mapWrites;

        public ComposableDictionaryTransformations(Func<IComposableDictionary<TKey1, TValue1>, TParameter, IComposableDictionary<TKey2, TValue2>> transform1, Func<IComposableReadOnlyDictionary<TKey1, TValue1>, TParameter, IComposableReadOnlyDictionary<TKey2, TValue2>> transform2, Func<IQueryable<TValue1>, TParameter, IQueryable<TValue2>> mapQuery, Func<IEnumerable<DictionaryWrite<TKey1, TValue1>>, TParameter, IEnumerable<DictionaryWrite<TKey2, TValue2>>> mapWrites)
        {
            _transform1 = transform1;
            _transform2 = transform2;
            _mapQuery = mapQuery;
            _mapWrites = mapWrites;
        }

        public IComposableDictionary<TKey2, TValue2> Transform(IComposableDictionary<TKey1, TValue1> source,
            TParameter p)
        {
            return _transform1(source, p);
        }

        protected IComposableReadOnlyDictionary<TKey2, TValue2> Transform(
            IComposableReadOnlyDictionary<TKey1, TValue1> source, TParameter p)
        {
            return _transform2(source, p);
        }

        protected IQueryable<TValue2> MapQuery(IQueryable<TValue1> query, TParameter parameter)
        {
            return _mapQuery(query, parameter);
        }

        protected IEnumerable<DictionaryWrite<TKey2, TValue2>> MapWrites(
            IEnumerable<DictionaryWrite<TKey1, TValue1>> writes, TParameter p)
        {
            return _mapWrites(writes, p);
        }
        
        public virtual IQueryableDictionary<TKey2, TValue2> Transform(IQueryableDictionary<TKey1, TValue1> source,
            TParameter p)
        {
            return new QueryableDictionaryAdapter<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), MapQuery(source.Values, p));
        }

        public virtual ICachedDictionary<TKey2, TValue2> Transform(ICachedDictionary<TKey1, TValue1> source,
            TParameter p)
        {
            return new CachedDictionaryAdapter<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), 
                () => Transform(source.AsBypassCache(), p),
                () => Transform(source.AsNeverFlush(), p),
                source.FlushCache,
                clear => MapWrites(source.GetWrites(clear), p));
        }

        public virtual ICachedDisposableQueryableDictionary<TKey2, TValue2> Transform(
            ICachedDisposableQueryableDictionary<TKey1, TValue1> source, TParameter p)
        {
            return new CachedDisposableQueryableDictionaryAdapter<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), 
                () => Transform(source.AsBypassCache(), p),
                () => Transform(source.AsNeverFlush(), p),
                source.FlushCache,
                clear => MapWrites(source.GetWrites(clear), p),
                source, MapQuery(source.Values, p));
        }

        public virtual IDisposableQueryableDictionary<TKey2, TValue2> Transform(
            IDisposableQueryableDictionary<TKey1, TValue1> source, TParameter p)
        {
            return new DisposableQueryableDictionaryAdapter<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), 
                source, MapQuery(source.Values, p));
        }

        public virtual ICachedQueryableDictionary<TKey2, TValue2> Transform(
            ICachedQueryableDictionary<TKey1, TValue1> source, TParameter p)
        {
            return new CachedQueryableDictionaryAdapter<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), 
                () => Transform(source.AsBypassCache(), p),
                () => Transform(source.AsNeverFlush(), p),
                source.FlushCache,
                clear => MapWrites(source.GetWrites(clear), p),
                MapQuery(source.Values, p));
        }
        
        public virtual ICachedDisposableDictionary<TKey2, TValue2> Transform(
            ICachedDisposableDictionary<TKey1, TValue1> source, TParameter p)
        {
            return new CachedDisposableDictionaryAdapter<TKey2, TValue2>(
                Transform((IComposableDictionary<TKey1, TValue1>) source, p), 
                () => Transform(source.AsBypassCache(), p),
                () => Transform(source.AsNeverFlush(), p),
                source.FlushCache,
                clear => MapWrites(source.GetWrites(clear), p),
                source);
        }

        public virtual IDisposableDictionary<TKey2, TValue2> Transform(IDisposableDictionary<TKey1, TValue1> source,
            TParameter p)
        {
            return new DisposableDictionaryAdapter<TKey2, TValue2>(
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