using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using LiveLinq.Set;

namespace IoFluently
{
    public abstract class DescendantsOrChildren<TFileOrFolder> : IEnumerable<TFileOrFolder>
        where TFileOrFolder : IFileOrFolderOrMissingPath
    {
        protected readonly FolderPath _folderPath;
        protected readonly string _pattern;

        protected DescendantsOrChildren(IFolderPath folderPath, string pattern, bool includeSubFolders)
        {
            _folderPath = folderPath.ExpectFolder();
            _pattern = pattern;
            IncludeSubFolders = includeSubFolders;
            FileSystem = folderPath.FileSystem;
        }

        public IFileSystem FileSystem { get; }

        protected bool IncludeSubFolders { get; }

        public IObservableReadOnlySet<FileOrFolderOrMissingPath> WithChangeNotifications()
        {
            return new ObservableReadOnlySet(_folderPath, _pattern, IncludeSubFolders, FileSystem, this);
        }

        public ISetChanges<FileOrFolderOrMissingPath> ToLiveLinq()
        {
            return WithChangeNotifications().ToLiveLinq();
        }
        
        private class ObservableReadOnlySet : IObservableReadOnlySet<FileOrFolderOrMissingPath> {
            private readonly FolderPath _folderPath;
            private readonly string _pattern;
            private bool _includeSubFolders;
            private IFileSystem _fileSystem;
            private readonly IEnumerable<TFileOrFolder> _enumerable;

            public ObservableReadOnlySet(FolderPath folderPath, string pattern, bool includeSubFolders, IFileSystem fileSystem, IEnumerable<TFileOrFolder> enumerable)
            {
                _folderPath = folderPath;
                _pattern = pattern;
                _includeSubFolders = includeSubFolders;
                _fileSystem = fileSystem;
                _enumerable = enumerable;
            }

            public ISetChanges<FileOrFolderOrMissingPath> ToLiveLinq()
            {
                return _fileSystem.ToLiveLinq(_folderPath, true, _includeSubFolders, _pattern);
            }

            public IEnumerator<FileOrFolderOrMissingPath> GetEnumerator()
            {
                return _enumerable.Select(x => x.ExpectFileOrFolderOrMissingPath()).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int Count => _enumerable.Count();
            
            public void Dispose()
            {
                
            }
        }

        public abstract IEnumerator<TFileOrFolder> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
