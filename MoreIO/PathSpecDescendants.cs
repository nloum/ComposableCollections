using System.Collections;
using System.Collections.Generic;

using System.Reactive.Linq;

using static LiveLinq.Set.Utility;
using System.IO;
using UtilityDisposables;
using LiveLinq.Set;

namespace MoreIO
{
    public class PathSpecDescendants : IReadOnlySet<PathSpec>
    {
        private readonly PathSpec _path;
        private readonly string _pattern;

        public PathSpecDescendants(PathSpec path, string pattern, bool includeSubdirectories)
        {
            _path = path;
            this._pattern = pattern;
            IncludeSubdirectories = includeSubdirectories;
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
                    SetChange(LiveLinq.Core.CollectionChangeType.Remove, rename.OldFullPath.ToPathSpec(_path.Flags).Value),
                    SetChange(LiveLinq.Core.CollectionChangeType.Add, rename.FullPath.ToPathSpec(_path.Flags).Value)
                    }.ToObservable())
                    .Merge()
                    .Subscribe(observer);
                var deletions = Observable.FromEvent<FileSystemEventArgs>(handler => _watcher.Deleted += (_, evt) => handler(evt), handler => _watcher.Deleted -= (_, evt) => handler(evt))
                    .Publish()
                    .RefCount()
                    .Select(deletion => SetChange(LiveLinq.Core.CollectionChangeType.Remove, deletion.FullPath.ToPathSpec(_path.Flags).Value))
                    .Subscribe(observer);
                var creations = Observable.FromEvent<FileSystemEventArgs>(handler => _watcher.Created += (_, evt) => handler(evt), handler => _watcher.Created -= (_, evt) => handler(evt))
                    .Publish()
                    .RefCount()
                    .Select(creation => SetChange(LiveLinq.Core.CollectionChangeType.Add, creation.FullPath.ToPathSpec(_path.Flags).Value))
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
                var subPath = fse.FullName.ToPathSpec(_path.Flags).Value;
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
