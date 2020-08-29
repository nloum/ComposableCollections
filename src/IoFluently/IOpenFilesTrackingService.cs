using System;
using System.Collections.Generic;

namespace IoFluently
{
    public interface IOpenFilesTrackingService
    {
         bool IsEnabled { get; }
        IDisposable TrackOpenFile(AbsolutePath absolutePath, Func<object> tag);
        IEnumerable<object> GetTagsForOpenFile(AbsolutePath absolutePath);
    }
}