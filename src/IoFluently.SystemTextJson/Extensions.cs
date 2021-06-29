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
                Type = absolutePath.Type,
                FileSize = absolutePath.FileSize?.ToString(),
                ReadOnly = absolutePath.IsReadOnly,
                CreationTime = absolutePath.CreationTime,
                LastAccessTime = absolutePath.LastAccessTime,
                LastWriteTime = absolutePath.LastWriteTime
            };
        }
    }
}