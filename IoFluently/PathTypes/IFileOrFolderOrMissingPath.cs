using System;
using System.Collections.Generic;
using SimpleMonads;
using TreeLinq;

namespace IoFluently
{
    public partial interface IAbsolutePath
    {
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
        
        /// <summary>
        /// The TreeLinq absolute path that this object represents
        /// </summary>
        public IReadOnlyList<string> Components { get; }

        /// <summary>
        /// The file or directory name, a.k.a the last component in the path
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The file extension, if there is one, including the dot.
        /// </summary>
        IMaybe<string> Extension { get; }
    }

    public interface IFileOrFolderOrMissingPath : IFileOrFolderOrMissingPath<File, Folder, MissingPath>
    {
    }

    public interface IFileOrFolderOrMissingPath<out TFile, out TFolder, out TMissingPath> : IEither<TFile, TFolder, TMissingPath>, IComparable, IAbsolutePath
        where TFile : File where TFolder : Folder where TMissingPath : MissingPath
    {
        
    }
}