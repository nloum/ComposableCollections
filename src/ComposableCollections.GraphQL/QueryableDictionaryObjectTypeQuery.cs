using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using HotChocolate.Types;
using Humanizer;

namespace ComposableCollections.GraphQL
{
    public class QueryableDictionaryObjectTypeQuery<TComposableDictionary, TKey, TValue> : ComposableDictionaryObjectTypeQuery<TComposableDictionary, TKey, TValue> where TComposableDictionary : IQueryableReadOnlyDictionary<TKey, TValue>
    {
        private readonly ComposableDictionaryObjectTypeParameters _parameters;

        public QueryableDictionaryObjectTypeQuery(ComposableDictionaryObjectTypeParameters parameters) : base(parameters, false)
        {
            _parameters = parameters;
        }

        protected override void Configure(IObjectTypeDescriptor<TComposableDictionary> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Field(_parameters.ValueNamePlural.Camelize()).Type(typeof(IQueryable<TValue>)).UseFiltering()
                .Resolve(rc => rc.Parent<TComposableDictionary>().Values);
        }
    }
}