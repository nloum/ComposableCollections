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
            return new AbsolutePathDirectoryContents(new Folder(_path / subpath));
        }

        public IChangeToken Watch(string filter)
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }

        public override IEnumerator<IFileOrFolder> GetEnumerator()
        {
            return _path.IoService.EnumerateDescendants(_path, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathDescendantFiles : AbsolutePathDescendantsOrChildren<File>, IFileProvider, IEnumerable<File>
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
            return new AbsolutePathDirectoryContents(new Folder(_path / subpath));
        }

        public IChangeToken Watch(string filter)
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }

        public override IEnumerator<File> GetEnumerator()
        {
            return _path.IoService.EnumerateDescendantFiles(_path, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathDescendantFolders : AbsolutePathDescendantsOrChildren<Folder>, IFileProvider, IEnumerable<Folder>
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
            return new AbsolutePathDirectoryContents(new Folder(_path / subpath));
        }

        public IChangeToken Watch(string filter)
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }

        public override IEnumerator<Folder> GetEnumerator()
        {
            return _path.IoService.EnumerateDescendantFolders(_path, _pattern).GetEnumerator();
        }
    }
}