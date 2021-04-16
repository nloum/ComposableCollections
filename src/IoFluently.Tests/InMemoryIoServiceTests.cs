using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoFluently.Tests
{
    [TestClass]
    public class InMemoryIoServiceTests
    {
        [TestMethod]
        public void ShouldAllowLinuxStylePaths()
        {
            var uut = new InMemoryIoService("\n", true, "/");
            uut.RootFolders.Add("/", new InMemoryIoService.Folder());
            var root = uut.ParseAbsolutePath("/");
            root.IsFolder().Should().Be(true);
            root.ToString().Should().Be("/");
            var testTxt = uut.ParseAbsolutePath("/test1.txt");
            testTxt.ToString().Should().Be("/test1.txt");
            testTxt.Parent().ToString().Should().Be("/");
        }

        [TestMethod]
        public void ShouldBeAbleToWriteToAndReadFromFiles()
        {
            var uut = new InMemoryIoService("\n", true, "/");
            uut.RootFolders.Add("/", new InMemoryIoService.Folder());
            var testTxt = uut.ParseAbsolutePath("/test1.txt");
            testTxt.WriteAllText("testcontents");
            testTxt.ReadAllText().Should().Be("testcontents");
        }
    }
}