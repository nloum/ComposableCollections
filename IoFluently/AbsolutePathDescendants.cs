using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using TreeLinq;

namespace IoFluently
{
    public class AbsolutePathDescendants : AbsolutePathDescendantsOrChildren<IFileOrFolder>, IFileProvider, IEnumerable<IFileOrFolder>
    {
        public AbsolutePathDescendants(IFolderPath path, string pattern = "*") : base(path, pattern, true)
        {
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new AbsolutePathFileInfoAdapter((_folderPath / subpath).ExpectFile());
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new AbsolutePathDirectoryContents(new FolderPath(_folderPath / subpath));
        }

        public IChangeToken Watch(string filter)
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }

        public override IEnumerator<IFileOrFolder> GetEnumerator()
        {
            return _folderPath.FileSystem.EnumerateDescendants(_folderPath, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathDescendantFiles : AbsolutePathDescendantsOrChildren<FilePath>, IFileProvider, IEnumerable<FilePath>
    {
        public AbsolutePathDescendantFiles(IFolderPath path, string pattern = "*") : base(path, pattern, true)
        {
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new AbsolutePathFileInfoAdapter((_folderPath / subpath).ExpectFile());
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new AbsolutePathDirectoryContents(new FolderPath(_folderPath / subpath));
        }

        public IChangeToken Watch(string filter)
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }

        public override IEnumerator<FilePath> GetEnumerator()
        {
            return _folderPath.FileSystem.EnumerateDescendantFiles(_folderPath, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathDescendantFolders : AbsolutePathDescendantsOrChildren<FolderPath>, IFileProvider, IEnumerable<FolderPath>
    {
        public AbsolutePathDescendantFolders(IFolderPath path, string pattern = "*") : base(path, pattern, true)
        {
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new AbsolutePathFileInfoAdapter((_folderPath / subpath).ExpectFile());
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new AbsolutePathDirectoryContents(new FolderPath(_folderPath / subpath));
        }

        public IChangeToken Watch(string filter)
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }

        public override IEnumerator<FolderPath> GetEnumerator()
        {
            return _folderPath.FileSystem.EnumerateDescendantFolders(_folderPath, _pattern).GetEnumerator();
        }
    }
}