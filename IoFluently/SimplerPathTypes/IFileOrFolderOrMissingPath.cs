using System.Collections.Generic;
using SimpleMonads;

namespace IoFluently
{
    public interface IFileOrFolderOrMissingPath : SubTypesOf<IFileOrFolderOrMissingPath>.IEither<IFile, IFolder, IMissingPath>
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
        public IIoService IoService { get; }
    }
}