using System.IO;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Readers.Rar;
using UnitsNet;

namespace IoFluently.SharpCompress
{
    public class RarFilePath : FilePath
    {
        private readonly string? _password;

        public RarFilePath(IFileOrFolderOrMissingPath path, string? password = null) : base(path)
        {
            _password = password;
        }

        public ArchiveType ArchiveType => ArchiveType.Rar;
        
        public IReader Read(Information? bufferSize = null)
        {
            return RarReader.Open(this.Open(FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: bufferSize), new ReaderOptions()
            {
                Password = _password,
                LeaveStreamOpen = false,
            });
        }
    }
}