using System;
using UnitsNet;

namespace IoFluently
{
    public partial interface IFilePath : IFileOrFolderPath, IFileOrMissingPath
    {
        public FilePathAncestors Ancestors { get; }
        public DateTimeOffset CreationTime { get; }
        public Information FileSize { get; }
        public bool IsReadOnly { get; }
        public DateTimeOffset LastAccessTime { get; }
        public DateTimeOffset LastWriteTime { get; }
        public FolderPath Parent { get; }
    }
}