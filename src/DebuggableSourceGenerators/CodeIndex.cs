using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using ComposableCollections;
using ComposableCollections.Dictionary;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.Dictionary.Sources;
using SimpleMonads;

namespace DebuggableSourceGenerators
{
    public class CodeIndex : IEnumerable<IKeyValue<TypeIdentifier, Type>>
    {
        private readonly IComposableReadOnlyDictionary<TypeIdentifier, Type> _types;

        public CodeIndex(IComposableDictionary<TypeIdentifier, Lazy<Type>> lazyTypes)
        {
            _types = lazyTypes.WithMapping(x => x.Value);
            
            // var types = new ComposableDictionary<TypeIdentifier, Type>();
            // foreach (var lazyType in lazyTypes.Values.ToImmutableList())
            // {
            //     var type = lazyType.Value;
            //     types[type.Identifier] = type;
            // }
            //
            // foreach (var lazyType in lazyTypes.Values.ToImmutableList())
            // {
            //     var type = lazyType.Value;
            //     if (type is Class clazz)
            //     {
            //         foreach (var constructor in clazz.Constructors)
            //         {
            //             foreach (var parameter in constructor.Parameters)
            //             {
            //                 var _ = parameter.Type.Value;
            //             }
            //         }
            //         
            //         foreach (var field in clazz.Fields)
            //         {
            //             var _ = field.Type.Value;
            //         }
            //     }
            //
            //     if (type is StructuredType structuredType)
            //     {
            //         foreach (var indexer in structuredType.Indexers)
            //         {
            //             foreach (var parameter in indexer.Parameters)
            //             {
            //                 var _ = parameter.Type.Value;
            //             }
            //
            //             var __ = indexer.ReturnType.Value;
            //         }
            //         
            //         foreach (var iface in structuredType.Interfaces)
            //         {
            //             var _ = iface.Value;
            //         }
            //         
            //         foreach (var method in structuredType.Methods)
            //         {
            //             foreach (var parameter in method.Parameters)
            //             {
            //                 var _ = parameter.Type.Value;
            //             }
            //
            //             var __ = method.ReturnType.Value;
            //         }
            //         
            //         foreach (var property in structuredType.Properties)
            //         {
            //             var __ = property.Type.Value;
            //         }
            //         
            //         foreach (var typeArgument in structuredType.TypeArguments)
            //         {
            //             var _ = typeArgument.Value;
            //         }
            //
            //         foreach (var typeArgument in structuredType.TypeParameters)
            //         {
            //             foreach (var mustBeAssignableTo in typeArgument.Value.MustBeAssignedTo)
            //             {
            //                 var _ = mustBeAssignableTo.Value;
            //             }
            //         }
            //     }
            // }
            //
            // foreach (var lazyType in lazyTypes.Values.ToImmutableList())
            // {
            //     var type = lazyType.Value;
            //     types[type.Identifier] = type;
            // }
        }

        public IEnumerator<IKeyValue<TypeIdentifier, Type>> GetEnumerator()
        {
            return _types.GetEnumerator();
        }

        public int Count => _types.Count;

        public Type GetValue(TypeIdentifier key)
        {
            return _types.GetValue(key);
        }

        public bool ContainsKey(TypeIdentifier key)
        {
            return _types.ContainsKey(key);
        }

        public IMaybe<Type> TryGetValue(TypeIdentifier key)
        {
            return _types.TryGetValue(key);
        }

        public IEnumerable<TypeIdentifier> Keys => _types.Keys;

        public IEnumerable<Type> Values => _types.Values;

        public bool TryGetValue(TypeIdentifier key, out Type value)
        {
            var maybe = _types.TryGetValue(key);
            if (maybe.HasValue)
            {
                value = maybe.Value;
                return true;
            }

            value = default;
            return false;
        }

        public Type this[TypeIdentifier key]
        {
            get => _types[key];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}