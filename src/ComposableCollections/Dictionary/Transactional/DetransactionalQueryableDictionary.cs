using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Common;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Utilities;

namespace ComposableCollections.Dictionary.Transactional
{
    public class DetransactionalQueryableDictionary<TKey, TValue> : DetransactionalDictionary<TKey, TValue>, IQueryableDictionary<TKey, TValue>
    {
        private readonly IReadOnlyTransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>> _source;

        public DetransactionalQueryableDictionary(ITransactionalCollection<IDisposableQueryableReadOnlyDictionary<TKey, TValue>, IDisposableQueryableDictionary<TKey, TValue>> dictionary) : base(dictionary)
        {
            _source = dictionary;
        }

        public IQueryable<TValue> Values
        {
            get
            {
                var result = _source.BeginRead();
                return new Queryable<TValue>(result.Values, result);
            }
        }
    }
}