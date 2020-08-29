using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IoFluently.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var ioService = new IoService();
            var repoRoot = ioService.CurrentDirectory.Ancestors().First(ancestor => (ancestor / ".git").IsFolder());

            using (var ioExtensionsWriter = (repoRoot / "src" / "IoFluently" / "IoExtensions.g.cs").OpenWriter())
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
    public static partial class IoExtensions
    {");
                
                GenerateIoExtensions(ioExtensionsWriter);
                
                ioExtensionsWriter.WriteLine("    }\n}");
            }
        }
        
        private static void GenerateIoExtensions(TextWriter textWriter)
        {
            var methods = typeof(IIoService).GetMethods();
            foreach (var method in methods)
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
                    firstParameter.ParameterType != typeof(RelativePath))
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

                    parameterString += $" {ConvertToCSharpTypeName(parameter.ParameterType)} {parameter.Name}";

                    if (parameter.HasDefaultValue)
                    {
                        parameterString += $" = {ConvertToCSharpValue(parameter.DefaultValue)}";
                    }
                    
                    parameters.Add(parameterString);

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