using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using TreeLinq;

namespace IoFluently
{
    public class AbsolutePathDescendants : AbsolutePathDescendantsOrChildren<IFileOrFolder>, IFileProvider, IEnumerable<IFileOrFolder>
    {
        public AbsolutePathDescendants(IFolder path, string pattern = "*") : base(path, pattern, true)
        {
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new AbsolutePathFileInfoAdapter((_path / subpath).ExpectFile());
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new AbsolutePathDirectoryContents(new FolderPath(_path / subpath));
        }

        public IChangeToken Watch(string filter)
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }

        public override IEnumerator<IFileOrFolder> GetEnumerator()
        {
            return _path.FileSystem.EnumerateDescendants(_path, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathDescendantFiles : AbsolutePathDescendantsOrChildren<FilePath>, IFileProvider, IEnumerable<FilePath>
    {
        public AbsolutePathDescendantFiles(IFolder path, string pattern = "*") : base(path, pattern, true)
        {
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new AbsolutePathFileInfoAdapter((_path / subpath).ExpectFile());
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new AbsolutePathDirectoryContents(new FolderPath(_path / subpath));
        }

        public IChangeToken Watch(string filter)
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }

        public override IEnumerator<FilePath> GetEnumerator()
        {
            return _path.FileSystem.EnumerateDescendantFiles(_path, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathDescendantFolders : AbsolutePathDescendantsOrChildren<FolderPath>, IFileProvider, IEnumerable<FolderPath>
    {
        public AbsolutePathDescendantFolders(IFolder path, string pattern = "*") : base(path, pattern, true)
        {
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new AbsolutePathFileInfoAdapter((_path / subpath).ExpectFile());
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new AbsolutePathDirectoryContents(new FolderPath(_path / subpath));
        }

        public IChangeToken Watch(string filter)
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }

        public override IEnumerator<FolderPath> GetEnumerator()
        {
            return _path.FileSystem.EnumerateDescendantFolders(_path, _pattern).GetEnumerator();
        }
    }
}