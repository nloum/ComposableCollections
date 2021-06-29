using System;
using SimpleMonads;

namespace IoFluently.SystemTextJson
{
    public static class Extensions
    {
        public static AbsolutePathJsonDto ToJsonDto(this AbsolutePath absolutePath)
        {
            return new AbsolutePathJsonDto()
            {
                Value = absolutePath,
                Type = absolutePath.GetPathType(),
                FileSize = absolutePath.TryFileSize().Select(x => x.ToString()).Otherwise((string) null),
                ReadOnly = absolutePath.TryIsReadOnly().ValueOrDefault,
                CreationTime = absolutePath.TryCreationTime().Select(x => (DateTimeOffset?)x).ValueOrDefault,
                LastAccessTime = absolutePath.TryLastAccessTime().Select(x => (DateTimeOffset?)x).ValueOrDefault,
                LastWriteTime = absolutePath.TryLastWriteTime().Select(x => (DateTimeOffset?)x).ValueOrDefault
            };
        }
    }
}