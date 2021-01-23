using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Xml;
using DebuggableSourceGenerators.NonLoadedAssembly;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace DebuggableSourceGenerators
{
    public class CodeIndex
    {
        Func<SyntaxTree, SemanticModel> GetSemanticModel;

        Dictionary<TypeIdentifier, IType> _types = new();

        ISyntaxService SyntaxService;
        ISymbolService SymbolService;
        private INonLoadedAssemblyService NonLoadedAssemblyService;
        private TypeRegistryServiceImpl TypeRegistryService;
        private List<Compilation> _compilations = new();

        public CodeIndex()
        {
            TypeRegistryService = new TypeRegistryServiceImpl(this);
            SymbolService = new SymbolService(TypeRegistryService);
            SyntaxService = new SyntaxService(TypeRegistryService, SymbolService, syntaxTree => GetSemanticModel(syntaxTree));
            NonLoadedAssemblyService = new NonLoadedAssemblyService(TypeRegistryService);
        }

        private class TypeRegistryServiceImpl : ITypeRegistryService
        {
            private CodeIndex _codeIndex;

            public TypeRegistryServiceImpl(CodeIndex codeIndex)
            {
                _codeIndex = codeIndex;
            }

            public Lazy<IType> GetType(TypeIdentifier identifier)
            {
                return new Lazy<IType>(_codeIndex.ResolveType(identifier));
            }

            public IType TryAddType(TypeIdentifier identifier, Func<IType> type)
            {
                return _codeIndex.TryAddType(identifier, type);
            }
        }

        public IType TryAddType(TypeIdentifier identifier, Func<IType> type)
        {
            if (!_types.ContainsKey(identifier))
            {
                _types.Add(identifier, type());
            }

            return _types[identifier];
        }

        public bool TryResolveType(TypeIdentifier identifier, out IType type)
        {
            return _types.TryGetValue(identifier, out type);
        }

        public IType ResolveType(TypeIdentifier identifier)
        {
            if (!TryResolveType(identifier, out var result))
            {
                throw new KeyNotFoundException($"Could not find type {identifier}");
            }

            return result;
        }

        public bool TryResolveType(string typeName, int arity, out IType type)
        {
            var identifier = new TypeIdentifier(typeName, arity);
            return _types.TryGetValue(identifier, out type);
        }

        public IType ResolveType(string typeName, int arity)
        {
            var identifier = new TypeIdentifier(typeName, arity);
            return ResolveType(identifier);
        }

        public bool TryResolveType(string namespaceName, string typeName, int arity, out IType type)
        {
            var identifier = new TypeIdentifier(namespaceName, typeName, arity);
            return _types.TryGetValue(identifier, out type);
        }

        public IType ResolveType(string namespaceName, string typeName, int arity)
        {
            var identifier = new TypeIdentifier(namespaceName, typeName, arity);
            return ResolveType(identifier);
        }

        public void AddNugetPackage(string packageName, string packageVersion, string targetFramework)
        {
            foreach (var assemblyFile in GetPathToNugetPacketDlls(packageName, packageVersion, targetFramework))
            {
                if (File.Exists(assemblyFile))
                {
                    AddAssemblyFile(assemblyFile);
                }
            }
        }

        public void AddProjectNugetDependencies(string projectFile)
        {
            var projectTargetFrameworks = GetProjectTargetFrameworks(projectFile).ToImmutableList();
            
            foreach (var nugetDependency in GetNugetDependencies(projectFile))
            {
                foreach (var targetFramework in projectTargetFrameworks)
                {
                    AddNugetPackage(nugetDependency.packageName, nugetDependency.packageVersion, targetFramework);
                }
            }
        }

        public void AddProjectNugetDependencies(string projectFile, string targetFramework)
        {
            var projectTargetFrameworks = GetProjectTargetFrameworks(projectFile).ToImmutableList();
            
            foreach (var nugetDependency in GetNugetDependencies(projectFile))
            {
                AddNugetPackage(nugetDependency.packageName, nugetDependency.packageVersion, targetFramework);
            }
        }

        public void AddSolution(string solutionFilePath)
        {
            var compilations = CompileSolution(solutionFilePath);
            foreach (var compilation in compilations)
            {
                AddCompilation(compilation);
            }
        }
        
        public void AddProject(string solutionFilePath, string projectName)
        {
            var compilation = CompileProject(solutionFilePath, projectName);
            AddCompilation(compilation);
        }

        public void AddAssemblyFile(string assemblyFilePath)
        {
            NonLoadedAssemblyService.AddAllTypes(assemblyFilePath);
        }

        public void AddCompilation(Compilation compilation)
        {
            _compilations.Add(compilation);
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
                 iface.Initialize(list.Key, list.Value.ToArray());
                 _types[list.Key] = iface;
             }
            
             foreach (var list in classDeclarationSyntaxes)
             {
                 var clazz = new SyntaxClass(TypeRegistryService, SyntaxService);
                 clazz.Initialize(list.Key, list.Value.ToArray());
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
            
            foreach (var reference in compilation.References)
            {
                if (File.Exists(reference.Display))
                {
                    AddAssemblyFile(reference.Display);
                }
            }
                
            foreach (var symbol in compilation.GetSymbolsWithName(_ => true, SymbolFilter.Type))
            {
                if (symbol is INamedTypeSymbol namedTypeSymbol)
                {
                    var key = SymbolService.GetTypeIdentifier(namedTypeSymbol);
                    if (!_types.ContainsKey(key))
                    {
                        SymbolService.GetType(namedTypeSymbol);                    
                    }
                }
            }
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