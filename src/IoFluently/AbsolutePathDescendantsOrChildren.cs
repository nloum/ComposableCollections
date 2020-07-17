using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Text;
using LiveLinq;
using LiveLinq.Core;
using LiveLinq.Dictionary;
using LiveLinq.Set;
using ReactiveProcesses;
using UtilityDisposables;

namespace IoFluently
{
    public abstract class AbsolutePathDescendantsOrChildren : IReadOnlyObservableSet<AbsolutePath>
    {
        protected readonly AbsolutePath _path;
        protected readonly string _pattern;

        protected AbsolutePathDescendantsOrChildren(AbsolutePath path, string pattern, bool includeSubFolders, IIoService ioService)
        {
            _path = path;
            _pattern = pattern;
            IncludeSubFolders = includeSubFolders;
            IoService = ioService;
        }

        public IIoService IoService { get; }

        protected bool IncludeSubFolders { get; }

        public ISetChanges<AbsolutePath> ToLiveLinq()
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

        public abstract IEnumerator<AbsolutePath> GetEnumerator();

        protected IEnumerable<AbsolutePath> GetChildren(AbsolutePath parent)
        {
            var parentString = parent.ToString();
            if (Directory.Exists(parentString))
            {
                return Directory.GetFileSystemEntries(parentString).Select(x => IoService.ParseAbsolutePath(x, _path.Flags));
            }
            
            return Enumerable.Empty<AbsolutePath>();
        }
    }
}
