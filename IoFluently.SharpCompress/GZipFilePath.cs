using System.IO;
using SharpCompress.Common;
using SharpCompress.Compressors.Deflate;
using SharpCompress.Readers;
using SharpCompress.Readers.GZip;
using SharpCompress.Writers;
using UnitsNet;

namespace IoFluently.SharpCompress
{
    public class GZipFilePath : FilePath
    {
        private readonly string? _password;

        public GZipFilePath(IFileOrFolderOrMissingPath path, string? password = null) : base(path)
        {
            _password = password;
        }

        public ArchiveType ArchiveType => ArchiveType.GZip;

        public IReader Read(Information? bufferSize = null)
        {
            return GZipReader.Open(this.Open(FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: bufferSize),
                new ReaderOptions()
                {
                    Password = _password,
                    LeaveStreamOpen = false,
                });
        }

        public IWriter Write(CompressionType compressionType, CompressionLevel compressionLevel,
            Information? bufferSize = null)
        {
            return this.ExpectGZipFileOrMissingPath()
                .Write(compressionType, compressionLevel, bufferSize);
        }
    }
}