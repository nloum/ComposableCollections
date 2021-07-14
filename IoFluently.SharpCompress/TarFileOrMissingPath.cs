using System.IO;
using SharpCompress.Common;
using SharpCompress.Writers;
using SharpCompress.Writers.Tar;
using UnitsNet;

namespace IoFluently.SharpCompress
{
    public class TarFileOrMissingPath : FileOrMissingPathBase
    {
        private readonly string? _password;

        public TarFileOrMissingPath(IFileOrFolderOrMissingPath path, string? password = null) : base(path)
        {
            _password = password;
        }

        public ArchiveType ArchiveType => ArchiveType.Tar;
        
        public IWriter Write(CompressionType compressionType, ArchiveEncoding archiveEncoding = null, Information? bufferSize = null)
        {
            return new TarWriter(this.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, bufferSize: bufferSize), new TarWriterOptions(compressionType, true)
            {
                CompressionType = compressionType,
                LeaveStreamOpen = false,
                ArchiveEncoding = archiveEncoding
            });
        }
    }
}