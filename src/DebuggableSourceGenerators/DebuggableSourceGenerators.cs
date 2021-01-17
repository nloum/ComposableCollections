using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace DebuggableSourceGenerators
{
    public static class DebuggableSourceGenerators
    {
        public static IEnumerable<string> GetPathToNugetPacketDlls(string packageName, string packageVersion,
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

        public static IEnumerable<(string packageName, string packageVersion)> GetNugetDependencies(string projectFile)
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

        public static IEnumerable<string> GetProjectTargetFrameworks(string projectFile)
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
        
        public static IEnumerable<Compilation> CompileSolution(string solutionUrl)
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
        
        public static Compilation CompileProject(string solutionUrl, string project)
        {
            return CompileSolution(solutionUrl)
                .First(compilation => compilation.AssemblyName == Path.GetFileNameWithoutExtension(project));
        }
    }
}