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
            root.IsFolder().Should().Be(true);
            root.ToString().Should().Be("/");
            var testTxt = uut.ParseAbsolutePath("/test1.txt");
            testTxt.ToString().Should().Be("/test1.txt");
            testTxt.Parent().ToString().Should().Be("/");
        }

        [TestMethod]
        public void ShouldBeAbleToWriteToAndReadFromFiles()
        {
            var uut = new InMemoryIoService("\n", true, "/");
            uut.RootFolders.Add("/", new InMemoryIoService.Folder());
            var testTxt = uut.ParseAbsolutePath("/test1.txt");
            testTxt.WriteAllText("testcontents");
            testTxt.ReadAllText().Should().Be("testcontents");
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