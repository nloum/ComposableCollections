using System;
using SimpleMonads;
using UnitsNet;

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
                FileSize = absolutePath.Collapse(x => x.FileSize.ToString(), _ => null, _ => null),
                ReadOnly = absolutePath.Collapse(x => (bool?)x.IsReadOnly, _ => null, _ => null),
                CreationTime = absolutePath.Collapse(x => (DateTimeOffset?)x.CreationTime, _ => null, _ => null),
                LastAccessTime = absolutePath.Collapse(x => (DateTimeOffset?)x.LastAccessTime, _ => null, _ => null),
                LastWriteTime = absolutePath.Collapse(x => (DateTimeOffset?)x.LastWriteTime, _ => null, _ => null),
            };
        }
    }
}