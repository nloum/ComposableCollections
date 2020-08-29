using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactiveProcesses;

namespace IoFluently.Tests
{
    [TestClass]
    public class IoServiceTests
    {
        [TestMethod]
        public void SimplifyShouldNotChangeSimplePath()
        {
            var uut = CreateUnitUnderTest();
            var simplified = uut.CurrentDirectory.Simplify();
            uut.CurrentDirectory.ToString().Should().Be(simplified.ToString());
        }
        
        private IIoService CreateUnitUnderTest()
        {
            return new IoService(new ReactiveProcessFactory());
        }

        [TestMethod]
        public void HasExtensionShouldWorkWithAndWithoutTheDot() {
            var uut = CreateUnitUnderTest();
            var testTxt = uut.ParseAbsolutePath("/test.txt");
            testTxt.HasExtension(".txt").Should().BeTrue();
            testTxt.HasExtension("txt").Should().BeTrue();
        }

        [TestMethod]
        public void WithoutExtensionsShouldWork()
        {
            var uut = CreateUnitUnderTest();
            var testTxt = uut.ParseAbsolutePath("/test.test.txt");
            var test = testTxt.WithoutExtension();
            test.ToString().Should().Be("/test.test");
        }

        [TestMethod]
        public void CommonShouldOnlyReturnFullFolderNames()
        {
            var ioService = CreateUnitUnderTest();

            var parent = ioService.ParseAbsolutePath("C:\\test1\\test2");
            var item1 = parent / "test3" / "test.csproj";
            var item2 = parent / "test4";

            var result = item1.CommonWith(item2);

            result.ToString().Should().Be(parent.ToString());
        }

        [TestMethod]
        public void ShouldNotParseWindowsAbsolutePathAsRelativePath()
        {
            var ioService = CreateUnitUnderTest();
            ioService.TryParseRelativePath("C:\\test1").HasValue.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldNotParseUnixAbsolutePathAsRelativePath()
        {
            var ioService = CreateUnitUnderTest();
            ioService.TryParseRelativePath("/test1").HasValue.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldNotParseRelativePathAsAbsolutePath()
        {
            var ioService = CreateUnitUnderTest();
            ioService.TryParseAbsolutePath("test1").HasValue.Should().BeFalse();
        }

        [TestMethod]
        public void RelativePathShouldWork()
        {
            var ioService = CreateUnitUnderTest();

            var parent = ioService.ParseAbsolutePath("C:\\test1\\test2");
            var item1 = parent / "test3" / "test.csproj";
            var item2 = parent / "test4";

            var result = item1.RelativeTo(item2);

            result.ToString().Should().Be("..\\test3\\test.csproj");
        }

        [TestMethod]
        public void ShouldParseWithComplexMixedDirectorySeparators()
        {
            var ioService = CreateUnitUnderTest();
            var result = ioService.ParseAbsolutePath("C:\\test1\\./test2\\");
            result.ToString().Should().Be("C:\\test1\\test2");
        }
        
        [TestMethod]
        public void ShouldParseWithMixedDirectorySeparators()
        {
            var ioService = CreateUnitUnderTest();
            var result = ioService.ParseAbsolutePath("C:\\test1/test2\\");
            result.ToString().Should().Be("C:\\test1\\test2");
        }

        [TestMethod]
        public void MovingShouldWork()
        {
            var ioService = CreateUnitUnderTest();

            var sourceFolder = ioService.CurrentDirectory / "test1";

            sourceFolder.CreateFolder()
                .ClearFolder();

            var textFileInSourceFolder = sourceFolder / "test.txt";

            textFileInSourceFolder.WriteText("testing 1 2 3");

            var targetFolder = (ioService.CurrentDirectory / "test2")
                .CreateFolder()
                .ClearFolder();

            var textFileMovePlan = textFileInSourceFolder.Translate(sourceFolder, targetFolder);

            textFileInSourceFolder.Exists().Should().BeTrue();
            textFileMovePlan.Destination.Exists().Should().BeFalse();
            
            textFileMovePlan.Move();

            textFileInSourceFolder.Exists().Should().BeFalse();
            textFileMovePlan.Destination.Exists().Should().BeTrue();
        }
    }
}