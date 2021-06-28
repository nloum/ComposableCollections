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
        public void ShouldCreateEmptyZipFile()
        {
            var inMemoryIoService = new InMemoryIoService("\n", true, "/", false);
            inMemoryIoService.RootFolders.Add("/", new InMemoryIoService.Folder());
            var testZipFilePath = inMemoryIoService.ParseAbsolutePath("/test.zip");
            var result = testZipFilePath.AsZipFile(true);
            result.SetTemporaryFolder(result.ParseAbsolutePath("/tmp"));

            using (var stream = result.ZipFilePath.Open(FileMode.Open, FileAccess.Read))
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
                {
                    archive.Entries.Should().BeEmpty();
                }
            }
        }
        
        [TestMethod]
        public void ShouldZipUpSingleFile()
        {
            var inMemoryIoService = new InMemoryIoService("\n", true, "/", false);
            inMemoryIoService.RootFolders.Add("/", new InMemoryIoService.Folder());
            var textFilePath = inMemoryIoService.DefaultRelativePathBase / "test.txt";
            textFilePath.WriteAllText("Test 1 2 3");
            var testZipFilePath = inMemoryIoService.ParseAbsolutePath("/test.zip");
            var result = testZipFilePath.AsZipFile(true);
            result.Zip(textFilePath, textFilePath.Parent());
            
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
            var inMemoryIoService = new InMemoryIoService("\n", true, "/", false);
            inMemoryIoService.RootFolders.Add("/", new InMemoryIoService.Folder());
            var folder = inMemoryIoService.DefaultRelativePathBase / "test";
            var textFilePath = folder / "test.txt";
            textFilePath.WriteAllText("Test 1 2 3");
            var testZipFilePath = inMemoryIoService.ParseAbsolutePath("/test.zip");
            var result = testZipFilePath.AsZipFile(true);
            result.Zip(folder, folder.Parent());
            
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
            
            var unzippedTo = inMemoryIoService.DefaultRelativePathBase / "test2";
            result.Unzip(unzippedTo);

            Verify(unzippedTo.Descendants());
        }
    }
}