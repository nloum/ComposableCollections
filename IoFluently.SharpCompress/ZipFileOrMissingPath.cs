using System.IO;
using SharpCompress.Common;
using SharpCompress.Compressors.Deflate;
using SharpCompress.Writers;
using SharpCompress.Writers.Zip;
using UnitsNet;

namespace IoFluently.SharpCompress
{
    public class ZipFileOrMissingPath : FileOrMissingPathBase
    {
        private readonly string? _password;
    
        public ZipFileOrMissingPath(IFileOrFolderOrMissingPath path, string? password = null) : base(path)
        {
            _password = password;
        }
    
        public ArchiveType ArchiveType => ArchiveType.Zip;
        
        public IWriter Write(CompressionType compressionType, string archiveComment,
            CompressionLevel deflateCompressionLevel,
            bool useZip64 = false,
            ArchiveEncoding archiveEncoding = null,
            Information? bufferSize = null)
        {
            return new ZipWriter(this.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, bufferSize: bufferSize), new ZipWriterOptions(compressionType)
            {
                CompressionType = compressionType,
                ArchiveComment = archiveComment,
                ArchiveEncoding = archiveEncoding,
                LeaveStreamOpen = false,
                DeflateCompressionLevel = deflateCompressionLevel,
                UseZip64 = useZip64
            });
        }
    }
}