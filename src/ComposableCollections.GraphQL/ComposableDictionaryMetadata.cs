using System;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;

namespace ComposableCollections.GraphQL
{
    internal class ComposableDictionaryMetadata
    {
        public Type Type { get; init; }
        public Type KeyType { get; init; }
        public Type ValueType { get; init; }

        public bool HasBuiltInKey
        {
            get
            {
                return Type.IsAssignableTo(typeof(IReadOnlyDictionaryWithBuiltInKey<,>).MakeGenericType(KeyType, ValueType));
            }
        }

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
                return Type.IsAssignableTo(typeof(IQueryableReadOnlyDictionary<,>).MakeGenericType(KeyType, ValueType))
                    || Type.IsAssignableTo(typeof(IQueryableReadOnlyDictionaryWithBuiltInKey<,>).MakeGenericType(KeyType, ValueType));
            }
        }

        public Type MutationObjectType
        {
            get
            {
                if (HasBuiltInKey)
                {
                    return typeof(DictionaryWithBuiltInKeyObjectTypeMutation<,,>).MakeGenericType(Type, KeyType, ValueType);
                }
                else
                {
                    return typeof(ComposableDictionaryObjectTypeMutation<,,>).MakeGenericType(Type, KeyType, ValueType);
                }
            }
        }

        public Type QueryObjectType
        {
            get
            {
                if (IsQueryable)
                {
                    if (HasBuiltInKey)
                    {
                        return typeof(QueryableDictionaryWithBuiltInKeyObjectTypeQuery<,,>).MakeGenericType(Type, KeyType, ValueType);
                    }
                    else
                    {
                        return typeof(QueryableDictionaryObjectTypeQuery<,,>).MakeGenericType(Type, KeyType, ValueType);
                    }
                }
                else
                {
                    if (HasBuiltInKey)
                    {
                        return typeof(DictionaryWithBuiltInKeyObjectTypeQuery<,,>).MakeGenericType(Type, KeyType, ValueType);
                    }
                    else
                    {
                        return typeof(ComposableDictionaryObjectTypeQuery<,,>).MakeGenericType(Type, KeyType, ValueType);
                    }
                }
            }
        }
    }
}