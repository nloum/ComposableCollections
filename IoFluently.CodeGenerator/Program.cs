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
using CodeIO.SourceCode.Write;
using Void = CodeIO.Void;

namespace IoFluently.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSystem = new LocalFileSystem();
            var repoRoot = fileSystem.CurrentDirectory.Ancestors(false).First(ancestor => fileSystem.IsFolder(ancestor / ".git"));

            using (var partialClassesWriter = (repoRoot / "IoFluently" / "PartialClasses.g.cs").ExpectTextFileOrMissingPath().OpenWriter())
            {
                partialClassesWriter.WriteLine(@"using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
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

");
                
                GeneratePartialClassesWithProperties(repoRoot, partialClassesWriter);
            }
            
            using (var ioExtensionsWriter = (repoRoot / "IoFluently" / "IoExtensions.g.cs").ExpectTextFileOrMissingPath().OpenWriter())
            {
                ioExtensionsWriter.WriteLine(@"using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
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
    /// methods on the object's FileSystem property. That is, myAbsolutePath.RelativeTo(parameter1) is equivalent to
    /// myAbsolutePath.FileSystem.RelativeTo(myAbsolutePath, parameter1). This shorthand makes the syntax be fluent
    /// while allowing the IFileSystem to be dependency injectable.
    /// </summary>
    public static partial class IoExtensions
    {");
                
                GenerateIoExtensions(repoRoot, ioExtensionsWriter);
                
                ioExtensionsWriter.WriteLine("    }\n}");
            }
        }

        private static Regex _memberRegex = new Regex(@"M\:(?<type_name>[0-9][a-z][A-Z]\.)\.(?<member_name>[0-9][a-z][A-Z])(\((?<parameters>.+)\))?");

        private static void GeneratePartialClassesWithProperties(FolderPath repoRoot, TextWriter textWriter)
        {
            var typeReader = new TypeReader();
            typeReader.AddSupportForReflection();
            var fileSystemType = (ReflectionNonGenericInterface)typeReader.GetTypeFormat<Type>()[typeof(IFileSystem)].Value;

            var partialTypes = new List<IPartialType>();

            foreach (var groupedByPartialClass in fileSystemType.Methods.OrderBy(method => method.Name).ThenBy(method => method.Parameters.Count).ThenBy(method => method.GetHashCode())
                .Where(x => ShouldBeProperty(x))
                .GroupBy(x => x.Parameters[0].Type.Identifier))
            {
                var properties = new List<PropertySourceCodeWriter>();
            
                var isInterface = typeReader.Types[groupedByPartialClass.Key] is IInterface;
                foreach (var method in groupedByPartialClass)
                {
                    string methodName;
                    if (method.ReturnType is IBoundGenericInterface boundIface && boundIface.Identifier.Name == "IMaybe" && method.Name.StartsWith("Try"))
                    {
                        methodName = method.Name.Substring(3);
                        if (methodName.StartsWith("Get"))
                        {
                            methodName = methodName.Substring(3);
                        }
                    }
                    else
                    {
                        methodName = method.Name;
                    }
                    properties.Add(new PropertySourceCodeWriter()
                    {
                        Name = methodName,
                        Implementation = new MethodToPropertyDelegateImplementation()
                        {
                            DelegatesTo = method,
                            Body = $"FileSystem.{method.Name}(this);"
                        }
                    });
                }

                if (isInterface)
                {
                    partialTypes.Add(new PartialInterface()
                    {
                        Identifier = groupedByPartialClass.Key,
                        Properties = properties.ToImmutableList()
                    });
                }
                else
                {
                    partialTypes.Add(new PartialClass()
                    {
                        Identifier = groupedByPartialClass.Key,
                        Properties = properties.ToImmutableList()
                    });
                }
                
                foreach (var type in typeReader.LazyTypes.ToImmutableList())
                {
                    if (type.Value.Value is IClass clazz && clazz.Interfaces.Any(x => x.Identifier.Equals(groupedByPartialClass.Key)))
                    {
                        var classProperties = new List<PropertySourceCodeWriter>();
                        foreach (var method in groupedByPartialClass)
                        {
                            string methodName;
                            if (method.ReturnType is IBoundGenericInterface boundIface && boundIface.Identifier.Name == "IMaybe" && method.Name.StartsWith("Try"))
                            {
                                methodName = method.Name.Substring(3);
                                if (methodName.StartsWith("Get"))
                                {
                                    methodName = methodName.Substring(3);
                                }
                            }
                            else
                            {
                                methodName = method.Name;
                            }
        
                            classProperties.Add(new PropertySourceCodeWriter()
                            {
                                Name = methodName,
                                Implementation = new MethodToPropertyDelegateImplementation()
                                {
                                    DelegatesTo = method,
                                    Body = $"FileSystem.{method.Name}(this);"
                                }
                            });
                        }
                        partialTypes.Add(new PartialClass()
                        {
                            Identifier = clazz.Identifier,
                            Properties = classProperties.ToImmutableList()
                        });
                    }
                }
            }
            
            var sourceCodeWriter = new SourceCodeWriter(textWriter);
            foreach (var partialType in partialTypes.Merge().ToImmutableList())
            {
                partialType.RemoveDuplicates().Generate(sourceCodeWriter);
            }
        }

        private static bool ShouldBeProperty(IMethod method)
        {
            if (method.Parameters.Count != 1 || method.ReturnType is ReflectionVoid && method.IsStatic ||
                  method.Visibility != Visibility.Public)
            {
                return false;
            }

            var onlyParam = method.Parameters[0];
            var onlyParamTypeName = onlyParam.Type.Identifier.Name;
            if (onlyParamTypeName != "AbsolutePath" && onlyParamTypeName != "RelativePath"
                && onlyParamTypeName != "IAbsolutePathTranslation"
                && onlyParamTypeName != "IFileOrFolderOrMissingPath"
                && onlyParamTypeName != "IFile"
                && onlyParamTypeName != "IFolder"
                && onlyParamTypeName != "IMissingPath"
                && onlyParamTypeName != "IFileOrMissingPath"
                && onlyParamTypeName != "IFolderOrMissingPath"
                && onlyParamTypeName != "IFileOrFolder")
            {
                return false;
            }

            if (method.Name.Contains("Open")
                || method.Name.Contains("Clear")
                || method.Name.Contains("Delete")
                || method.Name.Contains("Ensure")
                || method.Name.Contains("Observe")
                || (method.Name.Contains("Read") && !method.Name.Contains("ReadOnly"))
                || method.Name.Contains("Set")
                || method.Name.Equals("Decrypt")
                || method.Name.Equals("Encrypt")
                || method.Name.Contains("Enumerate")
                || method.Name.Equals("Renamings")
                || method.Name.Equals("Simplify"))
            {
                return false;
            }
            
            return true;
        }
        
        private static void GenerateIoExtensions(FolderPath repoRoot, TextWriter textWriter)
        {
            var typeReader = new TypeReader();
            typeReader.AddSupportForReflection();
            var fileSystemType = (ReflectionNonGenericInterface)typeReader.GetTypeFormat<Type>()[typeof(IFileSystem)].Value;
            var extensionMethodTypes = new[]
            {
                typeof(FolderPath),
                typeof(RelativePath),
                typeof(IFileOrFolderOrMissingPath),
                typeof(IPathTranslation),
                typeof(IFilePath),
                typeof(IFolderPath),
                typeof(IMissingPath),
                typeof(IFileOrFolderPath),
                typeof(IFileOrMissingPath),
                typeof(IFolderOrMissingPath)
            }.Select(x => typeReader.GetTypeFormat<Type>()[x].Value).ToImmutableHashSet();

            foreach (var method in fileSystemType.Methods)
            {
                if (ShouldBeProperty(method))
                {
                    continue;
                }
                
                if (method.Parameters.Count == 0)
                {
                    continue;
                }

                var firstParameter = method.Parameters[0];
                
                if (!extensionMethodTypes.Contains(firstParameter.Type))
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

                    parameterString += $"{ConvertToCSharpTypeName(((IReflectionType)parameter.Type).Type)} {parameter.Name}";

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

                textWriter.WriteLine($"        public static {ConvertToCSharpTypeName(((IReflectionType)method.ReturnType).Type)} {method.Name}({string.Join(", ",  parameters)}) {{");
                textWriter.Write("            ");
                if (method.ReturnType is not ReflectionVoid)
                {
                    textWriter.Write("return ");
                }
                textWriter.WriteLine($"{arguments[0]}.FileSystem.{method.Name}({string.Join(", ", arguments)});");
                textWriter.WriteLine("        }\n");

                if (method.ReturnType is IBoundGenericInterface boundIface)
                {
                    if (boundIface.Identifier.Name == "IMaybe" && method.Name.StartsWith("Try"))
                    {
                        var methodName = method.Name.Substring(3);
                        textWriter.WriteLine($"        public static {ConvertToCSharpTypeName(((IReflectionType)boundIface.Arguments[0]).Type)} {methodName}({string.Join(", ",  parameters)}) {{");
                        textWriter.Write("            ");
                        if (method.ReturnType is not ReflectionVoid)
                        {
                            textWriter.Write("return ");
                        }
                        textWriter.WriteLine($"{arguments[0]}.FileSystem.{method.Name}({string.Join(", ", arguments)}).Value;");
                        textWriter.WriteLine("        }\n");
                    }
                }
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
                var values = Enum.Format(parameterDefaultValue.GetType(), parameterDefaultValue, "g");
                var typeName = ConvertToCSharpTypeName(parameterDefaultValue.GetType());
                var result = string.Join(" | ", values.Split(',').Select(item => $"{typeName}.{item.Trim()}"));
                return result;
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