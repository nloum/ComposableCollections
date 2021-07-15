using System.Collections.Generic;
using System.Linq;

namespace IoFluently
{
    public class AbsolutePathChildren : AbsolutePathDescendantsOrChildren<IFileOrFolder>, IEnumerable<IFileOrFolder>
    {
        public AbsolutePathChildren(IFolderPath path, string pattern = "*") : base(path, pattern, false)
        {
        }

        public override IEnumerator<IFileOrFolder> GetEnumerator()
        {
            return _folderPath.FileSystem.EnumerateChildren(_folderPath, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathChildFiles : AbsolutePathDescendantsOrChildren<FilePath>, IEnumerable<FilePath>
    {
        public AbsolutePathChildFiles(IFolderPath path, string pattern = "*") : base(path, pattern, false)
        {
        }

        public override IEnumerator<FilePath> GetEnumerator()
        {
            return _folderPath.FileSystem.EnumerateChildFiles(_folderPath, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathChildFolders : AbsolutePathDescendantsOrChildren<FolderPath>, IEnumerable<FolderPath>
    {
        public AbsolutePathChildFolders(IFolderPath path, string pattern = "*") : base(path, pattern, false)
        {
        }

        public override IEnumerator<FolderPath> GetEnumerator()
        {
            return _folderPath.FileSystem.EnumerateChildFolders(_folderPath, _pattern).GetEnumerator();
        }
    }
}