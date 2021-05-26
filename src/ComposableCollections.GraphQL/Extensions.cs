using System;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using ComposableCollections.DictionaryWithBuiltInKey.Interfaces;
using HotChocolate;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Humanizer;
using LiveLinq.Dictionary;
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
            var interfaces = type.GetInterfaces().Concat(new[]{type})
                .SingleOrDefault(iface => GetGenericTypeDefinitionIfApplicable(iface) == typeof(IComposableReadOnlyDictionary<,>));

            if (interfaces == null)
            {
                interfaces = type.GetInterfaces().Concat(new[]{type})
                    .SingleOrDefault(iface => GetGenericTypeDefinitionIfApplicable(iface) == typeof(IReadOnlyDictionaryWithBuiltInKey<,>));
            }

            var types = interfaces.GetGenericArguments();

            return new ComposableDictionaryMetadata()
            {
                Type = type,
                KeyType = types[0],
                ValueType = types[1],
            };
        }

        public static IRequestExecutorBuilder AddTopLevelComposableDictionary<TComposableDictionary>(
            this IRequestExecutorBuilder builder, string name)
        {
            var metadata = typeof(TComposableDictionary).GetComposableDictionaryMetadata();

            builder = builder
                .AddComposableDictionary<TComposableDictionary>(name.Pascalize())
                .AddTypeExtension(new ObjectTypeExtension(descriptor =>
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
                .AddTypeExtension(new ObjectTypeExtension(descriptor =>
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

            if (metadata.IsObservable)
            {
                builder = builder.AddTypeExtension((ObjectTypeExtension) Activator.CreateInstance(metadata.SubscriptionObjectType,
                    "Subscription", name.Camelize()));
            }

            return builder;
        }

        public static ISchemaBuilder AddTopLevelComposableDictionary<TComposableDictionary>(
            this ISchemaBuilder builder, string name)
        {
            var metadata = typeof(TComposableDictionary).GetComposableDictionaryMetadata();

            builder = builder
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
                }));

            if (!metadata.IsReadOnly)
            {
                builder = builder.AddType(new ObjectTypeExtension(descriptor =>
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

            if (metadata.IsObservable)
            {
                builder = builder.AddType((ObjectTypeExtension) Activator.CreateInstance(metadata.SubscriptionObjectType,
                    "Subscription", name.Camelize()));
            }

            return builder;
        }

        public static IRequestExecutorBuilder AddComposableDictionary<TComposableDictionary>(this IRequestExecutorBuilder builder, string typeName)
        {
            var metadata = typeof(TComposableDictionary).GetComposableDictionaryMetadata();

            builder = builder
                .AddType((ObjectType)Activator.CreateInstance(metadata.QueryObjectType, typeName + "Query"))
                .AddType((ObjectType)Activator.CreateInstance(metadata.MutationObjectType, typeName + "Mutation"));

            if (metadata.IsObservable)
            {
                builder = builder.AddType((ObjectType)Activator.CreateInstance(metadata.SubscriptionType, typeName + "Change"));
            }
            
            return builder;
        }
        
        public static ISchemaBuilder AddComposableDictionary<TComposableDictionary>(this ISchemaBuilder builder, string typeName)
        {
            var metadata = typeof(TComposableDictionary).GetComposableDictionaryMetadata();

            builder = builder
                .AddType((ObjectType)Activator.CreateInstance(metadata.QueryObjectType, typeName + "Query"));

            if (metadata.MutationObjectType != null)
            {
                builder = builder.AddType(
                    (ObjectType) Activator.CreateInstance(metadata.MutationObjectType, typeName + "Mutation"));
            }

            if (metadata.IsObservable)
            {
                builder = builder.AddType((ObjectType)Activator.CreateInstance(metadata.SubscriptionType, typeName + "Change"));
            }
            
            return builder;
        }
    }
}