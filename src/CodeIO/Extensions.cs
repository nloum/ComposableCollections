using System;

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

                var identifier = type.GetTypeIdentifier();

                if (type.IsClass)
                {
                    return new Lazy<IType>(() =>
                    {
                        var result=new ReflectionNonGenericClass(type, lazyTypes);
                        return result;
                    });
                }

                throw new NotImplementedException();
            });
        }
        
        public static TypeIdentifier GetTypeIdentifier(this Type type)
        {
            return new TypeIdentifier()
            {
                Name = type.Name,
                Namespace = type.Namespace,
                Arity = type.GetGenericArguments().Length,
            };
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

            throw new NotImplementedException();
        }
    }
}