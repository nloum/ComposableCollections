using System;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HotChocolate;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleMonads.HotChocolate
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
        
        internal static Type GetObjectTypeOf(this Type type)
        {
            if (type.GetGenericTypeDefinitionIfApplicable() == typeof(ObjectType<>))
            {
                return type.GetGenericArguments()[0];
            }

            if (type.BaseType == null)
            {
                return null;
            }
            
            return type.BaseType.GetObjectTypeOf();
        }

        public static IRequestExecutorBuilder AddEmptyQueryType(this IRequestExecutorBuilder builder)
        {
            return builder.AddType(new ObjectType(descriptor => descriptor.Name("Query")));
        }
        
        public static IRequestExecutorBuilder AddMaybe<T>(this IRequestExecutorBuilder builder)
        {
            builder = builder.AddType<MaybeType<T>>();
            return builder;
        }

        public static ISchemaBuilder AddMaybe<T>(this ISchemaBuilder builder)
        {
            var type = typeof(T).GetObjectTypeOf() ?? typeof(T);
            
            builder = builder.AddType(typeof(MaybeType<>).MakeGenericType(type));
            return builder;
        }

        public static IRequestExecutorBuilder AddEither<TEither>(this IRequestExecutorBuilder builder, string unionTypeName = null, string interfaceTypeName = null, bool includeInterface = true)
            where TEither : IEither
        {
            return builder.AddEither<TEither>(out var _, out var __, unionTypeName, interfaceTypeName, includeInterface);
        }
        
        public static IRequestExecutorBuilder AddEither<TEither>(this IRequestExecutorBuilder builder, out UnionType<TEither> unionType, out InterfaceType interfaceType, string unionTypeName = null, string interfaceTypeName = null, bool includeInterface = true)
            where TEither : IEither
        {
            var metadata = typeof(TEither).GetEitherMetadata();

            unionTypeName ??= typeof(TEither).Name;
            interfaceTypeName ??= (metadata.ConvertibleTo ?? metadata.SubTypesOf)?.Name;
            
            var parameters = new EitherObjectTypeParameters()
            {
                UnionTypeName = unionTypeName,
                InterfaceTypeName = interfaceTypeName,
            };

            unionType = new EitherUnion<TEither>(parameters);
            builder = builder.AddType(unionType);
            
            var baseType = metadata.ConvertibleTo ?? metadata.SubTypesOf;
            if (baseType != null && includeInterface)
            {
                interfaceType = (InterfaceType)Activator.CreateInstance(typeof(EitherInterface<>).MakeGenericType(baseType), parameters);
                builder = builder.AddType(interfaceType);
            }
            else
            {
                interfaceType = null;
            }

            return builder;
        }

        public static ISchemaBuilder AddEither<TEither>(this ISchemaBuilder builder, string unionTypeName = null, string interfaceTypeName = null, bool includeInterface = true)
            where TEither : IEither
        {
            return builder.AddEither<TEither>(out var _, out var __, unionTypeName, interfaceTypeName, includeInterface);
        }
        
        public static ISchemaBuilder AddEither<TEither>(this ISchemaBuilder builder, out UnionType<TEither> unionType, out InterfaceType interfaceType, string unionTypeName = null, string interfaceTypeName = null, bool includeInterface = true)
            where TEither : IEither
        {
            var metadata = typeof(TEither).GetEitherMetadata();

            unionTypeName ??= typeof(TEither).Name;
            interfaceTypeName ??= (metadata.ConvertibleTo ?? metadata.SubTypesOf)?.Name;
            
            var parameters = new EitherObjectTypeParameters()
            {
                UnionTypeName = unionTypeName,
                InterfaceTypeName = interfaceTypeName,
                IncludeInterface = includeInterface,
            };

            unionType = new EitherUnion<TEither>(parameters);
            builder = builder.AddType(unionType);
            
            var baseType = metadata.ConvertibleTo ?? metadata.SubTypesOf;
            if (baseType != null && includeInterface)
            {
                interfaceType = (InterfaceType)Activator.CreateInstance(typeof(EitherInterface<>).MakeGenericType(baseType), parameters);
                builder = builder.AddType(interfaceType);
            }
            else
            {
                interfaceType = null;
            }

            return builder;
        }
        
        public static IInputFieldDescriptor FieldFromPropertyInfo<T, TProperty>(IInputObjectTypeDescriptor<T> descriptor, PropertyInfo property)
        {
            var param = LambdaExpression.Parameter(property.DeclaringType);
            var lambda = LambdaExpression.Lambda<Func<T, TProperty>>(LambdaExpression.MakeMemberAccess(param, property), param);
            var result = descriptor.Field(lambda);
            return result;
        }
        
        public static IInputFieldDescriptor Field<T>(this IInputObjectTypeDescriptor<T> descriptor,
            PropertyInfo property)
        {
            var method = typeof(Extensions).GetMethod("FieldFromPropertyInfo").MakeGenericMethod(typeof(T), property.PropertyType);
            var result = method.Invoke(null, new object[] {descriptor, property});
            return (IInputFieldDescriptor) result;
        }

        public static IRequestExecutorBuilder AddEitherInputType<TEither>(this IRequestExecutorBuilder builder, string typeName = null, bool useTypesAsNames = true) where TEither : IEither
        {
            return builder.AddType(new InputObjectType<TEither>(descriptor =>
            {
                var type = typeof(TEither);
                descriptor.Name(typeName ?? (type.Name + "Input"));
                descriptor.BindFieldsExplicitly();
                descriptor.Directive(new OneFieldDirective());
                //descriptor.Field(type.GetProperty("Value")).Ignore();
                var metadata = type.GetEitherMetadata();
                var types = metadata.Types;
                for (var i = 0; i < types.Count; i++)
                {
                    if (useTypesAsNames)
                    {
                        descriptor.Field(type.GetProperty($"Item{i + 1}")).Name(types[i].Name);
                    }
                    else
                    {
                        descriptor.Field(type.GetProperty($"Item{i + 1}"));
                    }
                }
            }));
        }
        
        public static ISchemaBuilder AddEitherInputType<TEither>(this ISchemaBuilder builder, string typeName = null, bool useTypesAsNames = true) where TEither : IEither
        {
            return builder.AddType(new InputObjectType<TEither>(descriptor =>
            {
                var type = typeof(TEither);
                descriptor.Name(typeName ?? (type.Name + "Input"));
                descriptor.BindFieldsExplicitly();
                descriptor.Directive(new OneFieldDirective());
                //descriptor.Field(type.GetProperty("Value")).Ignore();
                var metadata = type.GetEitherMetadata();
                var types = metadata.Types;
                for (var i = 0; i < types.Count; i++)
                {
                    if (useTypesAsNames)
                    {
                        descriptor.Field(type.GetProperty($"Item{i + 1}")).Name(types[i].Name);
                    }
                    else
                    {
                        descriptor.Field(type.GetProperty($"Item{i + 1}"));
                    }
                }
            }));
        }
    }
}