using System;
using SimpleMonads;
using UnitsNet;

namespace IoFluently.SystemTextJson
{
    public static class Extensions
    {
        public static AbsolutePathJsonDto ToJsonDto(this IFileOrFolderOrMissingPath fileOrFolderOrMissingPath)
        {
            return new AbsolutePathJsonDto()
            {
                Value = fileOrFolderOrMissingPath.ToString(),
                Type = fileOrFolderOrMissingPath.Type,
                FileSize = fileOrFolderOrMissingPath.Collapse(x => x.FileSize.ToString(), _ => null, _ => null),
                ReadOnly = fileOrFolderOrMissingPath.Collapse(x => (bool?)x.IsReadOnly, _ => null, _ => null),
                CreationTime = fileOrFolderOrMissingPath.Collapse(x => (DateTimeOffset?)x.CreationTime, _ => null, _ => null),
                LastAccessTime = fileOrFolderOrMissingPath.Collapse(x => (DateTimeOffset?)x.LastAccessTime, _ => null, _ => null),
                LastWriteTime = fileOrFolderOrMissingPath.Collapse(x => (DateTimeOffset?)x.LastWriteTime, _ => null, _ => null),
            };
        }
    }
}