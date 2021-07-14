using System.IO;
using SharpCompress.Common;
using SharpCompress.Compressors.Deflate;
using SharpCompress.Writers;
using SharpCompress.Writers.GZip;
using UnitsNet;

namespace IoFluently.SharpCompress
{
    public class GZipFileOrMissingPath : FileOrMissingPathBase
    {
        private readonly string? _password;

        public GZipFileOrMissingPath(IFileOrFolderOrMissingPath path, string? password = null) : base(path)
        {
            _password = password;
        }

        public ArchiveType ArchiveType => ArchiveType.GZip;

        public IWriter Write(CompressionType compressionType, CompressionLevel compressionLevel,
            Information? bufferSize = null)
        {
            return new GZipWriter(this.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, bufferSize: bufferSize),
                new GZipWriterOptions
                {
                    CompressionType = compressionType,
                    CompressionLevel = compressionLevel,
                    LeaveStreamOpen = false,
                });
        }
    }
}