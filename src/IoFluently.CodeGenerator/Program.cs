using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using CodeIO;
using CodeIO.LoadedTypes.Read;

namespace IoFluently.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var ioService = new IoService();
            var repoRoot = ioService.CurrentDirectory.Ancestors().First(ancestor => ioService.IsFolder(ancestor / ".git"));

            using (var ioExtensionsWriter = ioService.OpenWriter(repoRoot / "src" / "IoFluently" / "IoExtensions.g.cs"))
            {
                ioExtensionsWriter.WriteLine(@"using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reactive;
using System.Text;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using SimpleMonads;
using TreeLinq;
using UnitsNet;

namespace IoFluently
{
    /// <summary>
    /// Contains extension methods on AbsolutePath, RelativePath, and IAbsolutePathTranslation that essentially wrap
    /// methods on the object's IoService property. That is, myAbsolutePath.RelativeTo(parameter1) is equivalent to
    /// myAbsolutePath.IoService.RelativeTo(myAbsolutePath, parameter1). This shorthand makes the syntax be fluent
    /// while allowing the IIoService to be dependency injectable.
    /// </summary>
    public static partial class IoExtensions
    {");
                
                GenerateIoExtensions(repoRoot, ioExtensionsWriter);
                
                ioExtensionsWriter.WriteLine("    }\n}");
            }
        }

        private static Regex _memberRegex = new Regex(@"M\:(?<type_name>[0-9][a-z][A-Z]\.)\.(?<member_name>[0-9][a-z][A-Z])(\((?<parameters>.+)\))?");
        
        private static void GenerateIoExtensions(AbsolutePath repoRoot, TextWriter textWriter)
        {
            var typeReader = new TypeReader();
            typeReader.AddReflection();
            var ioServiceType = (ReflectionNonGenericInterface)typeReader.GetTypeFormat<Type>()[typeof(IIoService)].Value;
            var absolutePath = typeReader.GetTypeFormat<Type>()[typeof(AbsolutePath)].Value;
            var relativePath = typeReader.GetTypeFormat<Type>()[typeof(RelativePath)].Value;
            var absolutePathTranslation = typeReader.GetTypeFormat<Type>()[typeof(IAbsolutePathTranslation)].Value;

            foreach (var method in ioServiceType.Methods)
            {
                if (method.Parameters.Count == 0)
                {
                    continue;
                }

                var firstParameter = method.Parameters[0];
                
                if (!Equals(firstParameter.Type, absolutePath) &&
                    !Equals(firstParameter.Type, relativePath) &&
                    !Equals(firstParameter.Type, absolutePathTranslation))
                {
                    continue;
                }
                
                
                var parameters = new List<string>();
                var arguments = new List<string>();

                foreach (var parameter in method.Parameters)
                {
                    var parameterString = "";
                    if (parameters.Count == 0)
                    {
                        parameterString = "this " + parameterString;
                    }
                    else
                    {
                        if (parameter.IsOut)
                        {
                            parameterString = "out " + parameterString;
                        }
                    }

                    parameterString += $"{ConvertToCSharpTypeName(((ReflectionComplexTypeBase)parameter.Type).Type)} {parameter.Name}";

                    if (parameter.HasDefaultValue)
                    {
                        parameterString += $" = {ConvertToCSharpValue(parameter.DefaultValue)}";
                    }
                    
                    parameters.Add(parameterString.Trim());

                    if (parameter.IsOut)
                    {
                        arguments.Add($"out {parameter.Name}");
                    }
                    else
                    {
                        arguments.Add(parameter.Name);
                    }
                }

                textWriter.WriteLine($"public static {ConvertToCSharpTypeName(((ReflectionComplexTypeBase)method.ReturnType).Type)} {method.Name}({string.Join(", ",  parameters)}) {{");
                if (method.ReturnType != typeof(void))
                {
                    textWriter.Write("return ");
                }
                textWriter.WriteLine($"{arguments[0]}.IoService.{method.Name}({string.Join(", ", arguments)});");
                textWriter.WriteLine("}\n");
            }
            
            foreach (var method in ioServiceType.Methods.OrderBy(method => method.Name).ThenBy(method => method.Parameters.Count).ThenBy(method => method.GetHashCode()))
            {
                if (method.IsStatic || !method.IsPublic)
                {
                    continue;
                }

                if (method.GetParameters().Length == 0)
                {
                    continue;
                }

                var firstParameter = method.GetParameters()[0];
                if (firstParameter.ParameterType != typeof(AbsolutePath) &&
                    firstParameter.ParameterType != typeof(RelativePath) &&
                    firstParameter.ParameterType != typeof(IAbsolutePathTranslation))
                {
                    continue;
                }

                var parameters = new List<string>();
                var arguments = new List<string>();

                foreach (var parameter in method.GetParameters())
                {
                    var parameterString = "";
                    if (parameters.Count == 0)
                    {
                        parameterString = "this " + parameterString;
                    }
                    else
                    {
                        if (parameter.IsOut)
                        {
                            parameterString = "out " + parameterString;
                        }
                        else if (parameter.IsIn)
                        {
                            parameterString = "in " + parameterString;
                        }
                    }

                    parameterString += $"{ConvertToCSharpTypeName(parameter.ParameterType)} {parameter.Name}";

                    if (parameter.HasDefaultValue)
                    {
                        parameterString += $" = {ConvertToCSharpValue(parameter.DefaultValue)}";
                    }
                    
                    parameters.Add(parameterString.Trim());

                    if (parameter.IsOut)
                    {
                        arguments.Add($"out {parameter.Name}");
                    }
                    else if (parameter.IsIn)
                    {
                        arguments.Add($"in {parameter.Name}");
                    }
                    else
                    {
                        arguments.Add(parameter.Name);
                    }
                }

                textWriter.WriteLine($"public static {ConvertToCSharpTypeName(method.ReturnType)} {method.Name}({string.Join(", ",  parameters)}) {{");
                if (method.ReturnType != typeof(void))
                {
                    textWriter.Write("return ");
                }
                textWriter.WriteLine($"{arguments[0]}.IoService.{method.Name}({string.Join(", ", arguments)});");
                textWriter.WriteLine("}\n");
            }
        }

        private static string ConvertToCSharpValue(object parameterDefaultValue)
        {
            if (parameterDefaultValue == null)
            {
                return "null";
            }
            else if (parameterDefaultValue is bool b)
            {
                return b.ToString().ToLower();
            }
            else if (parameterDefaultValue is string str)
            {
                return $"@\"{str}\"";
            }
            else if (parameterDefaultValue.GetType().IsEnum)
            {
                return $"{ConvertToCSharpTypeName(parameterDefaultValue.GetType())}.{parameterDefaultValue}";
            }   
            else
            {
                return parameterDefaultValue.ToString();
            }
        }

        /// <summary>
        /// Converts a <see cref="Type"/> to its C# name. E.g., if you pass in typeof(<see cref="IEnumerable{int}"/>) this
        /// will return "IEnumerable<int>".
        /// This is useful mainly in exception error messages.
        /// </summary>
        /// <param name="type">The type to get a C# description of.</param>
        /// <returns>The string representation of a human-friendly type name</returns>
        private static string ConvertToCSharpTypeName( Type type, bool includeNamespaces = false ) {
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
                    baseName.Append( ConvertToCSharpTypeName( type.GenericTypeArguments[i], includeNamespaces ) );
                    if ( i + 1 < type.GenericTypeArguments.Length ) {
                        baseName.Append( ", " );
                    }
                }

                baseName.Append(">");
            }

            return baseName.ToString();
        }
        
    }
}