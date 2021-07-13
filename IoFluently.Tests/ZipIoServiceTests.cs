using System.Collections.Immutable;
using System.IO;
using System.IO.Compression;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyMSTest;

namespace IoFluently.Tests
{
    [TestClass]
    public class ZipIoServiceTests : VerifyBase
    {
        [TestMethod]
        public void ShouldZipUpSingleFile()
        {
            var inMemoryIoService = new InMemoryIoService(true, "/", false);
            var textFilePath = (inMemoryIoService.DefaultRelativePathBase / "test.txt")
                .ExpectTextFileOrMissingPath()
                .WriteAllText("Test 1 2 3");
            var testZipFilePath = inMemoryIoService.ParseAbsolutePath("/test.zip").ExpectFileOrMissingPath();
            var result = testZipFilePath.ExpectZipFileOrMissingPath(true);
            result.Zip(textFilePath, textFilePath.Parent);
            
            using (var stream = result.ZipFilePath.Open(FileMode.Open, FileAccess.Read))
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    archive.Entries.Count.Should().Be(1);
                    archive.Entries[0].FullName.Should().Be("/test.txt");
                    using var entry = archive.Entries[0].Open();
                    using var reader = new StreamReader(entry);
                    var text = reader.ReadToEnd();
                    text.Should().Be("Test 1 2 3");
                }
            }
        }

        [TestMethod]
        public void ShouldZipUpAndUnzipFolder()
        {
            var inMemoryIoService = new InMemoryIoService(true, "/", false);
            var folder = inMemoryIoService.DefaultRelativePathBase / "test";
            var textFilePath = folder / "test.txt";
            textFilePath.ExpectTextFileOrMissingPath().WriteAllText("Test 1 2 3");
            var testZipFilePath = inMemoryIoService.ParseAbsolutePath("/test.zip").ExpectFileOrMissingPath();
            var result = testZipFilePath.ExpectZipFileOrMissingPath(true);
            result.Zip(folder, folder.Parent.Value);
            
            using (var stream = result.ZipFilePath.Open(FileMode.Open, FileAccess.Read))
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    archive.Entries.Count.Should().Be(1);
                    archive.Entries[0].FullName.Should().Be("/test/test.txt");
                    using var entry = archive.Entries[0].Open();
                    using var reader = new StreamReader(entry);
                    var text = reader.ReadToEnd();
                    text.Should().Be("Test 1 2 3");
                }
            }
            
            var unzippedTo = (inMemoryIoService.DefaultRelativePathBase / "test2").EnsureIsEmptyFolder();
            result.Unzip(unzippedTo);

            Verify(unzippedTo.Descendants);
        }
    }
}