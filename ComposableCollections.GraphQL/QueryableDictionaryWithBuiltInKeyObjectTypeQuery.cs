using System.Linq;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using HotChocolate.Types;
using Humanizer;

namespace ComposableCollections.GraphQL
{
    public class QueryableDictionaryWithBuiltInKeyObjectTypeQuery<TDictionaryWithBuiltInKey, TKey, TValue> : DictionaryWithBuiltInKeyObjectTypeQuery<TDictionaryWithBuiltInKey, TKey, TValue> where TDictionaryWithBuiltInKey : IQueryableReadOnlyDictionaryWithBuiltInKey<TKey, TValue>
    {
        private readonly ComposableDictionaryObjectTypeParameters _parameters;

        public QueryableDictionaryWithBuiltInKeyObjectTypeQuery(ComposableDictionaryObjectTypeParameters parameters) : base(parameters, false)
        {
            _parameters = parameters;
        }

        protected override void Configure(IObjectTypeDescriptor<TDictionaryWithBuiltInKey> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field(_parameters.ValueNamePlural.Camelize()).Type(typeof(IQueryable<TValue>)).UseFiltering()
                .Resolve(rc => rc.Parent<TDictionaryWithBuiltInKey>().Values);
        }
    }
}