using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using IoFluently.SystemTextJson;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyMSTest;

namespace IoFluently.Tests
{
    [TestClass]
    public class FileSystemTests : VerifyBase
    {
        public enum IoServiceType
        {
            IoService,
            InMemoryWindowsIoService,
            InMemoryUnixIoService,
            InMemoryZipIoService,
        }
        
        private void CreateUnitsUnderTest(bool sameInstance, IoServiceType type1, bool enableOpenFilesTracking1, out IFileSystem unitUnderTest1, IoServiceType type2, bool enableOpenFilesTracking2, out IFileSystem unitUnderTest2)
        {
            unitUnderTest1 = CreateUnitUnderTest(type1, enableOpenFilesTracking1);
            
            if (type2 == type1 && sameInstance)
            {
                unitUnderTest2 = unitUnderTest1;
                return;
            }

            unitUnderTest2 = CreateUnitUnderTest(type2, enableOpenFilesTracking2);
        }
        
        private IEnvironment CreateUnitUnderTest(IoServiceType type, bool enableOpenFilesTracking)
        {
            if (type == IoServiceType.IoService)
            {
                return new LocalFileSystem(enableOpenFilesTracking);
            }
            else if (type == IoServiceType.InMemoryWindowsIoService)
            {
                var result = new InMemoryFileSystem(false, "/", enableOpenFilesTracking);
                result.RootFolders.Add("/", new InMemoryFolder());
                var finalResult = new FileSystemEnvironmentDecorator(result);
                finalResult.CurrentDirectory = result.ParseAbsolutePath("/").ExpectFolder();
                finalResult.TemporaryFolder = result.ParseAbsolutePath("/tmp").ExpectFolder();
                return finalResult;
            }
            else if (type == IoServiceType.InMemoryUnixIoService)
            {
                var result = new InMemoryFileSystem(true, "/", enableOpenFilesTracking);
                result.RootFolders.Add("/", new InMemoryFolder());
                var finalResult = new FileSystemEnvironmentDecorator(result);
                finalResult.CurrentDirectory = result.ParseAbsolutePath("/").ExpectFolder();
                finalResult.TemporaryFolder = result.ParseAbsolutePath("/tmp").ExpectFolder();
                return finalResult;
            }
            else if (type == IoServiceType.InMemoryZipIoService)
            {
                var inMemoryFileSystem = new InMemoryFileSystem(true, "/", enableOpenFilesTracking);
                inMemoryFileSystem.RootFolders.Add("/", new InMemoryFolder());
                var testZipFilePath = inMemoryFileSystem.ParseAbsolutePath("/test.zip").ExpectFileOrMissingPath();
                var result = testZipFilePath.ExpectZipFileOrMissingPath(true);
                var finalResult = new FileSystemEnvironmentDecorator(result);
                finalResult.CurrentDirectory = result.ParseAbsolutePath("/").ExpectFolder();
                finalResult.TemporaryFolder = result.ParseAbsolutePath("/tmp").ExpectFolder();
                return finalResult;
            }
            else
            {
                throw new ArgumentException($"Unknown IoServiceType {type}");
            }
        }

        [TestMethod]
        public void ExpectingAFileCreatedExternallyToBeAFileShouldNotFail()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, false);
            var path = uut.GenerateUniqueTemporaryPath();
            File.WriteAllText(path.FullName, "Hello 1 2 3");
            Action action = () => path.ExpectFile();
            action.Should().NotThrow();
            File.Delete(path.FullName);
            action = () => path.ExpectMissingPath();
            action.Should().NotThrow();
        }

        [TestMethod]
        public void ExpectingAFolderCreatedExternallyToBeAFolderShouldNotFail()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, false);
            var path = uut.GenerateUniqueTemporaryPath();
            Directory.CreateDirectory(path.FullName);
            Action action = () => path.ExpectFolder();
            action.Should().NotThrow();
            Directory.Delete(path.FullName);
            action = () => path.ExpectMissingPath();
            action.Should().NotThrow();
        }
        
        [TestMethod]
        public void ExpectingAMissingPathToBeAMissingPathShouldNotFail()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, false);
            var path = uut.GenerateUniqueTemporaryPath();
            Action action = () => path.ExpectMissingPath();
            action.Should().NotThrow();
        }

        [TestMethod]
        public void ExpectingAMissingPathToBeAFileShouldFail()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, false);
            var path = uut.GenerateUniqueTemporaryPath();
            Action action = () => path.ExpectFile();
            action.Should().Throw<WrongPathTypeException>();
        }
        
        [TestMethod]
        public void ExpectingAMissingPathToBeAFolderShouldFail()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, false);
            var path = uut.GenerateUniqueTemporaryPath();
            Action action = () => path.ExpectFolder();
            action.Should().Throw<WrongPathTypeException>();
        }

        [TestMethod]
        public void ExpectingAFileToBeAFileShouldNotFail()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, false);
            var path = uut.GenerateUniqueTemporaryPath()
                .ExpectTextFileOrMissingPath()
                .WriteAllText("Test 1 2 3");
            Action action = () => path.ExpectFile();
            action.Should().NotThrow();
        }

        [TestMethod]
        public void ExpectingAFileToBeAMissingPathShouldFail()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, false);
            var path = uut.GenerateUniqueTemporaryPath()
                .ExpectTextFileOrMissingPath()
                .WriteAllText("Test 1 2 3");
            Action action = () => path.ExpectMissingPath();
            action.Should().Throw<WrongPathTypeException>();
        }
        
        [TestMethod]
        public void ExpectingAFileToBeAFolderShouldFail()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, false);
            var path = uut.GenerateUniqueTemporaryPath()
                .ExpectTextFileOrMissingPath()
                .WriteAllText("Test 1 2 3");
            Action action = () => path.ExpectFolder();
            action.Should().Throw<WrongPathTypeException>();
        }
        
        [TestMethod]
        public void ExpectingAFolderToBeAFileShouldFail()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, false);
            var path = uut.GenerateUniqueTemporaryPath()
                .EnsureIsFolder();
            Action action = () => path.ExpectFile();
            action.Should().Throw<WrongPathTypeException>();
        }

        [TestMethod]
        public void ExpectingAFolderToBeAMissingPathShouldFail()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, false);
            var path = uut.GenerateUniqueTemporaryPath()
                .EnsureIsFolder();
            Action action = () => path.ExpectMissingPath();
            action.Should().Throw<WrongPathTypeException>();
        }
        
        [TestMethod]
        public void ExpectingAFolderToBeAFolderShouldNotFail()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, false);
            var path = uut.GenerateUniqueTemporaryPath()
                .EnsureIsFolder();
            Action action = () => path.ExpectFolder();
            action.Should().NotThrow();
        }

        [TestMethod]
        public async Task FindChildrenWithoutPatternShouldWork()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, true);
            var repoRoot = uut.CurrentDirectory.Ancestors.First(ancestor => (ancestor / ".git").Exists);
            var testFolder = (repoRoot / "test_folder").ExpectFolder();
            var children = testFolder.Children.Select(x => x.ToJsonDto()).ToList();
            await Verify(children);
        }
        
        [TestMethod]
        public async Task FindDescendantsWithoutPatternShouldWork()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, true);
            var repoRoot = uut.CurrentDirectory.Ancestors.First(ancestor => (ancestor / ".git").Exists);
            var testFolder = (repoRoot / "test_folder").ExpectFolder();
            var children = testFolder.Descendants.Select(x => x.ToJsonDto()).ToList();
            await Verify(children);
        }

        [TestMethod]
        public async Task FindChildrenByPatternShouldWork()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, true);
            var repoRoot = uut.CurrentDirectory.Ancestors.First(ancestor => (ancestor / ".git").Exists);
            var testFolder = (repoRoot / "test_folder").ExpectFolder();
            var children = testFolder.Children("*.md").Select(x => x.ToJsonDto()).ToList();
            await Verify(children);
        }
        
        [TestMethod]
        public async Task FindDescendantsByPatternShouldWork()
        {
            var uut = CreateUnitUnderTest(IoServiceType.IoService, true);
            var repoRoot = uut.CurrentDirectory.Ancestors.First(ancestor => (ancestor / ".git").Exists);
            var testFolder = (repoRoot / "test_folder").ExpectFolder();
            var children = testFolder.Descendants("*.md").Select(x => x.ToJsonDto()).ToList();
            await Verify(children);
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void RootsShouldNotBeEmptyByDefault(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, true);
            uut.Roots.Count.Should().NotBe(0);
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void ShouldCreateFullPath(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, true);
            var testFolder = uut.CurrentDirectory / "test1" / "test2" / "test3";
            testFolder.EnsureIsFolder();
            testFolder.IsFolder.Should().BeTrue();
            (uut.CurrentDirectory / "test1").EnsureIsNotFolder(true);
            (uut.CurrentDirectory / "test1").Exists.Should().BeFalse();
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void WriteAllTextCreateRecursivelyShouldWork(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, true);
            var temporaryPath = (uut.GenerateUniqueTemporaryPath().ExpectFileOrFolderOrMissingPath() / "test1"  / "test2")
                .ExpectTextFileOrMissingPath()
                .WriteAllText("", createRecursively: true);
            temporaryPath.ExpectFile().DeleteFileAsync(CancellationToken.None).Wait();
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void CreateTemporaryFileShouldWork(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, true);
            var temporaryPath = uut.GenerateUniqueTemporaryPath()
                .ExpectTextFileOrMissingPath()
                .WriteAllText("");
            temporaryPath.ExpectFile().DeleteFileAsync(CancellationToken.None).Wait();
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void CreateTemporaryFolderShouldWork(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, true);
            var temporaryPath = uut.GenerateUniqueTemporaryPath()
                .CreateFolder();
            temporaryPath.DeleteFolder();
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void CreateTemporaryNonExistentPathShouldWork(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, true);
            var temporaryPath = uut.GenerateUniqueTemporaryPath();
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
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
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void HasExtensionShouldWorkWithAndWithoutTheDot(IoServiceType type) {
            var uut = CreateUnitUnderTest(type, false);
            var testTxt = uut.TryParseAbsolutePath("/test.txt").Value.ExpectTextFileOrMissingPath();
            testTxt.HasExtension(".txt").Should().BeTrue();
            testTxt.HasExtension("txt").Should().BeTrue();
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void WithoutExtensionsShouldWork(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var testTxt = uut.TryParseAbsolutePath("/test.test.txt").Value;
            var test = testTxt.WithoutExtension;
            test.ToString().Should().Be("/test.test");
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void CommonShouldOnlyReturnFullFolderNames(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);

            var parent = ioService.TryParseAbsolutePath("C:\\test1\\test2").Value;
            var item1 = parent / "test3" / "test.csproj";
            var item2 = parent / "test4";

            var result = item1.TryCommonWith(item2).Value;

            result.ToString().Should().Be(parent.ToString());
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void ShouldNotParseWindowsAbsolutePathAsRelativePath(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);
            ioService.TryParseRelativePath("C:\\test1").HasValue.Should().BeFalse();
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void ShouldNotParseUnixAbsolutePathAsRelativePath(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);
            ioService.TryParseRelativePath("/test1").HasValue.Should().BeFalse();
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void ShouldNotParseRelativePathAsAbsolutePath(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);
            ioService.TryParseAbsolutePath("test1").HasValue.Should().BeFalse();
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
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
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void ShouldParseMacOSXPathWithColon(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var path = uut.ParseAbsolutePath("/src/TestResults/Deploy_family 2021-06-29 21:48:13/In");
            path.DirectorySeparator.Should().Be("/");
            path.Components.Should()
                .BeEquivalentTo("/", "src", "TestResults", "Deploy_family 2021-06-29 21:48:13", "In");
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public Task ShouldReadLinesInOrder(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var textFilePath = uut.GenerateUniqueTemporaryPath(".txt").ExpectTextFileOrMissingPath().WriteAllText("Line 1\nLine 2\nLine 3");
            return Verify(textFilePath.ReadLines().ToList())
                .UseParameters(type);
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public Task ShouldReadLinesBackwardsInCorrectOrder(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var textFilePath = uut.GenerateUniqueTemporaryPath(".txt").ExpectTextFileOrMissingPath().WriteAllText("Line 1\nLine 2\nLine 3");
            return Verify(textFilePath.ReadLinesBackwards().ToList())
                .UseParameters(type);
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void ShouldParseWithComplexMixedDirectorySeparators(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);
            var result = ioService.ParseAbsolutePath("C:\\test1\\./test2\\");
            result.ToString().Should().Be("C:\\test1\\test2");
            result.Components.Should().BeEquivalentTo("C:", "test1", "test2");
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void ShouldParseDescendantWithComplexMixedDirectorySeparators(IoServiceType type)
        {
            var ioService = CreateUnitUnderTest(type, false);
            var result = ioService.ParseAbsolutePath("C:\\test1") / "test2/test3";
            result.ToString().Should().Be("C:\\test1\\test2\\test3");
            result.Components.Should().BeEquivalentTo(new[] {"C:", "test1", "test2", "test3"});
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryWindowsIoService)]
        [DataRow(IoServiceType.InMemoryUnixIoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
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

            var repoRoot = ioService.CurrentDirectory.Ancestors.First(ancestor => (ancestor / ".git").IsFolder);
            var testFolder = repoRoot / "test_folder";
            var results = ioService.Query().Where(path => path.Parent.Value == testFolder).AsEnumerable().ToImmutableList();
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

            var repoRoot = ioService.CurrentDirectory.Ancestors.First(ancestor => (ancestor / ".git").IsFolder);
            var testFolder = repoRoot / "test_folder";
            var results = ioService.Query().Where(path => path.Ancestors.Contains(testFolder)).AsEnumerable().ToImmutableHashSet();
            results.Count.Should().BeGreaterThan(0);
            results.Count.Should().BeLessThan(10);
            results.Any(result => result.Name == "readme.md").Should().BeTrue();
            results.Any(result => result.Name == "test_folder").Should().BeFalse();
            results.Any(result => result.Name == "subfolder_readme.md").Should().BeTrue();
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void ShouldParseMixedDirectorySeparatorsAsAbsoluteWindowsPath(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var path = uut.ParseAbsolutePath("C:\\test1/test2");
            path.ToString().Should().Be("C:\\test1\\test2");
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void ShouldParseMixedDirectorySeparatorsAsAbsoluteWindowsUncPath(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var path = uut.ParseRelativePath(@"\\test1/test2");
            path.ToString().Should().Be(@"\\test1\test2");
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void ShouldParseMixedDirectorySeparatorsAsAbsoluteLinuxPath(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var path = uut.ParseAbsolutePath("/test1\\test2");
            path.ToString().Should().Be("/test1/test2");
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void ShouldParseMixedDirectorySeparatorsAsRelativeLinuxPath(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var path = uut.ParseRelativePath("test1/test2\\test3");
            path.ToString().Should().Be("./test1/test2/test3");
        }
        
        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        public void ShouldParseMixedDirectorySeparatorsAsRelativeWindowsPath(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var path = uut.ParseRelativePath("test1\\test2/test3");
            path.ToString().Should().Be(".\\test1\\test2\\test3");
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void RelativePathSlashNullShouldReturnOriginalPath(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var test1 = uut.ParseRelativePath("test1");
            var path = test1 / (RelativePath)null;
            path.Should().Be(test1);
            
            path = test1 / (string)null;
            path.Should().Be(test1);
            
            path = test1 / "";
            path.Should().Be(test1);
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void AbsolutePathSlashNullShouldReturnOriginalPath(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var test1 = uut.ParseAbsolutePath("/test1");
            var path = test1 / (RelativePath)null;
            path.Should().Be(test1);
            
            path = test1 / (string)null;
            path.Should().Be(test1);
            
            path = test1 / "";
            path.Should().Be(test1);
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void RelativePathWithTrailingSlashShouldBeEqualToWithoutTrailingSlash(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var withTrailingSlash = uut.ParseRelativePath("test1/");
            var withoutTrailingSlash = uut.ParseRelativePath("test1");
            withTrailingSlash.Should().Be(withoutTrailingSlash);
        }

        [TestMethod]
        [DataRow(IoServiceType.IoService)]
        [DataRow(IoServiceType.InMemoryZipIoService)]
        public void AbsolutePathWithTrailingSlashShouldBeEqualToWithoutTrailingSlash(IoServiceType type)
        {
            var uut = CreateUnitUnderTest(type, false);
            var withTrailingSlash = uut.ParseAbsolutePath("/test1/");
            var withoutTrailingSlash = uut.ParseAbsolutePath("/test1");
            withTrailingSlash.Should().Be(withoutTrailingSlash);
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

        [DataRow(true, IoServiceType.InMemoryZipIoService, false, IoServiceType.InMemoryZipIoService, false)]
        [DataRow(false, IoServiceType.InMemoryZipIoService, false, IoServiceType.InMemoryZipIoService, false)]
        [DataRow(true, IoServiceType.InMemoryZipIoService, false, IoServiceType.InMemoryUnixIoService, false)]
        [DataRow(false, IoServiceType.InMemoryZipIoService, false, IoServiceType.InMemoryUnixIoService, false)]
        [DataRow(true, IoServiceType.InMemoryUnixIoService, false, IoServiceType.InMemoryZipIoService, false)]
        [DataRow(false, IoServiceType.InMemoryUnixIoService, false, IoServiceType.InMemoryZipIoService, false)]
        [DataRow(true, IoServiceType.InMemoryZipIoService, false, IoServiceType.InMemoryWindowsIoService, false)]
        [DataRow(false, IoServiceType.InMemoryZipIoService, false, IoServiceType.InMemoryWindowsIoService, false)]
        [DataRow(true, IoServiceType.InMemoryWindowsIoService, false, IoServiceType.InMemoryZipIoService, false)]
        [DataRow(false, IoServiceType.InMemoryWindowsIoService, false, IoServiceType.InMemoryZipIoService, false)]
        public void MovingShouldWork(bool sameInstance, IoServiceType type1, bool enableOpenFileTracking1, IoServiceType type2, bool enableOpenFileTracking2)
        {
            var ioService = CreateUnitUnderTest(type1, false);

            var sourceFolder = ioService.CurrentDirectory / "test1";

            sourceFolder.EnsureIsEmptyFolder();

            var textFileInSourceFolder = (sourceFolder / "test.txt").ExpectTextFileOrMissingPath();

            textFileInSourceFolder.WriteAllText("testing 1 2 3");

            var targetFolder = (ioService.CurrentDirectory / "test2")
                .EnsureIsEmptyFolder();

            var textFileMovePlan = textFileInSourceFolder.Translate(sourceFolder, targetFolder);

            textFileMovePlan.Destination.Exists.Should().BeFalse();
            
            textFileMovePlan.Move();

            textFileMovePlan.Destination.Exists.Should().BeTrue();
        }
    }
}