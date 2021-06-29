using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace IoFluently
{
    public class AbsolutePathFileInfoAdapter : IFileInfo {
        private readonly AbsolutePath _absolutePath;
        private readonly bool _isPhysicalPath;

        public AbsolutePathFileInfoAdapter( AbsolutePath absolutePath, bool isPhysicalPath = false )
        {
            _absolutePath = absolutePath;
            _isPhysicalPath = isPhysicalPath;
        }

        public Stream CreateReadStream() => _absolutePath.IoService.TryOpen( _absolutePath, FileMode.Open, FileAccess.Read, FileShare.Read ).Value;

        public bool Exists => _absolutePath.IoService.Exists(_absolutePath);
        public long Length => (long) Math.Round(_absolutePath.IoService.TryFileSize(_absolutePath).Value.Bytes);
        public string PhysicalPath => _isPhysicalPath ? _absolutePath.ToString() : null;
        public string Name => _absolutePath.Name;
        public DateTimeOffset LastModified => _absolutePath.IoService.TryLastWriteTime(_absolutePath).Value;
        public bool IsDirectory => _absolutePath.IoService.IsFolder(_absolutePath);
    }
}