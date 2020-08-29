using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using UtilityDisposables;

namespace IoFluently
{
    public class OpenFilesTrackingService : IOpenFilesTrackingService
    {
        private object _lock = new object();
        private readonly Dictionary<AbsolutePath, Dictionary<Guid, object>> _openFiles = new Dictionary<AbsolutePath, Dictionary<Guid, object>>();
        
        public bool IsEnabled { get; }

        public OpenFilesTrackingService(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        public IDisposable TrackOpenFile(AbsolutePath absolutePath, Func<object> tag)
        {
            if (IsEnabled)
            {
                lock (_lock)
                {
                    var id = Guid.NewGuid();
                    
                    if (!_openFiles.TryGetValue(absolutePath, out var tags))
                    {
                        tags = new Dictionary<Guid, object>();
                        _openFiles[absolutePath] = tags;
                    }

                    tags.Add(id, tag());
                    
                    return new AnonymousDisposable(() =>
                    {
                        lock (_lock)
                        {
                            tags.Remove(id);
                        }
                    });
                }
            }
            
            return Disposable.Empty;
        }

        public IEnumerable<object> GetTagsForOpenFile(AbsolutePath absolutePath)
        {
            if (IsEnabled)
            {
                lock (_lock)
                {
                    if (_openFiles.TryGetValue(absolutePath, out var tags))
                    {
                        return tags.Values;
                    }
                }
            }
            
            return Enumerable.Empty<object>();
        }
    }
}