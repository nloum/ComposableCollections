using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace DebuggableSourceGenerators
{
    public class CodeIndexBuilder
    {
        Func<SyntaxTree, SemanticModel> GetSemanticModel;

        Dictionary<TypeIdentifier, IType> _types = new Dictionary<TypeIdentifier, IType>();

        ISyntaxService SyntaxService;
        ISymbolService SymbolService;
        private TypeRegistryServiceImpl TypeRegistryService;

        public CodeIndexBuilder()
        {
            TypeRegistryService = new TypeRegistryServiceImpl(this);
            SymbolService = new SymbolService(TypeRegistryService);
            SyntaxService = new SyntaxService(TypeRegistryService, SymbolService, syntaxTree => GetSemanticModel(syntaxTree));
        }

        public CodeIndex Build()
        {
            SymbolService.LoadTypesFromAssemblies();
            return new CodeIndex(this);
        }

        private class TypeRegistryServiceImpl : ITypeRegistryService
        {
            private CodeIndexBuilder CodeIndexBuilder;

            public TypeRegistryServiceImpl(CodeIndexBuilder codeIndexBuilder)
            {
                CodeIndexBuilder = codeIndexBuilder;
            }

            public Lazy<IType> GetType(string typeName, int arity = 0)
            {
                return CodeIndexBuilder.GetType(typeName, arity);
            }

            public Lazy<IType> GetType(string namespaceName, string typeName, int arity = 0)
            {
                return CodeIndexBuilder.GetType(namespaceName, typeName, arity);
            }

            public IType TryAddType(string typeName, int arity, Func<IType> type)
            {
                return CodeIndexBuilder.TryAddType(typeName, arity, type);
            }

            public IType TryAddType(string namespaceName, string typeName, int arity, Func<IType> type)
            {
                return CodeIndexBuilder.TryAddType(namespaceName, typeName, arity, type);
            }
        }

        public IType TryAddType(string typeName, int arity, Func<IType> type)
        {
            var typeIdentifier = new TypeIdentifier(typeName, arity);
            if (!_types.ContainsKey(typeIdentifier))
            {
                _types.Add(typeIdentifier, type());
            }

            return _types[typeIdentifier];
        }

        public IType TryAddType(string namespaceName, string typeName, int arity, Func<IType> type)
        {
            if (!string.IsNullOrWhiteSpace(namespaceName))
            {
                typeName = $"{namespaceName}.{typeName}";
            }

            return TryAddType(typeName, arity, type);
        }

        public Lazy<IType> GetType(string typeName, int arity = 0)
        {
            var typeIdentifier = new TypeIdentifier(typeName, arity);
            return new Lazy<IType>(() => _types[typeIdentifier]);
        }

        public Lazy<IType> GetType(string namespaceName, string typeName, int arity = 0)
        {
            if (!string.IsNullOrWhiteSpace(namespaceName))
            {
                typeName = $"{namespaceName}.{typeName}";
            }

            return GetType(typeName, arity);
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

        public CodeIndexBuilder AddSolution(string solutionFilePath)
        {
            var compilations = CompileSolution(solutionFilePath);
            foreach (var compilation in compilations)
            {
                Add(compilation);
            }
        }
        
        public CodeIndexBuilder AddProject(string solutionFilePath, string projectName)
        {
            var compilation = CompileProject(solutionFilePath, projectName);
            Add(compilation);
        }

        public CodeIndexBuilder Add(Compilation compilation)
        {
            GetSemanticModel = syntaxTree => compilation.GetSemanticModel(syntaxTree);

            var interfaceDeclarationSyntaxes = new Dictionary<TypeIdentifier, List<InterfaceDeclarationSyntax>>();
            var classDeclarationSyntaxes = new Dictionary<TypeIdentifier, List<ClassDeclarationSyntax>>();

            foreach (var syntaxTree in compilation.SyntaxTrees)
            {
                Utilities.TraverseTree(syntaxTree.GetRoot(), node =>
                {
                    if (node is InterfaceDeclarationSyntax interfaceDeclarationSyntax)
                    {
                        var typeIdentifier = new TypeIdentifier(Utilities.GetNamespace(interfaceDeclarationSyntax),
                            interfaceDeclarationSyntax.Identifier.Text,
                            interfaceDeclarationSyntax.TypeParameterList == null
                                ? 0
                                : interfaceDeclarationSyntax.TypeParameterList.Parameters.Count);

                        if (!interfaceDeclarationSyntaxes.TryGetValue(typeIdentifier, out var items))
                        {
                            items = new List<InterfaceDeclarationSyntax>();
                            interfaceDeclarationSyntaxes[typeIdentifier] = items;
                        }

                        items.Add(interfaceDeclarationSyntax);
                    }
                    else if (node is ClassDeclarationSyntax classDeclarationSyntax)
                    {
                        var typeIdentifier = new TypeIdentifier(
                            Utilities.GetNamespace(classDeclarationSyntax),
                            classDeclarationSyntax.Identifier.Text,
                            classDeclarationSyntax.TypeParameterList == null
                                ? 0
                                : classDeclarationSyntax.TypeParameterList.Parameters.Count);
                        if (!classDeclarationSyntaxes.TryGetValue(typeIdentifier, out var items))
                        {
                            items = new List<ClassDeclarationSyntax>();
                            classDeclarationSyntaxes[typeIdentifier] = items;
                        }

                        items.Add(classDeclarationSyntax);
                    }
                });
            }

            foreach (var list in interfaceDeclarationSyntaxes)
            {
                var iface = new SyntaxInterface(TypeRegistryService, SyntaxService);
                iface.Initialize(list.Value.ToArray());
                _types[list.Key] = iface;
            }

            foreach (var list in classDeclarationSyntaxes)
            {
                var clazz = new SyntaxClass(TypeRegistryService, SyntaxService);
                clazz.Initialize(list.Value.ToArray());
                _types[list.Key] = clazz;
            }

            // foreach (var reference in compilation.References)
            // {
            //     if (reference.GetType().Name == "MetadataImageReference")
            //     {
            //         var filePathProperty = reference.GetType().GetProperty("FilePath");
            //         var filePath = (string)filePathProperty.GetValue(reference);
            //         var assembly = Assembly.LoadFile(filePath);
            //         assembly.DefinedTypes
            //         Console.WriteLine(filePath);
            //     }
            // }
        }
    }
}