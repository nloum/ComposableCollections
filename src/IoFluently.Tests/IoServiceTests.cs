using System;
using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoFluently.Tests
{
    [TestClass]
    public class IoServiceTests
    {
        public enum IoServiceType
        {
            IoService,
            InMemoryWindowsIoService,
            InMemoryUnixIoService
        }
        
        private void CreateUnitsUnderTest(bool sameInstance, IoServiceType type1, bool enableOpenFilesTracking1, out IIoService unitUnderTest1, IoServiceType type2, bool enableOpenFilesTracking2, out IIoService unitUnderTest2)
        {
            unitUnderTest1 = CreateUnitUnderTest(type1, enableOpenFilesTracking1);
            
            if (type2 == type1 && sameInstance)
            {
                unitUnderTest2 = unitUnderTest1;
                return;
            }

            unitUnderTest2 = CreateUnitUnderTest(type2, enableOpenFilesTracking2);
        }

        private IIoService CreateUnitUnderTest(IoServiceType type, bool enableOpenFilesTracking)
        {
            if (type == IoServiceType.IoService)
            {
                return new IoService(enableOpenFilesTracking);
            }
            else if (type == IoServiceType.InMemoryWindowsIoService)
            {
                return new InMemoryIoService("\r\n", false, enableOpenFilesTracking);
            }
            else if (type == IoServiceType.InMemoryUnixIoService)
            {
                return new InMemoryIoService("\n", true, enableOpenFilesTracking);
            }
            else
            {
                throw new ArgumentException($"Unknown IoServiceType {type}");
            }
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void CreateTemporaryFileShouldWork(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, true);
            var temporaryPath = uut.CreateTemporaryPath(PathType.File);
            temporaryPath.IsFile().Should().BeTrue();
            temporaryPath.DeleteFile();
            temporaryPath.IsFile().Should().BeFalse();
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void CreateTemporaryFolderShouldWork(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, true);
            var temporaryPath = uut.CreateTemporaryPath(PathType.Folder);
            temporaryPath.IsFolder().Should().BeTrue();
            temporaryPath.DeleteFolder();
            temporaryPath.IsFolder().Should().BeFalse();
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void CreateTemporaryNonExistentPathShouldWork(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, true);
            var temporaryPath = uut.CreateTemporaryPath(PathType.None);
            temporaryPath.IsFolder().Should().BeFalse();
            temporaryPath.IsFile().Should().BeFalse();
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        public void SimplifyShouldNotChangeSimplePath(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, true);
            var simplified = uut.CurrentDirectory.Simplify();
            uut.CurrentDirectory.ToString().Should().Be(simplified.ToString());
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        public void HasExtensionShouldWorkWithAndWithoutTheDot(IoServiceType type) {
            var uut = CreateUnitUnderTest(type, false);
            var testTxt = uut.ParseAbsolutePath("/test.txt");
            testTxt.HasExtension(".txt").Should().BeTrue();
            testTxt.HasExtension("txt").Should().BeTrue();
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        public void WithoutExtensionsShouldWork(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var testTxt = uut.ParseAbsolutePath("/test.test.txt");
            var test = testTxt.WithoutExtension();
            test.ToString().Should().Be("/test.test");
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        public void CommonShouldOnlyReturnFullFolderNames(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);

            var parent = ioService.ParseAbsolutePath("C:\\test1\\test2");
            var item1 = parent / "test3" / "test.csproj";
            var item2 = parent / "test4";

            var result = item1.CommonWith(item2);

            result.ToString().Should().Be(parent.ToString());
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        public void ShouldNotParseWindowsAbsolutePathAsRelativePath(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);
            ioService.TryParseRelativePath("C:\\test1").HasValue.Should().BeFalse();
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        public void ShouldNotParseUnixAbsolutePathAsRelativePath(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);
            ioService.TryParseRelativePath("/test1").HasValue.Should().BeFalse();
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        public void ShouldNotParseRelativePathAsAbsolutePath(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);
            ioService.TryParseAbsolutePath("test1").HasValue.Should().BeFalse();
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        public void RelativePathShouldWork(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);

            var parent = ioService.ParseAbsolutePath("C:\\test1\\test2");
            var item1 = parent / "test3" / "test.csproj";
            var item2 = parent / "test4";

            var result = item1.RelativeTo(item2);

            result.ToString().Should().Be("..\\test3\\test.csproj");
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        public void ShouldParseWithComplexMixedDirectorySeparators(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);
            var result = ioService.ParseAbsolutePath("C:\\test1\\./test2\\");
            result.ToString().Should().Be("C:\\test1\\test2");
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        public void ShouldParseWithMixedDirectorySeparators(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);
            var result = ioService.ParseAbsolutePath("C:\\test1/test2\\");
            result.ToString().Should().Be("C:\\test1\\test2");
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void ShouldBeAbleToExecuteParentQuery(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);

            var repoRoot = ioService.CurrentDirectory.Ancestors().First(ancestor => (ancestor / ".git").IsFolder());
            var testFolder = repoRoot / "test_folder";
            var results = ioService.Query().Where(path => path.Parent() == testFolder).AsEnumerable().ToImmutableList();
            results.Count.Should().BeGreaterThan(0);
            results.Count.Should().BeLessThan(10);
            results.Any(result => result.Name == "readme.md").Should().BeTrue();
            results.Any(result => result.Name == "test_folder").Should().BeFalse();
            results.Any(result => result.Name == "subfolder_readme.md").Should().BeFalse();
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void ShouldBeAbleToExecuteAncestorQuery(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);

            var repoRoot = ioService.CurrentDirectory.Ancestors().First(ancestor => (ancestor / ".git").IsFolder());
            var testFolder = repoRoot / "test_folder";
            var results = ioService.Query().Where(path => path.Ancestors().Contains(testFolder)).AsEnumerable().ToImmutableHashSet();
            results.Count.Should().BeGreaterThan(0);
            results.Count.Should().BeLessThan(10);
            results.Any(result => result.Name == "readme.md").Should().BeTrue();
            results.Any(result => result.Name == "test_folder").Should().BeFalse();
            results.Any(result => result.Name == "subfolder_readme.md").Should().BeTrue();
        }

        [TestMethod]
        [DataRow(false, IoServiceType.IoService, true, IoServiceType.IoService, true)]
        [DataRow(false, IoServiceType.InMemoryWindowsIoService, true, IoServiceType.InMemoryWindowsIoService, true)]
        [DataRow(false, IoServiceType.InMemoryUnixIoService, true, IoServiceType.InMemoryUnixIoService, true)]
        [DataRow(true, IoServiceType.IoService, true, IoServiceType.IoService, true)]
        [DataRow(true, IoServiceType.InMemoryWindowsIoService, true, IoServiceType.InMemoryWindowsIoService, true)]
        [DataRow(true, IoServiceType.InMemoryUnixIoService, true, IoServiceType.InMemoryUnixIoService, true)]
        
        [DataRow(false, IoServiceType.IoService, false, IoServiceType.IoService, true)]
        [DataRow(false, IoServiceType.InMemoryWindowsIoService, false, IoServiceType.InMemoryWindowsIoService, true)]
        [DataRow(false, IoServiceType.InMemoryUnixIoService, false, IoServiceType.InMemoryUnixIoService, true)]
        [DataRow(true, IoServiceType.IoService, false, IoServiceType.IoService, true)]
        [DataRow(true, IoServiceType.InMemoryWindowsIoService, false, IoServiceType.InMemoryWindowsIoService, true)]
        [DataRow(true, IoServiceType.InMemoryUnixIoService, false, IoServiceType.InMemoryUnixIoService, true)]

        [DataRow(false, IoServiceType.IoService, true, IoServiceType.IoService, false)]
        [DataRow(false, IoServiceType.InMemoryWindowsIoService, true, IoServiceType.InMemoryWindowsIoService, false)]
        [DataRow(false, IoServiceType.InMemoryUnixIoService, true, IoServiceType.InMemoryUnixIoService, false)]
        [DataRow(true, IoServiceType.IoService, true, IoServiceType.IoService, false)]
        [DataRow(true, IoServiceType.InMemoryWindowsIoService, true, IoServiceType.InMemoryWindowsIoService, false)]
        [DataRow(true, IoServiceType.InMemoryUnixIoService, true, IoServiceType.InMemoryUnixIoService, false)]

        [DataRow(false, IoServiceType.IoService, false, IoServiceType.IoService, false)]
        [DataRow(false, IoServiceType.InMemoryWindowsIoService, false, IoServiceType.InMemoryWindowsIoService, false)]
        [DataRow(false, IoServiceType.InMemoryUnixIoService, false, IoServiceType.InMemoryUnixIoService, false)]
        [DataRow(true, IoServiceType.IoService, false, IoServiceType.IoService, false)]
        [DataRow(true, IoServiceType.InMemoryWindowsIoService, false, IoServiceType.InMemoryWindowsIoService, false)]
        [DataRow(true, IoServiceType.InMemoryUnixIoService, false, IoServiceType.InMemoryUnixIoService, false)]
        public void MovingShouldWork(bool sameInstance, IoServiceType type1, bool enableOpenFileTracking1, IoServiceType type2, bool enableOpenFileTracking2)
        {
            var ioService = CreateUnitUnderTest(type1, false);

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