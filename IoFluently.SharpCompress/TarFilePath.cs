using System.IO;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Readers.Tar;
using SharpCompress.Writers;
using UnitsNet;

namespace IoFluently.SharpCompress
{
    public class TarFilePath : FilePath
    {
        private readonly string? _password;

        public TarFilePath(IFileOrFolderOrMissingPath path, string? password = null) : base(path)
        {
            _password = password;
        }

        public ArchiveType ArchiveType => ArchiveType.Tar;
        
        public IReader Read(Information? bufferSize = null)
        {
            return TarReader.Open(this.Open(FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: bufferSize), new ReaderOptions()
            {
                Password = _password,
                LeaveStreamOpen = false,
            });
        }
        
        public IWriter Write(CompressionType compressionType, ArchiveEncoding archiveEncoding = null, Information? bufferSize = null)
        {
            return this.ExpectTarFileOrMissingPath()
                .Write(compressionType, archiveEncoding, bufferSize);
        }
    }
}