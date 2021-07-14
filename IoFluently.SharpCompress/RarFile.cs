using System.IO;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Readers.Rar;
using UnitsNet;

namespace IoFluently.SharpCompress
{
    public class RarFile : File
    {
        private readonly string? _password;

        public RarFile(IFileOrFolderOrMissingPath path, string? password = null) : base(path)
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