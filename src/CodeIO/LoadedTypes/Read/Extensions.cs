using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using CodeIO.LoadedTypes.Read;
using ComposableCollections;

namespace CodeIO
{
    public static class Extensions
    {
        /// <summary>
	    /// Converts a <see cref="Type"/> to its C# name. E.g., if you pass in typeof(<see cref="IEnumerable{int}"/>) this
	    /// will return "IEnumerable<int>".
	    /// This is useful mainly in exception error messages.
	    /// </summary>
	    /// <param name="type">The type to get a C# description of.</param>
	    /// <returns>The string representation of a human-friendly type name</returns>
	    public static string GetCSharpTypeName( this Type type, Type[] genericParameters, bool includeNamespaces = false ) {
		    if ( type == typeof( long ) ) {
			    return "long";
		    }

		    if ( type == typeof( int ) ) {
			    return "int";
		    }

		    if ( type == typeof( short ) ) {
			    return "short";
		    }

		    if ( type == typeof( ulong ) ) {
			    return "ulong";
		    }

		    if ( type == typeof( uint ) ) {
			    return "uint";
		    }

		    if ( type == typeof( ushort ) ) {
			    return "ushort";
		    }

		    if ( type == typeof( byte ) ) {
			    return "byte";
		    }

		    if ( type == typeof( sbyte ) ) {
			    return "sbyte";
		    }

		    if ( type == typeof( string ) ) {
			    return "string";
		    }

		    if ( type == typeof( void ) ) {
			    return "void";
		    }

			var baseName = new StringBuilder();
			var typeName = includeNamespaces ? type.Namespace + "." + type.Name : type.Name;
		    if ( type.Name.Contains( "`" ) ) {
			    baseName.Append( typeName.Substring( 0, typeName.IndexOf( "`" ) ) );
		    } else {
			    baseName.Append( typeName );
		    }

		    if ( genericParameters.Any() ) {
			    baseName.Append( "<" );
			    for ( var i = 0; i < genericParameters.Length; i++ ) {
				    baseName.Append( GetCSharpTypeName( genericParameters[i], includeNamespaces ) );
				    if ( i + 1 < genericParameters.Length ) {
					    baseName.Append( ", " );
				    }
			    }

			    baseName.Append( ">" );
		    }

		    return baseName.ToString();
	    }

		/// <summary>
		/// Converts a <see cref="Type"/> to its C# name. E.g., if you pass in typeof(<see cref="IEnumerable{int}"/>) this
		/// will return "IEnumerable<int>".
		/// This is useful mainly in exception error messages.
		/// </summary>
		/// <param name="type">The type to get a C# description of.</param>
		/// <returns>The string representation of a human-friendly type name</returns>
		public static string GetCSharpTypeName( this Type type, bool includeNamespaces = false ) {
		    if ( type == typeof(long) ) {
			    return "long";
		    }

		    if ( type == typeof( int ) ) {
			    return "int";
		    }

			if ( type == typeof(short) ) {
			    return "short";
		    }

			if ( type == typeof( ulong ) ) {
				return "ulong";
			}

			if ( type == typeof( uint ) ) {
				return "uint";
			}

			if ( type == typeof( ushort ) ) {
				return "ushort";
			}

			if ( type == typeof( byte ) ) {
				return "byte";
			}

			if ( type == typeof( sbyte ) ) {
				return "sbyte";
			}

			if ( type == typeof( string ) ) {
				return "string";
			}

			if ( type == typeof( void ) ) {
				return "void";
			}

			var baseName = new StringBuilder();
			var typeName = includeNamespaces ? type.Namespace + "." + type.Name : type.Name;
			if ( type.Name.Contains( "`" ) ) {
				baseName.Append( typeName.Substring( 0, typeName.IndexOf( "`" ) ) );
			} else {
				baseName.Append( typeName );
			}

			if ( type.GenericTypeArguments.Any() ) {
			    baseName.Append( "<" );
			    for ( var i = 0; i < type.GenericTypeArguments.Length; i++) {
				    baseName.Append( GetCSharpTypeName( type.GenericTypeArguments[i], includeNamespaces ) );
				    if ( i + 1 < type.GenericTypeArguments.Length ) {
					    baseName.Append( ", " );
				    }
			    }

			    baseName.Append(">");
		    }

		    return baseName.ToString();
	    }

        public static TypeReader AddReflection(this TypeReader typeReader)
        {
            return typeReader.AddTypeFormat<Type>(type => type.GetTypeIdentifier(), (type, lazyTypes) =>
            {
	            if (type.IsPrimitive)
                {
                    return new Lazy<IType>(new ReflectionPrimitive(type));
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

                if (type.IsEnum)
                {
	                return new Lazy<IType>(new ReflectionEnum(type, lazyTypes));
                }

                if (type == typeof(void))
                {
	                return new Lazy<IType>(ReflectionVoid.Instance);
                }

                throw new NotImplementedException($"Cannot process type: {type.GetCSharpTypeName()}");
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
        
        public static Visibility GetVisibility(this MethodInfo constructor)
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

            throw new NotImplementedException($"Unknown visibility for {t.GetCSharpTypeName()}");
        }
    }
}