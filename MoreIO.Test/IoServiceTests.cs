using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactiveProcesses;

namespace MoreIO.Test
{
    [TestClass]
    public class IoServiceTests
    {
        private IIoService CreateUnitUnderTest()
        {
            return new IoService(new ReactiveProcessFactory());
        }

        [TestMethod]
        public void CommonShouldOnlyReturnFullFolderNames()
        {
            var ioService = CreateUnitUnderTest();

            var parent = ioService.ToAbsolutePath("C:\\test1\\test2").Value;
            var item1 = parent / "test3" / "test.csproj";
            var item2 = parent / "test4";

            var result = item1.CommonWith(item2).Value;

            result.ToString().Should().Be(parent.ToString());
        }

        [TestMethod]
        public void RelativePathShouldWork()
        {
            var ioService = CreateUnitUnderTest();

            var parent = ioService.ToAbsolutePath("C:\\test1\\test2").Value;
            var item1 = parent / "test3" / "test.csproj";
            var item2 = parent / "test4";

            var result = item1.RelativeTo(item2);

            result.ToString().Should().Be("..\\test3\\test.csproj");
        }
        
        [TestMethod]
        public void MovingShouldWork()
        {
            var ioService = CreateUnitUnderTest();

            var test1 = ioService.ToAbsolutePath("test1").Value;

            test1.CreateFolder()
                .ClearFolder();

            var text1 = test1.Descendant("test.txt").Value;

            text1.WriteText("testing 1 2 3");

            var test2 = ioService.ToAbsolutePath("test2").Value
                .CreateFolder()
                .ClearFolder();

            var text2 = text1.Translate(test1, test2).Move();

            text1.Exists().Should().BeFalse();
            text2.Destination.Exists().Should().BeTrue();
        }

        [TestMethod]
        public void MovingShouldPromptChangeDetection()
        {
            var ioService = CreateUnitUnderTest();

            var test1 = ioService.ToAbsolutePath("test1").Value;

            test1.CreateFolder()
                .ClearFolder();

            var text1 = test1.Descendant("test.txt").Value;

            text1.WriteText("testing 1 2 3");

            var test2 = ioService.ToAbsolutePath("test2").Value
                .CreateFolder()
                .ClearFolder();

            var text2 = text1.Translate(test1, test2).Move();

            text1.Exists().Should().BeFalse();
            text2.Destination.Exists().Should().BeTrue();
        }
    }
}