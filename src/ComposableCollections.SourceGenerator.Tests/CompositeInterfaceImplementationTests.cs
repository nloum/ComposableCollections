using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComposableCollections.SourceGenerator.Tests
{
    [TestClass]
    public class CompositeInterfaceImplementationTests
    {
        [TestMethod]
        public void ShouldGenerateInterface()
        {
            const string projectPath = @"C:\HelloWorldApplication\HelloWorldProject.csproj";

            // Creating a build workspace.
            var workspace = MSBuildWorkspace.Create();

            // Opening the Hello World project.
            var project = workspace.OpenProjectAsync(projectPath).Result;

            // Getting the compilation.
            var compilation = project.GetCompilationAsync().Result;

        }
    }
}
