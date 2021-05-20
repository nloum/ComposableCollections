using System.Linq;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using HotChocolate.Types;

namespace ComposableCollections.GraphQL
{
    public class QueryableDictionaryWithBuiltInKeyObjectTypeQuery<TDictionaryWithBuiltInKey, TKey, TValue> : DictionaryWithBuiltInKeyObjectTypeQuery<TDictionaryWithBuiltInKey, TKey, TValue> where TDictionaryWithBuiltInKey : IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        public QueryableDictionaryWithBuiltInKeyObjectTypeQuery(string typeName) : base(typeName, false)
        {
        }

        protected override void Configure(IObjectTypeDescriptor<TDictionaryWithBuiltInKey> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field("values").Type(typeof(IQueryable<TValue>)).UseFiltering()
                .Resolve(rc => rc.Parent<TDictionaryWithBuiltInKey>().Values);
        }
    }
}