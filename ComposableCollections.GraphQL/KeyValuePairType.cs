using ComposableCollections.Dictionary;
using HotChocolate.Types;
using Humanizer;

namespace ComposableCollections.GraphQL
{
	public class KeyValuePairType<TKey, TValue> : ObjectType<IKeyValue<TKey, TValue>> {
		private readonly ComposableDictionaryObjectTypeParameters _parameters;

		public KeyValuePairType(ComposableDictionaryObjectTypeParameters parameters)
		{
			_parameters = parameters;
		}

		protected override void Configure( IObjectTypeDescriptor<IKeyValue<TKey, TValue>> descriptor ) {
			descriptor.Name( $"{_parameters.KeyNameSingular.Pascalize()}{_parameters.ValueNameSingular.Pascalize()}Pair" );
			descriptor.Field(x => x.Key).Name(_parameters.KeyNameSingular.Camelize());
			descriptor.Field(x => x.Value).Name(_parameters.ValueNameSingular.Camelize());
		}
	}
}