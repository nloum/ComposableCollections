using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace IoFluently
{
    public class AbsolutePathFileInfoAdapter : IFileInfo {
        private readonly IFilePath _absolutePath;
        private readonly bool _isPhysicalPath;

        public AbsolutePathFileInfoAdapter( IFilePath absolutePath, bool isPhysicalPath = false )
        {
            _absolutePath = absolutePath;
            _isPhysicalPath = isPhysicalPath;
        }

        public Stream CreateReadStream() => _absolutePath.FileSystem.Open( _absolutePath, FileMode.Open, FileAccess.Read, FileShare.Read );

        public bool Exists => _absolutePath.FileSystem.Exists(_absolutePath);
        public long Length => (long) Math.Round(_absolutePath.FileSystem.FileSize(_absolutePath).Bytes);
        public string PhysicalPath => _isPhysicalPath ? _absolutePath.FullName : null;
        public string Name => _absolutePath.FileSystem.Name(_absolutePath);
        public DateTimeOffset LastModified => _absolutePath.FileSystem.LastWriteTime(_absolutePath);
        public bool IsDirectory => _absolutePath.FileSystem.IsFolder(_absolutePath);
    }
}