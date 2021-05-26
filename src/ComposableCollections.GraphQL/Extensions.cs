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
            return builder.AddTopLevelComposableDictionary<TComposableDictionary>(new ComposableDictionaryObjectTypeParameters()
            {
                CollectionName = name,
                ValueNameSingular = "value",
                ValueNamePlural = "values",
                KeyNameSingular = "value",
                KeyNamePlural = "values",
            });
        }

        public static IRequestExecutorBuilder AddTopLevelComposableDictionary<TComposableDictionary>(
            this IRequestExecutorBuilder builder, ComposableDictionaryObjectTypeParameters parameters)
        {
            var metadata = typeof(TComposableDictionary).GetComposableDictionaryMetadata();

            builder = builder
                .AddComposableDictionary<TComposableDictionary>(parameters)
                .AddTypeExtension(new ObjectTypeExtension(descriptor =>
                {
                    descriptor.Name("Query");
                    descriptor.Field(parameters.CollectionName.Camelize())
                        .Type(metadata.QueryObjectType)
                        .Resolve(rc =>
                        {
                            var result = rc.Services.GetRequiredService<TComposableDictionary>();
                            return result;
                        });
                }));

            if (!metadata.IsReadOnly)
            {
                builder = builder.AddTypeExtension(new ObjectTypeExtension(descriptor =>
                {
                    descriptor.Name("Mutation");
                    descriptor.Field(parameters.CollectionName.Camelize())
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
                builder = builder.AddTypeExtension((ObjectTypeExtension) Activator.CreateInstance(metadata.SubscriptionObjectType, parameters));
            }

            return builder;
        }

        public static ISchemaBuilder AddTopLevelComposableDictionary<TComposableDictionary>(
            this ISchemaBuilder builder, string name)
        {
            return builder.AddTopLevelComposableDictionary<TComposableDictionary>(new ComposableDictionaryObjectTypeParameters()
            {
                CollectionName = name,
                ValueNameSingular = "value",
                ValueNamePlural = "values",
                KeyNameSingular = "value",
                KeyNamePlural = "values",
            });
        }

        public static ISchemaBuilder AddTopLevelComposableDictionary<TComposableDictionary>(
            this ISchemaBuilder builder, ComposableDictionaryObjectTypeParameters parameters)
        {
            var metadata = typeof(TComposableDictionary).GetComposableDictionaryMetadata();

            builder = builder
                .AddComposableDictionary<TComposableDictionary>(parameters)
                .AddType(new ObjectTypeExtension(descriptor =>
                {
                    descriptor.Name("Query");
                    descriptor.Field(parameters.CollectionName.Camelize())
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
                    descriptor.Field(parameters.CollectionName.Camelize())
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
                    parameters));
            }

            return builder;
        }

        public static IRequestExecutorBuilder AddComposableDictionary<TComposableDictionary>(
            this IRequestExecutorBuilder builder, string name)
        {
            return builder.AddComposableDictionary<TComposableDictionary>(new ComposableDictionaryObjectTypeParameters()
            {
                CollectionName = name,
                ValueNameSingular = "value",
                ValueNamePlural = "values",
                KeyNameSingular = "value",
                KeyNamePlural = "values",
            });
        }

        public static IRequestExecutorBuilder AddComposableDictionary<TComposableDictionary>(this IRequestExecutorBuilder builder, ComposableDictionaryObjectTypeParameters parameters)
        {
            var metadata = typeof(TComposableDictionary).GetComposableDictionaryMetadata();

            builder = builder.AddType((ObjectType)Activator.CreateInstance(metadata.QueryObjectType, parameters));

            if (!metadata.IsReadOnly)
            {
                builder = builder.AddType(
                    (ObjectType) Activator.CreateInstance(metadata.MutationObjectType, parameters));
            }

            if (metadata.IsObservable)
            {
                builder = builder.AddType((ObjectType)Activator.CreateInstance(metadata.SubscriptionType, parameters));
            }
            
            return builder;
        }

        public static ISchemaBuilder AddComposableDictionary<TComposableDictionary>(this ISchemaBuilder builder,
            string name)
        {
            return builder.AddComposableDictionary<TComposableDictionary>(new ComposableDictionaryObjectTypeParameters()
            {
                CollectionName = name,
                ValueNameSingular = "value",
                ValueNamePlural = "values",
                KeyNameSingular = "value",
                KeyNamePlural = "values",
            });
        }

        public static ISchemaBuilder AddComposableDictionary<TComposableDictionary>(this ISchemaBuilder builder, ComposableDictionaryObjectTypeParameters parameters)
        {
            var metadata = typeof(TComposableDictionary).GetComposableDictionaryMetadata();

            builder = builder
                .AddType((ObjectType)Activator.CreateInstance(metadata.QueryObjectType, parameters));

            if (metadata.MutationObjectType != null)
            {
                builder = builder.AddType(
                    (ObjectType) Activator.CreateInstance(metadata.MutationObjectType, parameters));
            }

            if (metadata.IsObservable)
            {
                builder = builder.AddType((ObjectType)Activator.CreateInstance(metadata.SubscriptionType, parameters));
            }
            
            return builder;
        }
    }
}