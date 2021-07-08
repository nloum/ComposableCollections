using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace IoFluently
{
    public class AbsolutePathFileInfoAdapter : IFileInfo {
        private readonly File _absolutePath;
        private readonly bool _isPhysicalPath;

        public AbsolutePathFileInfoAdapter( File absolutePath, bool isPhysicalPath = false )
        {
            _absolutePath = absolutePath;
            _isPhysicalPath = isPhysicalPath;
        }

        public Stream CreateReadStream() => _absolutePath.IoService.Open( _absolutePath, FileMode.Open, FileAccess.Read, FileShare.Read );

        public bool Exists => _absolutePath.IoService.Exists(_absolutePath);
        public long Length => (long) Math.Round(_absolutePath.IoService.FileSize(_absolutePath).Bytes);
        public string PhysicalPath => _isPhysicalPath ? _absolutePath.ToString() : null;
        public string Name => _absolutePath.Path.Name;
        public DateTimeOffset LastModified => _absolutePath.IoService.LastWriteTime(_absolutePath);
        public bool IsDirectory => _absolutePath.IoService.IsFolder(_absolutePath);
    }
}