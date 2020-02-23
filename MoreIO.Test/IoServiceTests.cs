using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoreIO.Test
{
    [TestClass]
    public class IoServiceTests
    {
        private IIoService CreateUnitUnderTest()
        {
            return new IoService();
        }

        [TestMethod]
        public void MovingShouldWork()
        {
            var ioService = CreateUnitUnderTest();

            var test1 = ioService.ToPath("test1").Value;

            test1.CreateFolder()
                .ClearFolder();

            var text1 = test1.Descendant("test.txt").Value;

            text1.WriteText("testing 1 2 3");

            var test2 = ioService.ToPath("test2").Value
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

            var test1 = ioService.ToPath("test1").Value;

            test1.CreateFolder()
                .ClearFolder();

            var text1 = test1.Descendant("test.txt").Value;

            text1.WriteText("testing 1 2 3");

            var test2 = ioService.ToPath("test2").Value
                .CreateFolder()
                .ClearFolder();

            var text2 = text1.Translate(test1, test2).Move();

            text1.Exists().Should().BeFalse();
            text2.Destination.Exists().Should().BeTrue();
        }
    }
}