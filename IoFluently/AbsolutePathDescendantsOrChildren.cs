using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using LiveLinq.Set;

namespace IoFluently
{
    public abstract class AbsolutePathDescendantsOrChildren : IObservableReadOnlySet<IFileOrFolderOrMissingPath>
    {
        protected readonly Folder<Folder> _path;
        protected readonly string _pattern;

        protected AbsolutePathDescendantsOrChildren(Folder<Folder> path, string pattern, bool includeSubFolders, IIoService ioService)
        {
            _path = path;
            _pattern = pattern;
            IncludeSubFolders = includeSubFolders;
            IoService = ioService;
        }

        public IIoService IoService { get; }

        protected bool IncludeSubFolders { get; }

        public ISetChanges<IFileOrFolderOrMissingPath> ToLiveLinq()
        {
            return IoService.ToLiveLinq(_path, true, IncludeSubFolders, _pattern);
        }

        public int Count => this.AsEnumerable().Count();
        
        public void Dispose()
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract IEnumerator<IFileOrFolderOrMissingPath> GetEnumerator();
    }
}
