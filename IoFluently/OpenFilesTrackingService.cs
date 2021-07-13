using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using UtilityDisposables;

namespace IoFluently
{
    /// <summary>
    /// Standard implementation of <see cref="IOpenFilesTrackingService"/>
    /// </summary>
    public class OpenFilesTrackingService : IOpenFilesTrackingService
    {
        private object _lock = new object();
        private readonly Dictionary<AbsolutePath, Dictionary<Guid, object>> _openFiles = new Dictionary<AbsolutePath, Dictionary<Guid, object>>();

        /// <inheritdoc />
        public bool IsEnabled { get; }

        /// <summary>
        /// Creates a service for tracking open files.
        /// </summary>
        /// <param name="isEnabled">When this service is created is the only time you can enable or disable open-files tracking,
        /// for performance reasons. Use this parameter to enable or disable open-files tracking.</param>
        public OpenFilesTrackingService(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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