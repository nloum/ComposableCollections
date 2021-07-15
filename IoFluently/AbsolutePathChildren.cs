using System.Collections.Generic;
using System.Linq;

namespace IoFluently
{
    public class AbsolutePathChildren : AbsolutePathDescendantsOrChildren<IFileOrFolder>, IEnumerable<IFileOrFolder>
    {
        public AbsolutePathChildren(IFolder path, string pattern = "*") : base(path, pattern, false)
        {
        }

        public override IEnumerator<IFileOrFolder> GetEnumerator()
        {
            return _path.IoService.EnumerateChildren(_path, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathChildFiles : AbsolutePathDescendantsOrChildren<File>, IEnumerable<File>
    {
        public AbsolutePathChildFiles(IFolder path, string pattern = "*") : base(path, pattern, false)
        {
        }

        public override IEnumerator<File> GetEnumerator()
        {
            return _path.IoService.EnumerateChildFiles(_path, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathChildFolders : AbsolutePathDescendantsOrChildren<Folder>, IEnumerable<Folder>
    {
        public AbsolutePathChildFolders(IFolder path, string pattern = "*") : base(path, pattern, false)
        {
        }

        public override IEnumerator<Folder> GetEnumerator()
        {
            return _path.IoService.EnumerateChildFolders(_path, _pattern).GetEnumerator();
        }
    }
}