using System;
using System.Linq;
using System.Linq.Expressions;
using ComposableCollections.Set.Base;

namespace ComposableCollections.Set.Adapters
{
    public class QueryableReadOnlySetAdapter<TValue> : DelegateReadOnlySet<TValue>, IQueryableReadOnlySet<TValue>
    {
        private readonly IQueryable<TValue> _queryable;

        public QueryableReadOnlySetAdapter(IReadOnlySet<TValue> state, IQueryable<TValue> queryable) : base(state)
        {
            _queryable = queryable;
        }

        public Type ElementType => _queryable.ElementType;
        public Expression Expression => _queryable.Expression;
        public IQueryProvider Provider => _queryable.Provider;
    }
}