using System.Collections.Generic;
using System.Linq;

namespace IoFluently
{
    public class AbsolutePathChildren : AbsolutePathDescendantsOrChildren
    {
        internal AbsolutePathChildren(IFolder<Folder> path, string pattern, IIoService ioService) : base(path, pattern, false, ioService)
        {
        }  

        public override IEnumerator<IFileOrFolderOrMissingPath> GetEnumerator()
        {
            return _path.IoService.Children(_path, _pattern).GetEnumerator();
        }
    }
}