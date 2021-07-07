using System.Linq;
using CodeIO.SourceCode.Read;
using CodeIO.UnloadedAssemblies.Read;
using IoFluently;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeIO.Tests.CSharp.Read
{
    [TestClass]
    public class TypeReaderTests
    {
        [TestMethod]
        public void ShouldParseCodeIOProject()
        {
            var ioService = new IoService();

            var repoRoot = ioService.DefaultRelativePathBase.Ancestors
                .First(ancestor => (ancestor / ".git").Exists);

            var solutionFilePath = repoRoot / "src" / "CodeIO.sln";
            
            
            var uut = new TypeReader();
            uut.AddSupportForSourceCode();
        }
    }
}