using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using LiveLinq.Set;

namespace IoFluently
{
    public abstract class AbsolutePathDescendantsOrChildren<TFileOrFolder> : IObservableReadOnlySet<AbsolutePath>, IEnumerable<TFileOrFolder>
        where TFileOrFolder : IFileOrFolderOrMissingPath
    {
        protected readonly Folder _path;
        protected readonly string _pattern;

        protected AbsolutePathDescendantsOrChildren(IFolder path, string pattern, bool includeSubFolders)
        {
            _path = path.ExpectFolder();
            _pattern = pattern;
            IncludeSubFolders = includeSubFolders;
            IoService = path.IoService;
        }

        public IIoService IoService { get; }

        protected bool IncludeSubFolders { get; }

        public ISetChanges<AbsolutePath> ToLiveLinq()
        {
            return IoService.ToLiveLinq(_path, true, IncludeSubFolders, _pattern);
        }

        public int Count => this.AsEnumerable<TFileOrFolder>().Count();
        
        public void Dispose()
        {
        }

        public abstract IEnumerator<TFileOrFolder> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<AbsolutePath> IEnumerable<AbsolutePath>.GetEnumerator()
        {
            return this.AsEnumerable<TFileOrFolder>().Select(x => x.ExpectFileOrFolderOrMissingPath()).GetEnumerator();
        }
    }
}
