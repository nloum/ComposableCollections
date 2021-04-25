using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Mono.Cecil;

namespace DebuggableSourceGenerators
{
    public static class Extensions
    {
        public static void AddNugetPackage(this CodeIndexBuilder codeIndexBuilder, string packageName, string packageVersion, string targetFramework)
        {
            foreach (var assemblyFile in GetPathToNugetPacketDlls(packageName, packageVersion, targetFramework))
            {
                if (File.Exists(assemblyFile))
                {
                    codeIndexBuilder.AddAssemblyFile(assemblyFile);
                }
            }
        }

        public static void AddProjectNugetDependencies(this CodeIndexBuilder codeIndexBuilder, string projectFile)
        {
            var projectTargetFrameworks = GetProjectTargetFrameworks(projectFile).ToImmutableList();
            
            foreach (var nugetDependency in GetNugetDependencies(projectFile))
            {
                foreach (var targetFramework in projectTargetFrameworks)
                {
                    codeIndexBuilder.AddNugetPackage(nugetDependency.packageName, nugetDependency.packageVersion, targetFramework);
                }
            }
        }

        public static void AddProjectNugetDependencies(this CodeIndexBuilder codeIndexBuilder, string projectFile, string targetFramework)
        {
            var projectTargetFrameworks = GetProjectTargetFrameworks(projectFile).ToImmutableList();
            
            foreach (var nugetDependency in GetNugetDependencies(projectFile))
            {
                codeIndexBuilder.AddNugetPackage(nugetDependency.packageName, nugetDependency.packageVersion, targetFramework);
            }
        }

        public static void AddSolution(this CodeIndexBuilder codeIndexBuilder, string solutionFilePath)
        {
            var compilations = CompileSolution(solutionFilePath);
            foreach (var compilation in compilations)
            {
                codeIndexBuilder.AddCompilation(compilation);
            }
        }
        
        public static void AddProject(this CodeIndexBuilder codeIndexBuilder, string solutionFilePath, string projectName)
        {
            var compilation = CompileProject(solutionFilePath, projectName);
            codeIndexBuilder.AddCompilation(compilation);
        }

        public static void AddAssemblyFile(this CodeIndexBuilder codeIndexBuilder, string assemblyFilePath)
        {
            var types = AssemblyDefinition
                .ReadAssembly(assemblyFilePath)
                .MainModule
                .Types;

            foreach (var type in types)
            {
                codeIndexBuilder.AddType(type);
            }
        }

        public static Lazy<Type> GetOrAdd(this CodeIndexBuilder codeIndexBuilder, TypeDefinition typeDefinition)
        {
            var identifier = new TypeIdentifier
            {
                Namespace = typeDefinition.Namespace,
                Name = typeDefinition.Name,
                Arity = typeDefinition.GenericParameters.Count
            };
            
            if (typeDefinition.IsClass)
            {
                return codeIndexBuilder.GetOrAdd(identifier, () => new Class()
                {
                    Identifier = identifier,
                    Properties = typeDefinition.Properties.Select(prop => new Property
                    {
                        Name = prop.Name,
                        Type = codeIndexBuilder.GetOrAdd(prop.PropertyType.Resolve())
                    }).ToImmutableList(),
                    Methods = typeDefinition.Methods.Select(meth => new Method()
                    {
                        Name = meth.Name,
                        ReturnType = codeIndexBuilder.GetOrAdd(meth.ReturnType.Resolve()),
                        Parameters = meth.Parameters.Select(param => new Parameter()
                        {
                            Name = param.Name,
                            Type = codeIndexBuilder.GetOrAdd(param.ParameterType.Resolve())
                        }).ToImmutableList(),
                    }).ToImmutableList()
                });
            }
            else if (typeDefinition.IsInterface)
            {
                return codeIndexBuilder.GetOrAdd(identifier, () => new Interface()
                {
                    Identifier = identifier,
                    Properties = typeDefinition.Properties.Select(prop => new Property
                    {
                        Name = prop.Name,
                        Type = codeIndexBuilder.GetOrAdd(prop.PropertyType.Resolve())
                    }).ToImmutableList(),
                    Methods = typeDefinition.Methods.Select(meth => new Method()
                    {
                        Name = meth.Name,
                        ReturnType = codeIndexBuilder.GetOrAdd(meth.ReturnType.Resolve()),
                        Parameters = meth.Parameters.Select(param => new Parameter()
                        {
                            Name = param.Name,
                            Type = codeIndexBuilder.GetOrAdd(param.ParameterType.Resolve())
                        }).ToImmutableList(),
                    }).ToImmutableList()
                });
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        
        public static void AddType(this CodeIndexBuilder codeIndexBuilder, TypeDefinition typeDefinition)
        {
            codeIndexBuilder.GetOrAdd(typeDefinition);
            
            foreach (var nestedType in typeDefinition.NestedTypes)
            {
                codeIndexBuilder.AddType(nestedType);
            }
        }

        public static void AddCompilation(this CodeIndexBuilder codeIndexBuilder, Compilation compilation)
        {
            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                var root = syntaxTree.GetRoot();
                codeIndexBuilder.AddSyntaxNode(root, semanticModel);
            }
        }

        public static void AddSyntaxNode(this CodeIndexBuilder codeIndexBuilder, SyntaxNode syntaxNode, SemanticModel semanticModel)
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
        }

        public static void AddSymbol(this CodeIndexBuilder codeIndexBuilder, INamedTypeSymbol symbol)
        {
            if (!symbol.IsType)
            {
                return;
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
        }
        
        private static IEnumerable<string> GetPathToNugetPacketDlls(string packageName, string packageVersion,
            string targetFramework)
        {
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix || 
                               Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            var packageFolder = Path.Combine(homePath, ".nuget", "packages", packageName, packageVersion, "lib", targetFramework);

            if (Directory.Exists(packageFolder))
            {
                return Directory.GetFiles(packageFolder, "*.dll")
                    .Select(dllFileName => Path.Combine(packageFolder, dllFileName));
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
            MSBuildLocator.RegisterDefaults();
            using MSBuildWorkspace workspace = MSBuildWorkspace.Create();

            Solution solution = workspace.OpenSolutionAsync(solutionUrl).Result;
            ProjectDependencyGraph projectGraph = solution.GetProjectDependencyGraph();

            foreach (ProjectId projectId in projectGraph.GetTopologicallySortedProjects())
            {
                var project = solution.GetProject(projectId);

                var additionalMetadataReferences = GetNugetDependencies(project.FilePath)
                    .SelectMany(nugetDependency =>
                    {
                        return GetProjectTargetFrameworks(project.FilePath)
                            .SelectMany(targetFramework => GetPathToNugetPacketDlls(nugetDependency.packageName,
                                nugetDependency.packageVersion, targetFramework));
                    })
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
        
        private static Compilation CompileProject(string solutionFilePath, string projectName)
        {
            return CompileSolution(solutionFilePath)
                .First(compilation => compilation.AssemblyName == Path.GetFileNameWithoutExtension(projectName));
        }
    }
}