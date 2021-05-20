using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using HotChocolate.Types;

namespace ComposableCollections.GraphQL
{
    public class QueryableDictionaryObjectTypeQuery<TComposableDictionary, TKey, TValue> : ComposableDictionaryObjectTypeQuery<TComposableDictionary, TKey, TValue> where TComposableDictionary : IQueryableReadOnlyDictionary<TKey, TValue>
    {
        public QueryableDictionaryObjectTypeQuery(string typeName) : base(typeName, false)
        {
        }

        protected override void Configure(IObjectTypeDescriptor<TComposableDictionary> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field("values").Type(typeof(IQueryable<TValue>)).UseFiltering()
                .Resolve(rc => rc.Parent<TComposableDictionary>().Values);
        }
    }
}