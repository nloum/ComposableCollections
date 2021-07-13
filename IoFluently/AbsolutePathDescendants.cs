using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using TreeLinq;

namespace IoFluently
{
    public class AbsolutePathDescendants : AbsolutePathDescendantsOrChildren, IFileProvider
    {
        internal AbsolutePathDescendants(Folder path, string pattern, IIoService ioService) : base(path, pattern, true, ioService)
        {
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new AbsolutePathFileInfoAdapter(_path / subpath);
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new AbsolutePathDirectoryContents(_path / subpath);
        }

        public IChangeToken Watch(string filter)
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }
        
        public override IEnumerator<AbsolutePath> GetEnumerator()
        {
            return _path.IoService.Descendants(_path, _pattern).Select(x => x.Path).GetEnumerator();
        }
    }
}