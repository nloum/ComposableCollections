using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using TreeLinq;

namespace IoFluently
{
    public class AbsolutePathDescendants : AbsolutePathDescendantsOrChildren<IFileOrFolder>, IFileProvider
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
    
    public class AbsolutePathDescendantFiles : AbsolutePathDescendantsOrChildren<IFile>, IFileProvider
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
        
        public override IEnumerator<IFile> GetEnumerator()
        {
            return _path.IoService.EnumerateDescendantFiles(_path, _pattern).GetEnumerator();
        }
    }
    
    public class AbsolutePathDescendantFolders : AbsolutePathDescendantsOrChildren<IFolder>, IFileProvider
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
        
        public override IEnumerator<IFolder> GetEnumerator()
        {
            return _path.IoService.EnumerateDescendantFolders(_path, _pattern).GetEnumerator();
        }
    }
}