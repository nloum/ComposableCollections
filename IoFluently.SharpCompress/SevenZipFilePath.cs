using System.IO;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Common;
using SharpCompress.Readers;
using UnitsNet;

namespace IoFluently.SharpCompress
{
    public class SevenZipFilePath : FilePath
    {
        private readonly string? _password;

        public SevenZipFilePath(IFileOrFolderOrMissingPath path, string? password = null) : base(path)
        {
            _password = password;
        }

        public ArchiveType ArchiveType => ArchiveType.SevenZip;
        
        public SevenZipArchive Read(Information? bufferSize = null)
        {
            return SevenZipArchive.Open(this.Open(FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: bufferSize), new ReaderOptions()
            {
                Password = _password,
                LeaveStreamOpen = false,
            });
        }
    }
}