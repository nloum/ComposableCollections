using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileProviders;

namespace IoFluently
{
    public class AbsolutePathDirectoryContents : IDirectoryContents {
        private readonly FolderPath _folderPath;

        public AbsolutePathDirectoryContents( FolderPath folderPath ) => _folderPath = folderPath;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<IFileInfo> GetEnumerator() => _folderPath
            .FileSystem.EnumerateChildFiles(_folderPath)
            .Select( x => new AbsolutePathFileInfoAdapter( x ) )
            .GetEnumerator();

        public bool Exists => _folderPath.FileSystem.Exists(_folderPath);
    }
}