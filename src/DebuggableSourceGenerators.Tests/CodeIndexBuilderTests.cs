using System.Linq;
using FluentAssertions;
using IoFluently;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMonads;

namespace DebuggableSourceGenerators.Tests
{
    [TestClass]
    public class CodeIndexBuilderTests
    {
        [TestMethod]
        public void ShouldIndexSourceInCSharpProject()
        {
            var uut = new CodeIndexBuilder();
            var ioService = new IoService();

            var repoRoot = ioService.CurrentDirectory.Ancestors().First(x => (x / ".git").IsFolder());
            var solutionPath = repoRoot / "src" / "FluentSourceGenerators.sln";
            var debuggableSourceGeneratorsProjectPath =
                repoRoot / "src" / "DebuggableSourceGenerators" / "DebuggableSourceGenerators.csproj";

            uut.AddProject(solutionPath, debuggableSourceGeneratorsProjectPath);

            var codeIndex = uut.Build();

            var codeIndexTypeId = new TypeIdentifier()
            {
                Namespace = "DebuggableSourceGenerators",
                Name = "CodeIndex",
                Arity = 0
            };
            
            codeIndex.ContainsKey(codeIndexTypeId).Should().BeTrue();

            var codeIndexType = codeIndex[codeIndexTypeId];

            codeIndexType.Should().NotBeNull();

            var codeIndexClass = codeIndexType as Class;
            
            codeIndexClass.Should().NotBeNull();

            codeIndexClass.Constructors.Count.Should().Be(1);
            codeIndexClass.Fields.Count.Should().Be(2);
            codeIndexClass.Indexers.Count.Should().Be(1);
            codeIndexClass.Properties.Count.Should().Be(3);
            codeIndexClass.Methods.Count.Should().Be(6);
        }
        
        [TestMethod]
        public void ShouldIndexNugetPackageFromCSharpProject()
        {
            var uut = new CodeIndexBuilder();
            var ioService = new IoService();

            var repoRoot = ioService.CurrentDirectory.Ancestors().First(x => (x / ".git").IsFolder());
            var solutionPath = repoRoot / "src" / "FluentSourceGenerators.sln";
            var debuggableSourceGeneratorsProjectPath =
                repoRoot / "src" / "DebuggableSourceGenerators" / "DebuggableSourceGenerators.csproj";

            uut.AddProject(solutionPath, debuggableSourceGeneratorsProjectPath);

            var codeIndex = uut.Build();

            var queryableTypeId = new TypeIdentifier()
            {
                Namespace = "ComposableCollections.Utilities",
                Name = "Queryable",
                Arity = 1
            };

            codeIndex.ContainsKey(queryableTypeId).Should().BeTrue();

            var queryableType = codeIndex[queryableTypeId];

            queryableType.Should().NotBeNull();

            var codeIndexClass = queryableType as Class;
            
            codeIndexClass.Should().NotBeNull();

            codeIndexClass.Constructors.Count.Should().Be(1);
            codeIndexClass.Fields.Count.Should().Be(2);
            codeIndexClass.Indexers.Count.Should().Be(0);
            codeIndexClass.Properties.Count.Should().Be(3);
            codeIndexClass.Methods.Count.Should().Be(2);
        }
    }
}