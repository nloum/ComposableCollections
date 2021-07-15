using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileProviders;

namespace IoFluently
{
    public class AbsolutePathDirectoryContents : IDirectoryContents {
        private readonly FolderPath _absolutePath;

        public AbsolutePathDirectoryContents( FolderPath absolutePath ) => _absolutePath = absolutePath;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<IFileInfo> GetEnumerator() => _absolutePath
            .FileSystem.EnumerateChildFiles(_absolutePath)
            .Select( x => new AbsolutePathFileInfoAdapter( x ) )
            .GetEnumerator();

        public bool Exists => _absolutePath.FileSystem.Exists(_absolutePath);
    }
}