using System;
using ComposableCollections.Dictionary.Interfaces;

namespace ComposableCollections.GraphQL
{
    internal class ComposableDictionaryMetadata
    {
        public Type Type { get; init; }
        public Type KeyType { get; init; }
        public Type ValueType { get; init; }

        public bool IsReadOnly
        {
            get
            {
                return !Type.IsAssignableTo(typeof(IComposableDictionary<,>).MakeGenericType(KeyType, ValueType));
            }
        }

        public bool IsQueryable
        {
            get
            {
                return Type.IsAssignableTo(typeof(IQueryableReadOnlyDictionary<,>).MakeGenericType(KeyType, ValueType));
            }
        }

        public Type MutationObjectType
        {
            get
            {
                return typeof(ComposableDictionaryObjectTypeMutation<,,>).MakeGenericType(Type, KeyType, ValueType);
            }
        }

        public Type QueryObjectType
        {
            get
            {
                if (!IsQueryable)
                {
                    return typeof(ComposableDictionaryObjectTypeQuery<,,>).MakeGenericType(Type, KeyType, ValueType);
                }
                else
                {
                    return typeof(QueryableDictionaryObjectTypeQuery<,,>).MakeGenericType(Type, KeyType, ValueType);
                }
            }
        }
    }
}