using System;

namespace IoFluently.SystemTextJson
{
    public class AbsolutePathJsonDto
    {
        public string Value { get; init; }
        public PathType Type { get; init; }
        public string FileSize { get; init; }
        public bool? ReadOnly { get; init; }
        public DateTimeOffset? CreationTime { get; init; }
        public DateTimeOffset? LastAccessTime { get; init; }
        public DateTimeOffset? LastWriteTime { get; init; }
    }
}