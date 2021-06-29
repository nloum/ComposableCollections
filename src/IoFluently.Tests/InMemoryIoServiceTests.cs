using System.IO;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
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
            root.IsFolder.Should().Be(true);
            root.ToString().Should().Be("/");
            var testTxt = uut.ParseAbsolutePath("/test1.txt");
            testTxt.ToString().Should().Be("/test1.txt");
            testTxt.Parent.ToString().Should().Be("/");
        }

        [TestMethod]
        public void ShouldBeAbleToWriteTextToAndReadTextFromFiles()
        {
            var uut = new InMemoryIoService("\n", true, "/");
            uut.RootFolders.Add("/", new InMemoryIoService.Folder());
            var testTxt = uut.ParseAbsolutePath("/test1.txt");
            testTxt.WriteAllText("testcontents");
            testTxt.ReadAllText().Should().Be("testcontents");
        }

        [TestMethod]
        public void ShouldBeAbleToWriteToAndReadFromFiles()
        {
            var uut = new InMemoryIoService("\n", true, "/");
            uut.RootFolders.Add("/", new InMemoryIoService.Folder());
            var testTxt = uut.ParseAbsolutePath("/test1.txt");
            var originalBuffer = new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            using (var writer = testTxt.Open(FileMode.CreateNew))
            {
                writer.Write(originalBuffer);
            }

            using (var reader = testTxt.Open(FileMode.Open))
            {
                reader.Length.Should().Be(originalBuffer.Length);
                var buffer = new byte[256];
                var bytesRead = reader.Read(buffer, 0, buffer.Length);
                bytesRead.Should().Be(originalBuffer.Length);
                buffer[0].Should().Be(0);
                buffer[1].Should().Be(1);
                buffer[2].Should().Be(2);
                buffer[3].Should().Be(3);
                buffer[4].Should().Be(4);
                buffer[5].Should().Be(5);
                buffer[6].Should().Be(6);
                buffer[7].Should().Be(7);
                buffer[8].Should().Be(8);
                buffer[9].Should().Be(9);
                buffer[10].Should().Be(10);
            }
        }

        [TestMethod]
        public void FileProviderImplementationShouldWorkForConfiguration()
        {
            var configFileContents = $@"{{
	""ConfigSection1"": {{
		""Value1"": ""astringvalue""
	}}
}}";

            var ioService = new InMemoryIoService( "\n", false, "\\" );
            ioService.RootFolders.Add( "C:", new InMemoryIoService.Folder() );
            var root = ioService.ParseAbsolutePath( "C:\\" );
            ioService.WriteAllText( ioService.ParseAbsolutePath( "C:\\appsettings.json" ), configFileContents );

            var configuration = new ConfigurationBuilder()
                .AddJsonFile( root.Descendants(), "appsettings.json", false, false )
                .Build();

            configuration["ConfigSection1:Value1"].Should().Be("astringvalue");
        }
    }
}