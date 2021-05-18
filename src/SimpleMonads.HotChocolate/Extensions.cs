using System;
using System.Collections.Immutable;
using System.Linq;
using HotChocolate;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleMonads.HotChocolate
{
    public static class Extensions
    {
        public static IRequestExecutorBuilder AddEmptyQueryType(this IRequestExecutorBuilder builder)
        {
            return builder.AddType(new ObjectType(descriptor =>
            {
                descriptor.Name("Query");
                descriptor.Field("hello").Type<StringType>().Resolve(rc => "world!");
            }));
        }

        public static IRequestExecutorBuilder AddEither<TEither>(this IRequestExecutorBuilder builder)
            where TEither : IEither
        {
            builder = builder.AddType<EitherUnion<TEither>>();
            
            var metadata = typeof(TEither).GetEitherMetadata();

            var baseType = metadata.ConvertibleTo ?? metadata.SubTypesOf;
            if (baseType != null)
            {
                var interfaceType = (InterfaceType)Activator.CreateInstance(typeof(EitherInterface<>).MakeGenericType(baseType));
                builder = builder.AddType(interfaceType);
            }

            return builder;
        }
        
        public static ISchemaBuilder AddEither<TEither>(this ISchemaBuilder builder)
            where TEither : IEither
        {
            builder = builder.AddType<EitherUnion<TEither>>();
            
            var metadata = typeof(TEither).GetEitherMetadata();

            var baseType = metadata.ConvertibleTo ?? metadata.SubTypesOf;
            if (baseType != null)
            {
                var interfaceType = (InterfaceType)Activator.CreateInstance(typeof(EitherInterface<>).MakeGenericType(baseType));
                builder = builder.AddType(interfaceType);
            }

            return builder;
        }

        internal static Type GetGenericTypeDefinitionIfApplicable(this Type type)
        {
            if (type.IsConstructedGenericType)
            {
                var result = type.GetGenericTypeDefinition();
                return result;
            }

            return type;
        }
        
        internal static EitherMetadata GetEitherMetadata(this Type eitherType)
        {
            var interfaces = eitherType.GetInterfaces();
            var eitherInterfaces = interfaces
                .OrderBy(iface =>
                {
                    if (iface.DeclaringType != null)
                    {
                        if (iface.DeclaringType.GetGenericTypeDefinitionIfApplicable() == typeof(SubTypesOf<>))
                        {
                            return 0;
                        }

                        if (iface.DeclaringType.GetGenericTypeDefinitionIfApplicable() == typeof(ConvertibleTo<>))
                        {
                            return 1;
                        }
                    }

                    return 2;
                })
                .ThenByDescending(iface => iface.GenericTypeArguments.Length)
                .ToImmutableList();

            var iface = eitherInterfaces.First();

            Type castedTo = null;
            Type subTypeOf = null;

            if (iface.DeclaringType != null)
            {
                if (iface.DeclaringType.GetGenericTypeDefinitionIfApplicable() == typeof(SubTypesOf<>))
                {
                    subTypeOf = iface.GenericTypeArguments[0];
                }
                else if (iface.DeclaringType.GetGenericTypeDefinitionIfApplicable() == typeof(ConvertibleTo<>))
                {
                    castedTo = iface.GenericTypeArguments[0];
                }
                else throw new InvalidOperationException();
            }

            return new EitherMetadata()
            {
                SubTypesOf = subTypeOf,
                ConvertibleTo = castedTo,
                Types = iface.GenericTypeArguments.Skip(subTypeOf == null && castedTo == null ? 0 : 1).ToArray(),
            };
        }
    }
}