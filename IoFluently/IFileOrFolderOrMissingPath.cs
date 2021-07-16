using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using SimpleMonads;

namespace IoFluently
{
    public partial interface IFileOrFolderOrMissingPath : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFilePath, IFolderPath, IMissingPath>
    {
        /// <summary>
        /// The TreeLinq absolute path that this object represents
        /// </summary>
        public IReadOnlyList<string> Components { get; }

        /// <summary>
        /// Indicates whether or not the absolute path is case sensitive
        /// </summary>
        public bool IsCaseSensitive { get; }
        
        /// <summary>
        /// Indicates what the directory separator is for this absolute path (e.g., '/' or '\') 
        /// </summary>
        public string DirectorySeparator { get; }
        
        /// <summary>
        /// The IIoService that is used for this absolute path
        /// </summary>
        public IFileSystem FileSystem { get; }
        
        public string FullName { get; }
        
        public FileAttributes Attributes { get; set; }
        
        public FileOrFolderOrMissingPathAncestors Ancestors { get; }
        public bool CanBeSimplified { get; }
        public bool Exists { get; }
        public string Extension { get; }
        public bool HasExtension { get; }
        public bool IsFile { get; }
        public bool IsFolder { get; }
        public string Name { get; }
        public FolderPath Root { get; }
        public IMaybe<FileOrFolderOrMissingPath> Parent { get; }
        public PathType Type { get; }
        public FileOrFolderOrMissingPath WithoutExtension { get; }
    }
}