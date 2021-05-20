using ComposableCollections.Dictionary;
using HotChocolate.Types;

namespace ComposableCollections.GraphQL
{
	public class KeyValuePairType<TKey, TValue> : ObjectType<IKeyValue<TKey, TValue>> {
		private readonly string _typeName;

		public KeyValuePairType(string typeName)
		{
			_typeName = typeName;
		}

		protected override void Configure( IObjectTypeDescriptor<IKeyValue<TKey, TValue>> descriptor ) {
			descriptor.Name( $"{_typeName}KeyValuePair" );
		}
	}
}