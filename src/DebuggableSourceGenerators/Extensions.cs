using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml;
using ComposableCollections.Utilities;
using Humanizer;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace DebuggableSourceGenerators
{
    public static class Extensions
    {
        static Extensions()
        {
            MSBuildLocator.RegisterDefaults();
        }
        
        public static CodeIndexBuilder AddNugetPackage(this CodeIndexBuilder codeIndexBuilder, string packageName, string packageVersion, string targetFramework)
        {
            foreach (var assemblyFile in GetPathToNugetPackageDlls(packageName, packageVersion, targetFramework))
            {
                if (File.Exists(assemblyFile))
                {
                    codeIndexBuilder.AddAssemblyFile(assemblyFile);
                }
            }

            return codeIndexBuilder;
        }

        public static CodeIndexBuilder AddProjectNugetDependencies(this CodeIndexBuilder codeIndexBuilder, string projectFile)
        {
            var projectTargetFrameworks = GetProjectTargetFrameworks(projectFile).ToImmutableList();
            
            foreach (var nugetDependency in GetNugetDependencies(projectFile))
            {
                foreach (var targetFramework in projectTargetFrameworks)
                {
                    codeIndexBuilder.AddNugetPackage(nugetDependency.packageName, nugetDependency.packageVersion, targetFramework);
                }
            }

            return codeIndexBuilder;
        }

        public static CodeIndexBuilder AddProjectNugetDependencies(this CodeIndexBuilder codeIndexBuilder, string projectFile, string targetFramework)
        {
            var projectTargetFrameworks = GetProjectTargetFrameworks(projectFile).ToImmutableList();
            
            foreach (var nugetDependency in GetNugetDependencies(projectFile))
            {
                codeIndexBuilder.AddNugetPackage(nugetDependency.packageName, nugetDependency.packageVersion, targetFramework);
            }

            return codeIndexBuilder;
        }

        public static CodeIndexBuilder AddSolution(this CodeIndexBuilder codeIndexBuilder, string solutionFilePath)
        {
            var compilations = CompileSolution(solutionFilePath);
            foreach (var compilation in compilations)
            {
                codeIndexBuilder.AddCompilation(compilation);
            }

            return codeIndexBuilder;
        }
        
        public static CodeIndexBuilder AddProject(this CodeIndexBuilder codeIndexBuilder, string solutionFilePath, string projectFilePath, string projectAssemblyName = null)
        {
            projectAssemblyName ??= Path.GetFileNameWithoutExtension(projectFilePath);
            
            var compilation = CompileProject(solutionFilePath, projectAssemblyName);
            codeIndexBuilder.AddCompilation(compilation);
            codeIndexBuilder.AddProjectNugetDependencies(projectFilePath);

            return codeIndexBuilder;
        }

        public static CodeIndexBuilder AddAssemblyFile(this CodeIndexBuilder codeIndexBuilder, string assemblyFilePath)
        {
            var typeDefinitions = AssemblyDefinition
                .ReadAssembly(assemblyFilePath)
                .MainModule
                .Types;

            foreach (var typeDefinition in typeDefinitions)
            {
                // we don't want to index anonymous types
                if (typeDefinition.Name.Contains("AnonymousType"))
                {
                    continue;
                }
                
                codeIndexBuilder.AddType(typeDefinition);
            }

            return codeIndexBuilder;
        }

        public static TypeIdentifier GetTypeIdentifier(TypeDefinition typeDefinition)
        {
            return TypeIdentifier.Parse(typeDefinition.Namespace, typeDefinition.Name);
        }

        public static Lazy<Type> GetOrAdd(this CodeIndexBuilder codeIndexBuilder, TypeDefinition typeDefinition)
        {
            var identifier = GetTypeIdentifier(typeDefinition);
            
            var typeParameters = new Dictionary<string, Lazy<TypeParameter>>();
            
            // TODO - sometimes generic classes have a nested generic type that has type parameters with the same name.
            // e.g., class MyClass<T> { class MySubClass<T> { } }
            // In this case, the GenericParameters property has two values, both named T. So we just take the last one,
            // assuming that that's the inner T. This behavior could probably be improved.

            foreach (var genericParameter in typeDefinition.GenericParameters)
            {
                typeParameters[genericParameter.Name] = new Lazy<TypeParameter>(() => new TypeParameter()
                {
                    Identifier = new TypeIdentifier() {Name = genericParameter.Name},
                    VarianceMode = genericParameter.IsContravariant
                        ? VarianceMode.In
                        : (genericParameter.IsCovariant ? VarianceMode.Out : VarianceMode.None),
                    MustBeAssignedTo = genericParameter.Constraints
                        .Select(constraint => GetType(constraint.ConstraintType))
                        .ToImmutableList()
                });
            }
            
            Lazy<Type> GetType(TypeReference typeRef)
            {
                var typeDef = typeRef.Resolve();

                if (typeDef == null)
                {
                    return new Lazy<Type>(() => typeParameters[typeRef.Name].Value);
                }

                return codeIndexBuilder.GetOrAdd(typeDef);
            }
            
            if (typeDefinition.IsClass)
            {
                return codeIndexBuilder.GetOrAdd(identifier, () => new Class()
                {
                    Identifier = identifier,
                    Constructors = typeDefinition.GetConstructors().Select(constructor => new Constructor()
                    {
                        Parameters = constructor.Parameters.Select(parameter => new Parameter
                        {
                            Name = parameter.Name,
                            Mode = ParameterMode.In,
                            Type = GetType(parameter.ParameterType)
                        }).ToImmutableList(),
                    }).ToImmutableList(),
                    Fields = typeDefinition.Fields.Select(field => new Field()
                    {
                        Name = field.Name,
                        Type = GetType(field.FieldType)
                    }).ToImmutableList(),
                    Indexers = typeDefinition.Properties.Where(prop => prop.HasParameters).Select(prop =>
                    {
                        return new Indexer
                        {
                            Parameters = prop.Parameters.Select(parameter => new Parameter()
                            {
                                Name = parameter.Name,
                                Mode = parameter.IsIn ? ParameterMode.In : ParameterMode.Out,
                                Type = GetType(parameter.ParameterType)
                            }).ToImmutableList(),
                            ReturnType = GetType(prop.PropertyType)
                        };
                    }).ToImmutableList(),
                    Properties = typeDefinition.Properties.Where(prop => !prop.HasParameters).Select(prop =>
                    {
                        return new Property
                        {
                            Name = prop.Name,
                            Type = GetType(prop.PropertyType)
                        };
                    }).ToImmutableList(),
                    TypeParameters = typeParameters.Values.ToImmutableList(),
                    Methods = typeDefinition.Methods.Where(meth => !meth.IsConstructor && !meth.IsGetter && !meth.IsSetter)
                        .Select(meth => new Method()
                        {
                            Name = meth.Name,
                            ReturnType = GetType(meth.ReturnType),
                            Parameters = meth.Parameters.Select(param => new Parameter()
                            {
                                Name = param.Name,
                                Type = GetType(param.ParameterType)
                            }).ToImmutableList(),
                        }).ToImmutableList()
                });
            }
            else if (typeDefinition.IsInterface)
            {
                return codeIndexBuilder.GetOrAdd(identifier, () => new Interface()
                {
                    Identifier = identifier,
                    Indexers = typeDefinition.Properties.Where(prop => prop.HasParameters).Select(prop =>
                    {
                        return new Indexer
                        {
                            Parameters = prop.Parameters.Select(parameter => new Parameter()
                            {
                                Name = parameter.Name,
                                Mode = parameter.IsIn ? ParameterMode.In : ParameterMode.Out,
                                Type = GetType(parameter.ParameterType)
                            }).ToImmutableList(),
                            ReturnType = GetType(prop.PropertyType)
                        };
                    }).ToImmutableList(),
                    Properties = typeDefinition.Properties.Where(prop => !prop.HasParameters).Select(prop =>
                    {
                        return new Property
                        {
                            Name = prop.Name,
                            Type = GetType(prop.PropertyType)
                        };
                    }).ToImmutableList(),
                    TypeParameters = typeParameters.Values.ToImmutableList(),
                    Methods = typeDefinition.Methods.Select(meth => new Method()
                    {
                        Name = meth.Name,
                        ReturnType = GetType(meth.ReturnType),
                        Parameters = meth.Parameters.Select(param => new Parameter()
                        {
                            Name = param.Name,
                            Type = GetType(param.ParameterType)
                        }).ToImmutableList(),
                    }).ToImmutableList()
                });
            }
            else if (typeDefinition.IsEnum)
            {
                return codeIndexBuilder.GetOrAdd(identifier, () =>
                {
                    throw new NotImplementedException();
                });
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        
        public static CodeIndexBuilder AddType(this CodeIndexBuilder codeIndexBuilder, TypeDefinition typeDefinition)
        {
            codeIndexBuilder.GetOrAdd(typeDefinition);
            
            foreach (var nestedType in typeDefinition.NestedTypes)
            {
                codeIndexBuilder.AddType(nestedType);
            }

            return codeIndexBuilder;
        }

        public static CodeIndexBuilder AddCompilation(this CodeIndexBuilder codeIndexBuilder, Compilation compilation)
        {
            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                var root = syntaxTree.GetRoot();
                codeIndexBuilder.AddSyntaxNode(root, semanticModel);
            }

            return codeIndexBuilder;
        }

        public static CodeIndexBuilder AddSyntaxNode(this CodeIndexBuilder codeIndexBuilder, SyntaxNode syntaxNode, SemanticModel semanticModel)
        {
            var symbol = semanticModel.GetDeclaredSymbol(syntaxNode) as INamedTypeSymbol;
            if (symbol != null)
            {
                codeIndexBuilder.AddSymbol(symbol);
            }
            
            foreach (var child in syntaxNode.ChildNodes())
            {
                codeIndexBuilder.AddSyntaxNode(child, semanticModel);
            }

            return codeIndexBuilder;
        }

        public static CodeIndexBuilder AddSymbol(this CodeIndexBuilder codeIndexBuilder, INamedTypeSymbol symbol)
        {
            if (!symbol.IsType)
            {
                return codeIndexBuilder;
            }

            var typeIdentifier = new TypeIdentifier()
            {
                Name = symbol.Name,
                Namespace = symbol.ContainingNamespace.Name,
                Arity = symbol.Arity,
            };

            codeIndexBuilder.GetOrAdd(typeIdentifier, () =>
            {
                var fields = new List<Field>();
                var methods = new List<Method>();
                var properties = new List<Property>();
                var indexers = new List<Indexer>();
                var constructors = new List<Constructor>();
                
                var members = symbol.GetMembers();
                foreach (var member in members)
                {
                    if (member is IFieldSymbol fieldSymbol)
                    {
                        fields.Add(new Field()
                        {
                            Name = fieldSymbol.Name,
                            IsStatic = fieldSymbol.IsStatic,
                            Type = codeIndexBuilder.GetType(new TypeIdentifier()
                            {
                                Namespace = fieldSymbol.Type.ContainingNamespace.Name,
                                Name = fieldSymbol.Type.Name,
                                Arity = 0,
                            }),
                            Visibility = Visibility.Public
                        });
                    }
                    else if (member is IMethodSymbol methodSymbol)
                    {
                        if (methodSymbol.MethodKind == MethodKind.PropertyGet ||
                            methodSymbol.MethodKind == MethodKind.PropertySet)
                        {
                            continue;
                        }

                        if (methodSymbol.MethodKind == MethodKind.Constructor)
                        {
                            constructors.Add(new Constructor()
                            {
                                Visibility = Visibility.Public,
                                Parameters = methodSymbol.Parameters.Select(param => new Parameter()
                                {
                                    Name = param.Name,
                                    Type = codeIndexBuilder.GetType(new TypeIdentifier()
                                    {
                                        Name = param.Type.Name,
                                        Namespace = param.Type.ContainingNamespace.Name,
                                        Arity = 0,
                                    }),
                                    Mode = ParameterMode.In
                                }).ToList(),
                            });
                        }
                        else
                        {
                            methods.Add(new Method()
                            {
                                Name = methodSymbol.Name,
                                IsStatic = methodSymbol.IsStatic,
                                ReturnType = codeIndexBuilder.GetType(new TypeIdentifier()
                                {
                                    Namespace = methodSymbol.ReturnType.ContainingNamespace.Name,
                                    Name = methodSymbol.ReturnType.Name,
                                    Arity = 0,
                                }),
                                Visibility = Visibility.Public,
                                Parameters = methodSymbol.Parameters.Select(param => new Parameter()
                                {
                                    Name = param.Name,
                                    Type = codeIndexBuilder.GetType(new TypeIdentifier()
                                    {
                                        Name = param.Type.Name,
                                        Namespace = param.Type.ContainingNamespace.Name,
                                        Arity = 0,
                                    }),
                                    Mode = ParameterMode.In
                                }).ToList(),
                                IsVirtual = methodSymbol.IsVirtual,
                                TypeParameters = methodSymbol.TypeParameters.Select(tps => tps.Name).ToList()
                            });
                        }
                    }
                    else if (member is IPropertySymbol propertySymbol)
                    {
                        if (propertySymbol.IsIndexer)
                        {
                            indexers.Add(new Indexer()
                            {
                                IsStatic = propertySymbol.IsStatic,
                                ReturnType = codeIndexBuilder.GetType(new TypeIdentifier()
                                {
                                    Namespace = propertySymbol.Type.ContainingNamespace.Name,
                                    Name = propertySymbol.Type.Name,
                                    Arity = 0,
                                }),
                                Parameters = propertySymbol.Parameters.Select(param => new Parameter()
                                {
                                    Name = param.Name,
                                    Type = codeIndexBuilder.GetType(new TypeIdentifier()
                                    {
                                        Name = param.Type.Name,
                                        Namespace = param.Type.ContainingNamespace.Name,
                                        Arity = 0,
                                    }),
                                    Mode = ParameterMode.In
                                }).ToList(),
                                GetterVisibility = Visibility.Public,
                                SetterVisibility = Visibility.Public,
                                IsVirtual = propertySymbol.IsVirtual
                            });
                        }
                        else
                        {
                            properties.Add(new Property()
                            {
                                Name = propertySymbol.Name,
                                IsStatic = propertySymbol.IsStatic,
                                Type = codeIndexBuilder.GetType(new TypeIdentifier()
                                {
                                    Namespace = propertySymbol.Type.ContainingNamespace.Name,
                                    Name = propertySymbol.Type.Name,
                                    Arity = 0,
                                }),
                                GetterVisibility = Visibility.Public,
                                SetterVisibility = Visibility.Public,
                                IsVirtual = propertySymbol.IsVirtual
                            });
                        }
                    }
                }

                return new Class()
                {
                    Constructors = constructors.ToImmutableList(),
                    Methods = methods.ToImmutableList(),
                    Indexers = indexers.ToImmutableList(),
                    Properties = properties.ToImmutableList(),
                    Fields = fields.ToImmutableList(),
                };
            });

            return codeIndexBuilder;
        }

        private static IEnumerable<string> GetTargetFrameworksForNugetPackage(string packageName, string packageVersion)
        {
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || 
                               Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            var packageFolder = Path.Combine(homePath, ".nuget", "packages", packageName, packageVersion, "lib");

            if (!Directory.Exists(packageFolder))
            {
                return Enumerable.Empty<string>();
            }
            
            return Directory.GetDirectories(packageFolder).Select(dir => Path.GetFileName(dir));
        }

        private static int? HowCompatibleAreTargetFrameworks(string olderTargetFramework,
            string newerTargetFramework)
        {
            if (olderTargetFramework == newerTargetFramework)
            {
                return 0;
            }
            
            if (newerTargetFramework == "net5.0")
            {
                if (olderTargetFramework == "net472")
                {
                    return null;
                }

                if (olderTargetFramework == "net46")
                {
                    return null;
                }

                if (olderTargetFramework == "net40")
                {
                    return null;
                }
                
                if (olderTargetFramework.StartsWith("netstandard"))
                {
                    return 1;
                }
                
                if (olderTargetFramework == "netcoreapp3.1")
                {
                    return 2;
                }
                
                if (olderTargetFramework == "netcoreapp3.0")
                {
                    return 3;
                }
                
                if (olderTargetFramework == "netcoreapp2.1")
                {
                    return 4;
                }
                
                if (olderTargetFramework == "netcoreapp2.0")
                {
                    return 5;
                }
            }

            return null;
        }
        
        private static IEnumerable<string> GetPathToNugetPackageDlls(string packageName, string packageVersion,
            string targetFramework)
        {
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || 
                               Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            var availableTargetFrameworks = GetTargetFrameworksForNugetPackage(packageName, packageVersion).ToImmutableList();
            
            var packageTargetFramework = availableTargetFrameworks
                .Select(availableFramework => new { compatibility = HowCompatibleAreTargetFrameworks(availableFramework, targetFramework), availableFramework })
                .Where(x => x.compatibility != null)
                .OrderBy(x => x.compatibility)
                .Select(x => x.availableFramework)
                .FirstOrDefault();

            if (packageTargetFramework == null)
            {
                Console.WriteLine($"None of the available target frameworks for {packageName} {packageVersion} ({availableTargetFrameworks.Humanize()}) are compatible with {targetFramework})");
                return Enumerable.Empty<string>();
            }
            
            var packageFolder = Path.Combine(homePath, ".nuget", "packages", packageName, packageVersion, "lib", packageTargetFramework);

            if (Directory.Exists(packageFolder))
            {
                return Directory.GetFiles(packageFolder, "*.dll");
            }
            
            // TODO - error out and fix the times this breaks
            return Enumerable.Empty<string>();
        }

        private static IEnumerable<(string packageName, string packageVersion)> GetNugetDependencies(string projectFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(projectFile);
            var result = new List<(string packageName, string packageVersion)>();
            var packageReferences = xmlDoc.SelectNodes("//Project/ItemGroup/PackageReference");
            if (packageReferences != null)
            {
                foreach (var item in packageReferences)
                {
                    var packageReference = item as XmlElement;
                    var packageName = packageReference.Attributes["Include"].Value;
                    var packageVersion = packageReference.Attributes["Version"].Value;
                    result.Add((packageName, packageVersion));
                }
            }

            return result;
        }

        private static IEnumerable<string> GetProjectTargetFrameworks(string projectFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(projectFile);
            var result = new List<string>();
            var packageReferences = xmlDoc.SelectNodes("//Project/PropertyGroup/TargetFramework");
            if (packageReferences != null)
            {
                foreach (var item in packageReferences)
                {
                    var targetFramework = item as XmlElement;
                    result.Add(targetFramework.InnerText);
                }
            }

            return result;
        }
        
        private static IEnumerable<Compilation> CompileSolution(string solutionUrl)
        {
            using MSBuildWorkspace workspace = MSBuildWorkspace.Create();

            Solution solution = workspace.OpenSolutionAsync(solutionUrl).Result;
            ProjectDependencyGraph projectGraph = solution.GetProjectDependencyGraph();

            foreach (ProjectId projectId in projectGraph.GetTopologicallySortedProjects())
            {
                var project = solution.GetProject(projectId);

                var dllDependencies = GetNugetDependencies(project.FilePath)
                    .SelectMany(nugetDependency =>
                    {
                        return GetProjectTargetFrameworks(project.FilePath)
                            .SelectMany(targetFramework => GetPathToNugetPackageDlls(nugetDependency.packageName,
                                nugetDependency.packageVersion, targetFramework));
                    }).ToImmutableList();

                var additionalMetadataReferences = dllDependencies
                    .Select(dllFilePath => MetadataReference.CreateFromFile(dllFilePath));

                project = project.AddMetadataReferences(additionalMetadataReferences);
                
                var compilationTask = project.GetCompilationAsync();
                compilationTask.Wait();
                Compilation projectCompilation = compilationTask.Result;
                
                if (null != projectCompilation && !string.IsNullOrEmpty(projectCompilation.AssemblyName))
                {
                    yield return projectCompilation;
                }
            }
        }
        
        private static Compilation CompileProject(string solutionFilePath, string projectAssemblyName)
        {
            return CompileSolution(solutionFilePath)
                .First(compilation => compilation.AssemblyName == Path.GetFileNameWithoutExtension(projectAssemblyName));
        }
    }
}