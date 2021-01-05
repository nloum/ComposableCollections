using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;

namespace DebuggableSourceGenerators
{
    public static class DebuggableSourceGenerators
    {
        public static IEnumerable<Compilation> CompileSolution(string solutionUrl)
        {
            MSBuildLocator.RegisterDefaults();
            using MSBuildWorkspace workspace = MSBuildWorkspace.Create();

            Solution solution = workspace.OpenSolutionAsync(solutionUrl).Result;
            ProjectDependencyGraph projectGraph = solution.GetProjectDependencyGraph();

            foreach (ProjectId projectId in projectGraph.GetTopologicallySortedProjects())
            {
                var project = solution.GetProject(projectId);
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