using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Dictionary.Interfaces;
using SimpleMonads;

namespace ComposableCollections.Dictionary.Adapters
{
    public class QueryableToQueryableReadOnlyDictionaryAdapter<TKey, TValue> : IQueryableReadOnlyDictionary<TKey, TValue>
    {
        private readonly IQueryable<TValue> _queryable;
        private readonly Expression<Func<TValue, TKey>> _getKey;
        private readonly Func<TKey, Expression<Func<TValue, bool>>> _compareKey;
        private readonly Expression<Func<TValue, IKeyValue<TKey, TValue>>> _getKeyValue;

        public QueryableToQueryableReadOnlyDictionaryAdapter(IQueryable<TValue> queryable, Expression<Func<TValue, TKey>> getKey)
        {
            _queryable = queryable;
            _getKey = getKey;
            
            var memberExpression = getKey.Body as MemberExpression;
            _compareKey = key =>
            {
                var parameter = Expression.Parameter(typeof(TValue), "p1");
                var equality = Expression.Equal(Expression.MakeMemberAccess(parameter, memberExpression.Member), Expression.Constant(key, typeof(TKey)));
                var result = Expression.Lambda<Func<TValue, bool>>(equality, parameter);
                return result;
            };

            var valueParameter = Expression.Parameter(typeof(TValue), "p1");
            var body = Expression.New(typeof(KeyValue<TKey, TValue>).GetConstructor(new[] {typeof(TKey), typeof(TValue)}),
                Expression.MakeMemberAccess(valueParameter, memberExpression.Member),
                valueParameter);
            _getKeyValue = Expression.Lambda<Func<TValue, IKeyValue<TKey, TValue>>>(body, valueParameter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IKeyValue<TKey, TValue>> GetEnumerator()
        {
            return _queryable.Select(_getKeyValue).AsEnumerable().GetEnumerator();
        }

        public int Count => _queryable.Count();
        public IEqualityComparer<TKey> Comparer => EqualityComparer<TKey>.Default;

        public TValue this[TKey key] => _queryable.Where(_compareKey(key)).Single();

        public IEnumerable<TKey> Keys => _queryable.Select(_getKey);

        IEnumerable<TValue> IComposableReadOnlyDictionary<TKey, TValue>.Values => _queryable;

        public bool ContainsKey(TKey key)
        {
            return _queryable.Where(_compareKey(key)).FirstOrDefault() != null;
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            var result = _queryable.Where(_compareKey(key)).FirstOrDefault();
            if (result == null)
            {
                return Maybe<TValue>.Nothing();
            }

            return result.ToMaybe();
        }

        IQueryable<TValue> IQueryableReadOnlyDictionary<TKey, TValue>.Values => _queryable;
    }
}