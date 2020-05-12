using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactiveProcesses;

namespace IoFluently.Test
{
    [TestClass]
    public class IoServiceTests
    {
        private IIoService CreateUnitUnderTest()
        {
            return new IoService(new ReactiveProcessFactory());
        }

        [TestMethod]
        public void WithoutExtensionsShouldWork()
        {
            var uut = CreateUnitUnderTest();
            var testTxt = uut.ToAbsolutePath("/test.test.txt");
            var test = testTxt.WithoutExtension();
            test.ToString().Should().Be("/test.test");
        }

        [TestMethod]
        public void CommonShouldOnlyReturnFullFolderNames()
        {
            var ioService = CreateUnitUnderTest();

            var parent = ioService.ToAbsolutePath("C:\\test1\\test2");
            var item1 = parent / "test3" / "test.csproj";
            var item2 = parent / "test4";

            var result = item1.CommonWith(item2);

            result.ToString().Should().Be(parent.ToString());
        }

        [TestMethod]
        public void RelativePathShouldWork()
        {
            var ioService = CreateUnitUnderTest();

            var parent = ioService.ToAbsolutePath("C:\\test1\\test2");
            var item1 = parent / "test3" / "test.csproj";
            var item2 = parent / "test4";

            var result = item1.RelativeTo(item2);

            result.ToString().Should().Be("..\\test3\\test.csproj");
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