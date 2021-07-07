using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using CodeIO.SourceCode.Read;
using Humanizer;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Mono.Cecil;
using NuGet.Common;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace CodeIO.UnloadedAssemblies.Read
{
    public static class Extensions
    {
        static Extensions()
        {
            MSBuildLocator.RegisterDefaults();
        }
        
        public static TypeReader AddSupportForUnloadedAssemblies(this TypeReader typeReader)
        {
            return typeReader.AddTypeFormat<TypeDefinition>(type => type.GetTypeIdentifier(), (typeDefinition, lazyTypes) =>
            {
                throw new NotImplementedException($"No support for {typeDefinition.GetTypeIdentifier().ToString().Pluralize()} yet");
            });
        }

        public static TypeReader AddSolution(this TypeReader typeReader, string solutionFilePath, Func<string, bool> projectAssemblyNamePredicate = null)
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
                        typeReader.AddCompilation(projectCompilation);
                        typeReader.AddProjectNugetDependencies(project.FilePath);

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
                        //         typeReader.AddType(typeDefinition);
                        //     }
                        // }
                    }
                }
            }
            
            return typeReader;
        }
        
        public static TypeReader AddUnloadedAssemblies(this TypeReader typeReader)
        {
            return typeReader.AddTypeFormat<TypeDefinition>(type => type.GetTypeIdentifier(), (type, lazyTypes) =>
            {
                throw new NotImplementedException($"Cannot process type: {type.GetTypeIdentifier()}");
            });
        }
        
        public static TypeReader AddNugetPackage(this TypeReader typeReader, string packageName, string packageVersion, string targetFramework)
        {
            foreach (var assemblyFile in GetPathToNugetPackageDlls(packageName, packageVersion, targetFramework))
            {
                if (File.Exists(assemblyFile))
                {
                    typeReader.AddAssemblyFile(assemblyFile);
                }
            }

            return typeReader;
        }

        public static TypeReader AddProjectNugetDependencies(this TypeReader typeReader, string projectFile)
        {
            var projectTargetFrameworks = GetProjectTargetFrameworks(projectFile).ToImmutableList();
            
            foreach (var nugetDependency in GetNugetDependencies(projectFile))
            {
                foreach (var targetFramework in projectTargetFrameworks)
                {
                    typeReader.AddNugetPackage(nugetDependency.packageName, nugetDependency.packageVersion, targetFramework);
                }
            }

            return typeReader;
        }

        public static TypeReader AddProjectNugetDependencies(this TypeReader typeReader, string projectFile, string targetFramework)
        {
            var projectTargetFrameworks = GetProjectTargetFrameworks(projectFile).ToImmutableList();
            
            foreach (var nugetDependency in GetNugetDependencies(projectFile))
            {
                typeReader.AddNugetPackage(nugetDependency.packageName, nugetDependency.packageVersion, targetFramework);
            }

            return typeReader;
        }

        public static TypeReader AddAssemblyFile(this TypeReader typeReader, string assemblyFilePath)
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
                
                typeReader.Add(typeDefinition);
            }

            return typeReader;
        }

        public static TypeIdentifier GetTypeIdentifier(this TypeDefinition typeDefinition)
        {
            return TypeIdentifier.Parse(typeDefinition.Namespace, typeDefinition.Name);
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
                var resourceTask = repository.GetResourceAsync<PackageMetadataResource>();
                resourceTask.Wait();
                var resource = resourceTask.Result;

                var pkgId = new PackageIdentity(packageName, NuGetVersion.Parse(packageVersion));
                
                var packageTask = resource.GetMetadataAsync(
                    pkgId,
                    cache,
                    logger,
                    cancellationToken);
                packageTask.Wait();
                var package = packageTask.Result;

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