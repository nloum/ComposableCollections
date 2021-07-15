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
            return _path.FileSystem.EnumerateChildren(_path, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathChildFiles : AbsolutePathDescendantsOrChildren<FilePath>, IEnumerable<FilePath>
    {
        public AbsolutePathChildFiles(IFolder path, string pattern = "*") : base(path, pattern, false)
        {
        }

        public override IEnumerator<FilePath> GetEnumerator()
        {
            return _path.FileSystem.EnumerateChildFiles(_path, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathChildFolders : AbsolutePathDescendantsOrChildren<FolderPath>, IEnumerable<FolderPath>
    {
        public AbsolutePathChildFolders(IFolder path, string pattern = "*") : base(path, pattern, false)
        {
        }

        public override IEnumerator<FolderPath> GetEnumerator()
        {
            return _path.FileSystem.EnumerateChildFolders(_path, _pattern).GetEnumerator();
        }
    }
}