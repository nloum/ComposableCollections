using System;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using HotChocolate;
using HotChocolate.Types;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;

namespace ComposableCollections.GraphQL
{
    public static class Extensions
    {
        internal static Type GetGenericTypeDefinitionIfApplicable(this Type type)
        {
            if (type.IsConstructedGenericType)
            {
                var result = type.GetGenericTypeDefinition();
                return result;
            }

            return type;
        }

        internal static ComposableDictionaryMetadata GetComposableDictionaryMetadata(this Type type)
        {
            var interfaces = type.GetInterfaces()
                .SingleOrDefault(iface => GetGenericTypeDefinitionIfApplicable(iface) == typeof(IComposableReadOnlyDictionary<,>));

            var types = interfaces.GetGenericArguments();

            return new ComposableDictionaryMetadata()
            {
                Type = type,
                KeyType = types[0],
                ValueType = types[1],
            };
        }

        public static ISchemaBuilder AddTopLevelComposableDictionary<TComposableDictionary>(
            this ISchemaBuilder builder, string name)
        {
            var metadata = typeof(TComposableDictionary).GetComposableDictionaryMetadata();

            return builder
                .AddComposableDictionary<TComposableDictionary>(name.Pascalize())
                .AddType(new ObjectTypeExtension(descriptor =>
                {
                    descriptor.Name("Query");
                    descriptor.Field(name.Camelize())
                        .Type(metadata.QueryObjectType)
                        .Resolve(rc =>
                        {
                            var result = rc.Services.GetRequiredService<TComposableDictionary>();
                            return result;
                        });
                }))
                .AddType(new ObjectTypeExtension(descriptor =>
                {
                    descriptor.Name("Mutation");
                    descriptor.Field(name.Camelize())
                        .Type(metadata.MutationObjectType)
                        .Resolve(rc =>
                        {
                            var result = rc.Services.GetRequiredService<TComposableDictionary>();
                            return result;
                        });
                }));
        }

        public static ISchemaBuilder AddComposableDictionary<TComposableDictionary>(this ISchemaBuilder builder, string typeName)
        {
            var metadata = typeof(TComposableDictionary).GetComposableDictionaryMetadata();

            return builder
                .AddType((ObjectType)Activator.CreateInstance(metadata.QueryObjectType, typeName + "Query"))
                .AddType((ObjectType)Activator.CreateInstance(metadata.MutationObjectType, typeName + "Mutation"));
        }
    }
}