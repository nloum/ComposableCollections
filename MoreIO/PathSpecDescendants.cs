using System.Collections;
using System.Collections.Generic;

using System.Reactive.Linq;

using static LiveLinq.Set.Utility;
using System.IO;
using UtilityDisposables;
using LiveLinq.Set;

namespace MoreIO
{
    public class PathSpecDescendants : IReadOnlyObservableSet<PathSpec>
    {
        private readonly PathSpec _path;
        private readonly string _pattern;
        public IIoService IoService { get; }

        public PathSpecDescendants(PathSpec path, string pattern, bool includeSubdirectories, IIoService ioService)
        {
            _path = path;
            this._pattern = pattern;
            IncludeSubdirectories = includeSubdirectories;
            IoService = ioService;
        }

        protected bool IncludeSubdirectories { get; }

        public ISetChanges<PathSpec> ToLiveLinq()
        {
            return Observable.Create<ISetChange<PathSpec>>(observer =>
            {
                var _watcher = new FileSystemWatcher(_path.ToString())
                {
                    EnableRaisingEvents = true,
                    IncludeSubdirectories = IncludeSubdirectories,
                    Filter = _pattern
                };

                var renamings = Observable.FromEvent<RenamedEventArgs>(handler => _watcher.Renamed += (_, evt) => handler(evt), handler => _watcher.Renamed -= (_, evt) => handler(evt))
                    .Publish()
                    .RefCount()
                    .Select(rename => new[] {
                    SetChange(LiveLinq.Core.CollectionChangeType.Remove, IoService.ToPath(rename.OldFullPath, _path.Flags).Value),
                    SetChange(LiveLinq.Core.CollectionChangeType.Add, IoService.ToPath(rename.FullPath, _path.Flags).Value)
                    }.ToObservable())
                    .Merge()
                    .Subscribe(observer);
                var deletions = Observable.FromEvent<FileSystemEventArgs>(handler => _watcher.Deleted += (_, evt) => handler(evt), handler => _watcher.Deleted -= (_, evt) => handler(evt))
                    .Publish()
                    .RefCount()
                    .Select(deletion => SetChange(LiveLinq.Core.CollectionChangeType.Remove, IoService.ToPath(deletion.FullPath, _path.Flags).Value))
                    .Subscribe(observer);
                var creations = Observable.FromEvent<FileSystemEventArgs>(handler => _watcher.Created += (_, evt) => handler(evt), handler => _watcher.Created -= (_, evt) => handler(evt))
                    .Publish()
                    .RefCount()
                    .Select(creation => SetChange(LiveLinq.Core.CollectionChangeType.Add, IoService.ToPath(creation.FullPath, _path.Flags).Value))
                    .Subscribe(observer);

                foreach (var childPath in AsEnumerable())
                {
                    observer.OnNext(SetChange(LiveLinq.Core.CollectionChangeType.Add, childPath));
                }

                return new AnonymousDisposable(() =>
                {
                    renamings.Dispose();
                    deletions.Dispose();
                    creations.Dispose();
                    _watcher.Dispose();
                });
            }).ToLiveLinq();
        }
        
        private IEnumerable<PathSpec> AsEnumerable()
        {
            var directory = new DirectoryInfo(_path.ToString());

            foreach (var fse in directory.GetFileSystemInfos(_pattern))
            {
                var subPath = IoService.ToPath(fse.FullName, _path.Flags).Value;
                yield return subPath;
                if (IncludeSubdirectories)
                {
                    foreach(var descendant in subPath.Descendants())
                    {
                        yield return descendant;
                    }
                }
            }
        }

        public IEnumerator<PathSpec> GetEnumerator()
        {
            return AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
