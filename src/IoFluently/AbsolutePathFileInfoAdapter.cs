using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace IoFluently
{
    public class AbsolutePathFileInfoAdapter : IFileInfo {
        private readonly AbsolutePath _absolutePath;

        public AbsolutePathFileInfoAdapter( AbsolutePath absolutePath ) => _absolutePath = absolutePath;

        public Stream CreateReadStream() => _absolutePath.Open( FileMode.Open, FileAccess.Read, FileShare.Read );

        public bool Exists => _absolutePath.Exists();
        public long Length => (long) Math.Round(_absolutePath.FileSize().Bytes);
        public string PhysicalPath => _absolutePath.ToString();
        public string Name => _absolutePath.Name;
        public DateTimeOffset LastModified => _absolutePath.LastWriteTime();
        public bool IsDirectory => _absolutePath.IsFolder();
    }
}