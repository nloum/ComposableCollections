using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using TreeLinq;

namespace IoFluently
{
    public class DescendantFilesOrFolders : DescendantsOrChildren<IFileOrFolderPath>, IFileProvider, IEnumerable<IFileOrFolderPath>
    {
        public DescendantFilesOrFolders(IFolderPath folderPath, string pattern = "*") : base(folderPath, pattern, true)
        {
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new FilePathFileInfoAdapter((_folderPath / subpath).ExpectFile());
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new FolderContents(new FolderPath(_folderPath / subpath));
        }

        public IChangeToken Watch(string filter)
        {
            // TODO - implement this properly
            return new EmptyChangeToken();
        }

        public override IEnumerator<IFileOrFolderPath> GetEnumerator()
        {
            return _folderPath.FileSystem.EnumerateDescendants(_folderPath, _pattern).GetEnumerator();
        }
    }
    
    public class DescendantFiles : DescendantsOrChildren<FilePath>, IFileProvider, IEnumerable<FilePath>
    {
        public DescendantFiles(IFolderPath folderPath, string pattern = "*") : base(folderPath, pattern, true)
        {
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new FilePathFileInfoAdapter((_folderPath / subpath).ExpectFile());
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new FolderContents(new FolderPath(_folderPath / subpath));
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
    
    public class DescendantFolders : DescendantsOrChildren<FolderPath>, IFileProvider, IEnumerable<FolderPath>
    {
        public DescendantFolders(IFolderPath folderPath, string pattern = "*") : base(folderPath, pattern, true)
        {
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new FilePathFileInfoAdapter((_folderPath / subpath).ExpectFile());
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return new FolderContents(new FolderPath(_folderPath / subpath));
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