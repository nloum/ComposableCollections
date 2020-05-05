using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using LiveLinq;
using LiveLinq.Core;
using LiveLinq.Set;
using UtilityDisposables;
using static LiveLinq.Utility;

namespace MoreIO
{
    public class AbsolutePathDescendants : IReadOnlyObservableSet<AbsolutePath>
    {
        private readonly AbsolutePath _path;
        private readonly string _pattern;

        public AbsolutePathDescendants(AbsolutePath path, string pattern, bool includeSubdirectories, IIoService ioService)
        {
            _path = path;
            _pattern = pattern;
            IncludeSubdirectories = includeSubdirectories;
            IoService = ioService;
        }

        public IIoService IoService { get; }

        protected bool IncludeSubdirectories { get; }

        public ISetChanges<AbsolutePath> ToLiveLinq()
        {
            return Observable.Create<ISetChange<AbsolutePath>>(observer =>
            {
                var _watcher = new FileSystemWatcher(_path.ToString())
                {
                    EnableRaisingEvents = true,
                    IncludeSubdirectories = IncludeSubdirectories,
                    Filter = _pattern
                };

                var renamings = Observable
                    .FromEvent<RenamedEventArgs>(handler => _watcher.Renamed += (_, evt) => handler(evt),
                        handler => _watcher.Renamed -= (_, evt) => handler(evt))
                    .Publish()
                    .RefCount()
                    .Select(rename => new[]
                    {
                        SetChange(CollectionChangeType.Remove, IoService.ToPath(rename.OldFullPath, _path.Flags).Value),
                        SetChange(CollectionChangeType.Add, IoService.ToPath(rename.FullPath, _path.Flags).Value)
                    }.ToObservable())
                    .Merge()
                    .Subscribe(observer);
                var deletions = Observable.FromEvent<FileSystemEventArgs>(
                        handler => _watcher.Deleted += (_, evt) => handler(evt),
                        handler => _watcher.Deleted -= (_, evt) => handler(evt))
                    .Publish()
                    .RefCount()
                    .Select(deletion => SetChange(CollectionChangeType.Remove,
                        IoService.ToPath(deletion.FullPath, _path.Flags).Value))
                    .Subscribe(observer);
                var creations = Observable.FromEvent<FileSystemEventArgs>(
                        handler => _watcher.Created += (_, evt) => handler(evt),
                        handler => _watcher.Created -= (_, evt) => handler(evt))
                    .Publish()
                    .RefCount()
                    .Select(creation => SetChange(CollectionChangeType.Add,
                        IoService.ToPath(creation.FullPath, _path.Flags).Value))
                    .Subscribe(observer);

                foreach (var childPath in AsEnumerable())
                    observer.OnNext(SetChange(CollectionChangeType.Add, childPath));

                return new AnonymousDisposable(() =>
                {
                    renamings.Dispose();
                    deletions.Dispose();
                    creations.Dispose();
                    _watcher.Dispose();
                });
            }).ToLiveLinq();
        }

        public IEnumerator<AbsolutePath> GetEnumerator()
        {
            return AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<AbsolutePath> AsEnumerable()
        {
            var directory = new DirectoryInfo(_path.ToString());

            foreach (var fse in directory.GetFileSystemInfos(_pattern))
            {
                var subPath = IoService.ToPath(fse.FullName, _path.Flags).Value;
                yield return subPath;
                if (IncludeSubdirectories)
                    foreach (var descendant in subPath.Descendants())
                        yield return descendant;
            }
        }
    }
}