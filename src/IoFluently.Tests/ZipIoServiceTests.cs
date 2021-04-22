using System.IO;
using System.IO.Compression;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoFluently.Tests
{
    [TestClass]
    public class ZipIoServiceTests
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
    }
}