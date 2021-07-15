using System.Collections.Generic;
using System.Linq;

namespace IoFluently
{
    public class AbsolutePathChildren : AbsolutePathDescendantsOrChildren<IFileOrFolder>
    {
        public AbsolutePathChildren(IFolder path, string pattern = "*") : base(path, pattern, false)
        {
        }

        public override IEnumerator<IFileOrFolder> GetEnumerator()
        {
            return _path.IoService.EnumerateChildren(_path, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathChildFiles : AbsolutePathDescendantsOrChildren<IFile>
    {
        public AbsolutePathChildFiles(IFolder path, string pattern = "*") : base(path, pattern, false)
        {
        }

        public override IEnumerator<IFile> GetEnumerator()
        {
            return _path.IoService.EnumerateChildFiles(_path, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathChildFolders : AbsolutePathDescendantsOrChildren<IFolder>
    {
        public AbsolutePathChildFolders(IFolder path, string pattern = "*") : base(path, pattern, false)
        {
        }

        public override IEnumerator<IFolder> GetEnumerator()
        {
            return _path.IoService.EnumerateChildFolders(_path, _pattern).GetEnumerator();
        }
    }
}