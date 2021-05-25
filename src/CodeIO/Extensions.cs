using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CodeIO.LoadedTypes.Read;
using ComposableCollections;

namespace CodeIO
{
    public static class Extensions
    {
        public static TypeReader AddReflection(this TypeReader typeReader)
        {
            return typeReader.AddTypeFormat<Type>(type => type.GetTypeIdentifier(), (type, lazyTypes) =>
            {
                if (type.IsPrimitive)
                {
                    return new Lazy<IType>(new Primitive()
                    {
                        Identifier = type.GetTypeIdentifier(),
                        Type = type.Name switch
                        {
                            "Int32" => PrimitiveType.Int,
                            "UInt32" => PrimitiveType.UInt,
                            "Int16" => PrimitiveType.Short,
                            "UInt16" => PrimitiveType.UShort,
                            "Int64" => PrimitiveType.Long,
                            "UInt64" => PrimitiveType.ULong,
                            "Double" => PrimitiveType.Double,
                            "Float" => PrimitiveType.Float,
                            "Decimal" => PrimitiveType.Decimal,
                            "Boolean" => PrimitiveType.Bool,
                            "Byte" => PrimitiveType.Byte,
                            "SByte" => PrimitiveType.SByte,
                            "Char" => PrimitiveType.Char,
                            "IntPtr" => PrimitiveType.NInt,
                            "UIntPtr" => PrimitiveType.NUint,
                        }
                    });
                }

                if (type.IsGenericParameter)
                {
                    return new Lazy<IType>(new GenericParameter()
                    {
                        Identifier = type.GetTypeIdentifier(),
                        MustExtend = type.GetGenericParameterConstraints()
                            .Select(type => lazyTypes[type].Value)
                    });
                }
                
                if (type.IsClass)
                {
                    return new Lazy<IType>(() =>
                    {
                        if (type.IsConstructedGenericType)
                        {
                            return new ReflectionBoundGenericClass(type, lazyTypes);
                        }

                        if (type.ContainsGenericParameters)
                        {
                            return new ReflectionUnboundGenericClass(type, lazyTypes);
                        }
                        
                        return new ReflectionNonGenericClass(type, lazyTypes);
                    });
                }

                if (type.IsInterface)
                {
                    return new Lazy<IType>(() =>
                    {
                        if (type.IsConstructedGenericType)
                        {
                            return new ReflectionBoundGenericInterface(type, lazyTypes);
                        }

                        if (type.ContainsGenericParameters)
                        {
                            return new ReflectionUnboundGenericInterface(type, lazyTypes);
                        }

                        var result = new ReflectionNonGenericInterface(type, lazyTypes);
                        return result;
                    });
                }

                throw new NotImplementedException();
            });
        }
        
        public static TypeIdentifier GetTypeIdentifier(this Type type)
        {
            var name = type.Name;
            var lastIndex = name.LastIndexOf('`');
            if (lastIndex != -1)
            {
                name = name.Substring(0, lastIndex);
            }
            return new TypeIdentifier()
            {
                Name = name,
                Namespace = type.Namespace,
                Arity = type.GetGenericArguments().Length,
            };
        }

        public static Visibility GetVisibility(this ConstructorInfo constructor)
        {
            if (constructor.IsPublic)
            {
                return Visibility.Public;
            }

            if (constructor.IsPrivate)
            {
                return Visibility.Private;
            }

            if (constructor.IsFamily)
            {
                return Visibility.Protected;
            }
            
            return Visibility.Internal;
        }
        
        public static Visibility GetTypeVisibility(this Type t)
        {
            if (t.IsVisible
                && t.IsPublic
                && !t.IsNotPublic
                && !t.IsNested
                && !t.IsNestedPublic
                && !t.IsNestedFamily
                && !t.IsNestedPrivate
                && !t.IsNestedAssembly
                && !t.IsNestedFamORAssem
                && !t.IsNestedFamANDAssem)
            {
                return Visibility.Public;
            }

            if (!t.IsVisible
                && !t.IsPublic
                && !t.IsNotPublic
                && t.IsNested
                && !t.IsNestedPublic
                && !t.IsNestedFamily
                && t.IsNestedPrivate
                && !t.IsNestedAssembly
                && !t.IsNestedFamORAssem
                && !t.IsNestedFamANDAssem)
            {
                return Visibility.Private;
            }

            if (!t.IsVisible
                && !t.IsPublic
                && t.IsNotPublic
                && !t.IsNested
                && !t.IsNestedPublic
                && !t.IsNestedFamily
                && !t.IsNestedPrivate
                && !t.IsNestedAssembly
                && !t.IsNestedFamORAssem
                && !t.IsNestedFamANDAssem)
            {
                return Visibility.Internal;
            }

            if (!t.IsVisible
                && !t.IsPublic
                && !t.IsNotPublic
                && t.IsNested
                && !t.IsNestedPublic
                && t.IsNestedFamily
                && !t.IsNestedPrivate
                && !t.IsNestedAssembly
                && !t.IsNestedFamORAssem
                && !t.IsNestedFamANDAssem)
            {
                return Visibility.Protected;
            }

            if (t.IsNestedPublic)
            {
                return Visibility.Public;
            }

            throw new NotImplementedException();
        }
    }
}