using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;

namespace IoFluently
{
    public class ChildFilesOrFolders : DescendantsOrChildren<IFileOrFolderPath>, IEnumerable<IFileOrFolderPath>
    {
        public ChildFilesOrFolders(IFolderPath folderPath, string pattern = "*") : base(folderPath, pattern, false)
        {
        }

        public override IEnumerator<IFileOrFolderPath> GetEnumerator()
        {
            return _folderPath.FileSystem.EnumerateChildren(_folderPath, _pattern).GetEnumerator();
        }
        
        
    }
    
    public class ChildFiles : DescendantsOrChildren<FilePath>, IEnumerable<FilePath>
    {
        public ChildFiles(IFolderPath folderPath, string pattern = "*") : base(folderPath, pattern, false)
        {
        }

        public override IEnumerator<FilePath> GetEnumerator()
        {
            return _folderPath.FileSystem.EnumerateChildFiles(_folderPath, _pattern).GetEnumerator();
        }
    }
    
    public class ChildFolders : DescendantsOrChildren<FolderPath>, IEnumerable<FolderPath>
    {
        public ChildFolders(IFolderPath folderPath, string pattern = "*") : base(folderPath, pattern, false)
        {
        }

        public override IEnumerator<FolderPath> GetEnumerator()
        {
            return _folderPath.FileSystem.EnumerateChildFolders(_folderPath, _pattern).GetEnumerator();
        }
    }
}