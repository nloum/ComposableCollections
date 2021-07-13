using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileProviders;

namespace IoFluently
{
    public class AbsolutePathDirectoryContents : IDirectoryContents {
        private readonly Folder _absolutePath;

        public AbsolutePathDirectoryContents( Folder absolutePath ) => _absolutePath = absolutePath;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<IFileInfo> GetEnumerator() => _absolutePath
            .Children
            .Where( x => x.IoService.IsFile(x) ).Select( x => new AbsolutePathFileInfoAdapter( x ) )
            .GetEnumerator();

        public bool Exists => _absolutePath.IoService.Exists(_absolutePath.Path);
    }
}