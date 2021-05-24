using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using Humanizer;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Mono.Cecil;
using Mono.Cecil.Rocks;
using NuGet.Common;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using ILogger = Microsoft.Build.Framework.ILogger;

namespace DebuggableSourceGenerators.Read
{
    public static class Extensions
    {
        static Extensions()
        {
            MSBuildLocator.RegisterDefaults();
        }

        public static TypeIdentifier GetTypeIdentifier(this Type type)
        {
            return new TypeIdentifier()
            {
                Namespace = type.Namespace,
                Name = type.Name,
                Arity = type.GetGenericArguments().Length,
            };
        }

        public static CodeIndexBuilder Add(this CodeIndexBuilder codeIndexBuilder, Type type)
        {
            codeIndexBuilder.GetOrAdd(type);
            return codeIndexBuilder;
        }
        
        public static Lazy<TypeBase> GetOrAdd(this CodeIndexBuilder codeIndexBuilder, Type type)
        {
            var identifier = type.GetTypeIdentifier();

            return codeIndexBuilder.GetOrAdd(identifier, () =>
            {
                if (type.IsClass)
                {
                    return new Class
                    {
                        Identifier = identifier,
                        CodeIndex = codeIndexBuilder.LazyBuild(),
                        Constructors = type.GetConstructors().Select(constructor =>
                        {
                            return new Constructor()
                            {
                                Parameters = constructor.GetParameters().Select(parameter => new Parameter
                                {
                                    Type = codeIndexBuilder.GetOrAdd(parameter.ParameterType)
                                }).ToImmutableList(),
                            };
                        }).ToImmutableList(),
                        Fields = type.GetFields().Select(field =>
                        {
                            return new Field()
                            {
                                Type = codeIndexBuilder.GetOrAdd(field.FieldType)
                            };
                        }).ToImmutableList(),
                        Indexers = type.GetProperties().Where(prop => prop.GetIndexParameters().Length > 0).Select(
                            indexer =>
                            {
                                return new Indexer()
                                {
                                    Parameters = indexer.GetIndexParameters().Select(parameter => new Parameter
                                    {
                                        Type = codeIndexBuilder.GetOrAdd(parameter.ParameterType)
                                    }).ToImmutableList(),
                                    ReturnType = codeIndexBuilder.GetOrAdd(indexer.PropertyType)
                                };
                            }).ToImmutableList(),
                        Properties = type.GetProperties().Where(prop => prop.GetIndexParameters().Length == 0).Select(
                            property =>
                            {
                                return new Property()
                                {
                                    Type = codeIndexBuilder.GetOrAdd(property.PropertyType)
                                };
                            }).ToImmutableList(),
                        Interfaces = type.GetInterfaces().Select(iface => new Lazy<Interface>(() => (Interface)codeIndexBuilder.GetOrAdd(iface).Value))
                            .ToImmutableList(),
                        Methods = type.GetMethods().Select(
                            method =>
                            {
                                return new Method()
                                {
                                    Parameters = method.GetParameters().Select(parameter => new Parameter
                                    {
                                        Type = codeIndexBuilder.GetOrAdd(parameter.ParameterType)
                                    }).ToImmutableList(),
                                    ReturnType = codeIndexBuilder.GetOrAdd(method.ReturnType)
                                };
                            }).ToImmutableList(),
                        BaseClass = new Lazy<Class>(() => (Class) codeIndexBuilder.GetOrAdd(type.BaseType).Value),
                        TypeParameters = type.GenericTypeArguments.Select(arg => new Lazy<TypeParameter>(() =>
                            new TypeParameter()
                            {
                                Identifier = new TypeIdentifier()
                                {
                                    Name = arg.Name,
                                    Namespace = null,
                                    Arity = 0
                                },
                            })).ToImmutableList(),
                    };
                }
                if (type.IsInterface)
                {
                    return new Interface
                    {
                        Identifier = identifier,
                        CodeIndex = codeIndexBuilder.LazyBuild(),
                        Indexers = type.GetProperties().Where(prop => prop.GetIndexParameters().Length > 0).Select(
                            indexer =>
                            {
                                return new Indexer()
                                {
                                    Parameters = indexer.GetIndexParameters().Select(parameter => new Parameter
                                    {
                                        Type = codeIndexBuilder.GetOrAdd(parameter.ParameterType)
                                    }).ToImmutableList(),
                                    ReturnType = codeIndexBuilder.GetOrAdd(indexer.PropertyType)
                                };
                            }).ToImmutableList(),
                        Properties = type.GetProperties().Where(prop => prop.GetIndexParameters().Length == 0).Select(
                            property =>
                            {
                                return new Property()
                                {
                                    Type = codeIndexBuilder.GetOrAdd(property.PropertyType)
                                };
                            }).ToImmutableList(),
                        Interfaces = type.GetInterfaces().Select(iface => new Lazy<Interface>(() => (Interface)codeIndexBuilder.GetOrAdd(iface).Value))
                            .ToImmutableList(),
                        Methods = type.GetMethods().Select(
                            method =>
                            {
                                return new Method()
                                {
                                    Parameters = method.GetParameters().Select(parameter => new Parameter
                                    {
                                        Type = codeIndexBuilder.GetOrAdd(parameter.ParameterType)
                                    }).ToImmutableList(),
                                    ReturnType = codeIndexBuilder.GetOrAdd(method.ReturnType)
                                };
                            }).ToImmutableList(),
                        TypeParameters = type.GenericTypeArguments.Select(arg => new Lazy<TypeParameter>(() =>
                            new TypeParameter()
                            {
                                Identifier = new TypeIdentifier()
                                {
                                    Name = arg.Name,
                                    Namespace = null,
                                    Arity = 0
                                },
                            })).ToImmutableList(),
                    };
                }
                if (type.IsEnum)
                {
                    return new Enum
                    {
                        Identifier = identifier,
                        CodeIndex = codeIndexBuilder.LazyBuild(),
                        Values = type.GetFields().Select(field => field.Name).ToImmutableList(),
                    };
                }
                
                throw new NotImplementedException();
            });
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

        public static CodeIndexBuilder AddSolution(this CodeIndexBuilder codeIndexBuilder, string solutionFilePath, Func<string, bool> projectAssemblyNamePredicate = null)
        {
            if (projectAssemblyNamePredicate == null)
            {
                projectAssemblyNamePredicate = _ => true;
            }
            
            using MSBuildWorkspace workspace = MSBuildWorkspace.Create();

            var openSolutionTask = workspace.OpenSolutionAsync(solutionFilePath);
            openSolutionTask.Wait();
            Solution solution = openSolutionTask.Result;
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
                    .Select(dllFilePath => MetadataReference.CreateFromFile(dllFilePath)).ToList();
                
                project = project.AddMetadataReferences(additionalMetadataReferences);
                
                var compilationTask = project.GetCompilationAsync();
                compilationTask.Wait();
                Compilation projectCompilation = compilationTask.Result;
                
                if (null != projectCompilation && !string.IsNullOrEmpty(projectCompilation.AssemblyName))
                {
                    if (projectAssemblyNamePredicate(projectCompilation.AssemblyName))
                    {
                        codeIndexBuilder.AddCompilation(projectCompilation);
                        codeIndexBuilder.AddProjectNugetDependencies(project.FilePath);

                        // using (var pestream = new MemoryStream())
                        // using (var pdbstream = new MemoryStream())
                        // using (var xmldocstream = new MemoryStream())
                        // using (var win32resources = new MemoryStream())
                        // {
                        //     var result = projectCompilation.Emit(pestream, pdbstream, xmldocstream, win32resources);
                        //     
                        //     if (!result.Success)
                        //     {
                        //         var formatter = new DiagnosticFormatter();
                        //         var errors = result.Diagnostics.Where(diag => diag.Severity == DiagnosticSeverity.Error)
                        //             .ToImmutableList();
                        //         foreach (var diagnostic in errors)
                        //         {
                        //             var message = formatter.Format(diagnostic);
                        //             Console.WriteLine(message);
                        //         }
                        //     }
                        //
                        //     pestream.Seek(0, SeekOrigin.Begin);
                        //     pdbstream.Seek(0, SeekOrigin.Begin);
                        //     xmldocstream.Seek(0, SeekOrigin.Begin);
                        //     win32resources.Seek(0, SeekOrigin.Begin);
                        //     var typeDefinitions = AssemblyDefinition
                        //         .ReadAssembly(pestream, new ReaderParameters()
                        //         {
                        //             InMemory = true,
                        //             ReadingMode = ReadingMode.Immediate,
                        //             SymbolStream = pdbstream,
                        //             ReadSymbols = true,
                        //         })
                        //         .MainModule
                        //         .Types;
                        //
                        //     foreach (var typeDefinition in typeDefinitions)
                        //     {
                        //         codeIndexBuilder.AddType(typeDefinition);
                        //     }
                        // }
                    }
                }
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

        public static TypeIdentifier GetTypeIdentifier(Mono.Cecil.TypeDefinition typeDefinition)
        {
            return TypeIdentifier.Parse(typeDefinition.Namespace, typeDefinition.Name);
        }

        public static Lazy<TypeBase> GetOrAdd(this CodeIndexBuilder codeIndexBuilder, Mono.Cecil.TypeDefinition typeDefinition)
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
                    CodeIndex = codeIndexBuilder.LazyBuild(),
                    VarianceMode = genericParameter.IsContravariant
                        ? VarianceMode.In
                        : (genericParameter.IsCovariant ? VarianceMode.Out : VarianceMode.None),
                    MustBeAssignedTo = genericParameter.Constraints
                        .Select(constraint => GetType(constraint.ConstraintType))
                        .ToImmutableList()
                });
            }
            
            Lazy<TypeBase> GetType(TypeReference typeRef)
            {
                Mono.Cecil.TypeDefinition typeDef = null;
                try
                {
                    typeDef = typeRef.Resolve();
                    
                    if (typeDef == null)
                    {
                        return new Lazy<TypeBase>(() => typeParameters[typeRef.Name].Value);
                    }

                    return codeIndexBuilder.GetOrAdd(typeDef);
                }
                catch (AssemblyResolutionException e)
                {
                    Console.WriteLine(e);
                    return new Lazy<TypeBase>(() => null);
                }
            }
            
            if (typeDefinition.IsClass)
            {
                return codeIndexBuilder.GetOrAdd(identifier, () => new Class()
                {
                    Identifier = identifier,
                    CodeIndex = codeIndexBuilder.LazyBuild(),
                    BaseClass = new Lazy<Class>(() => (Class)codeIndexBuilder.GetOrAdd(typeDefinition.BaseType.Resolve()).Value),
                    Interfaces = typeDefinition.Interfaces.Select(iface => new Lazy<Interface>(() => (Interface)codeIndexBuilder.GetOrAdd(iface.InterfaceType.Resolve()).Value)).ToImmutableList(),
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
                    CodeIndex = codeIndexBuilder.LazyBuild(),
                    Interfaces = typeDefinition.Interfaces.Select(iface => new Lazy<Interface>(() => (Interface)codeIndexBuilder.GetOrAdd(iface.InterfaceType.Resolve()).Value)).ToImmutableList(),
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
        
        public static CodeIndexBuilder AddType(this CodeIndexBuilder codeIndexBuilder, Mono.Cecil.TypeDefinition typeDefinition)
        {
            codeIndexBuilder.GetOrAdd(typeDefinition);
            
            foreach (var nestedType in typeDefinition.NestedTypes)
            {
                codeIndexBuilder.AddType(nestedType);
            }

            return codeIndexBuilder;
        }

        public static CodeIndexBuilder AddSyntaxNode(this CodeIndexBuilder codeIndexBuilder, SyntaxNode syntaxNode, SemanticModel semanticModel)
        {
            var symbol = semanticModel.GetDeclaredSymbol(syntaxNode) as INamedTypeSymbol;
            if (symbol != null)
            {
                if (symbol is not IErrorTypeSymbol)
                {
                    codeIndexBuilder.AddSymbol(symbol);
                }
            }
            
            foreach (var child in syntaxNode.ChildNodes())
            {
                codeIndexBuilder.AddSyntaxNode(child, semanticModel);
            }

            return codeIndexBuilder;
        }

        public static string GetFullNamespace(this INamespaceSymbol namespaceSymbol)
        {
            if (namespaceSymbol.IsGlobalNamespace)
            {
                return "";
            }
            
            var result = namespaceSymbol.ToString();
            return result;
        }

        public static Lazy<TypeBase> GetOrAdd(this CodeIndexBuilder codeIndexBuilder, INamedTypeSymbol symbol)
        {
            var typeIdentifier = new TypeIdentifier()
            {
                Name = symbol.Name,
                Namespace = GetFullNamespace(symbol.ContainingNamespace),
                Arity = symbol.Arity,
            };

            return codeIndexBuilder.GetOrAdd(typeIdentifier, () =>
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

                var baseTypes = symbol.Interfaces.ToList();
                if (symbol.BaseType != null)
                {
                    baseTypes.Add(symbol.BaseType);
                }

                var interfaces = baseTypes.Where(x => x.TypeKind == TypeKind.Interface || (x.TypeKind == TypeKind.Error && x.Name.StartsWith("I")))
                    .Select(type => new Lazy<Interface>(() =>
                    {
                        var result = (Interface) codeIndexBuilder.GetOrAdd(type).Value;
                        return result;
                    })).ToImmutableList();
                var baseClass = baseTypes.Where(x => x.TypeKind == TypeKind.Class || (x.TypeKind == TypeKind.Error && !x.Name.StartsWith("I"))).Select(type => new Lazy<Class>(() => (Class)codeIndexBuilder.GetOrAdd(type).Value)).FirstOrDefault();

                if (symbol.TypeKind == TypeKind.Class)
                {
                    return new Class()
                    {
                        Identifier = typeIdentifier,
                        CodeIndex = codeIndexBuilder.LazyBuild(),
                        Interfaces = interfaces,
                        BaseClass = baseClass,
                        Constructors = constructors.ToImmutableList(),
                        Methods = methods.ToImmutableList(),
                        Indexers = indexers.ToImmutableList(),
                        Properties = properties.ToImmutableList(),
                        Fields = fields.ToImmutableList(),
                    };
                }
                else if (symbol.TypeKind == TypeKind.Interface)
                {
                    return new Interface()
                    {
                        Identifier = typeIdentifier,
                        CodeIndex = codeIndexBuilder.LazyBuild(),
                        Interfaces = interfaces,
                        Methods = methods.ToImmutableList(),
                        Indexers = indexers.ToImmutableList(),
                        Properties = properties.ToImmutableList(),
                    };
                }
                else if (symbol.TypeKind == TypeKind.Enum)
                {
                    return new Enum()
                    {
                        Identifier = typeIdentifier,
                        CodeIndex = codeIndexBuilder.LazyBuild(),
                        Values = fields.Select(f => f.Name).ToImmutableList(),
                    };
                }
                else if (symbol.TypeKind == TypeKind.Error)
                {
                    return null;
                }
                else
                {
                    throw new NotImplementedException($"No support for {symbol.TypeKind.ToString().Pluralize()} yet");
                }
            });
        }
        
        public static CodeIndexBuilder AddSymbol(this CodeIndexBuilder codeIndexBuilder, INamedTypeSymbol symbol)
        {
            if (!symbol.IsType)
            {
                return codeIndexBuilder;
            }

            codeIndexBuilder.GetOrAdd(symbol);

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
                var logger = NullLogger.Instance;
                CancellationToken cancellationToken = CancellationToken.None;
                
                var cache = new SourceCacheContext();
                var repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
                var resource = repository.GetResourceAsync<PackageMetadataResource>().WaitForResult();

                var pkgId = new PackageIdentity(packageName, NuGetVersion.Parse(packageVersion));
                
                var package = resource.GetMetadataAsync(
                    pkgId,
                    cache,
                    logger,
                    cancellationToken).WaitForResult();

                // foreach (IPackageSearchMetadata package in packages)
                // {
                    Console.WriteLine($"Version: {package.Identity.Version}");
                    Console.WriteLine($"Listed: {package.IsListed}");
                    Console.WriteLine($"Tags: {package.Tags}");
                    Console.WriteLine($"Description: {package.Description}");
                // }

                // TODO - is this okay?
                return new[] {"netstandard2.0"};
                
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
                    .Select(dllFilePath => MetadataReference.CreateFromFile(dllFilePath)).ToList();
                
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