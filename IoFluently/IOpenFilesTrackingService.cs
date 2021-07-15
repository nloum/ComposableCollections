using System;
using System.Collections.Generic;

namespace IoFluently
{
    /// <summary>
    /// An object that allows IoFluently to track which files ar open by which parts of the code. Useful for tracking down
    /// bugs.
    /// </summary>
    public interface IOpenFilesTrackingService
    {
        /// <summary>
        /// There's a performance penalty to enabling open-files tracking, so it is disabled by default. This value indicates
        /// whether open files tracking is enabled or not.
        /// </summary>
        bool IsEnabled { get; }
        
        /// <summary>
        /// Tells the service that the specified path has been opened.
        /// </summary>
        /// <param name="fileOrFolderOrMissingPath">The path that has been opened</param>
        /// <param name="tag">A way of tagging this file being opened, so that when multiple parts of your program
        /// attempt to open the same file, you can tell which part of the code is opening the file based on the tag applied
        /// to each attempt to open the file.</param>
        /// <returns>An object that, when disposed of, tells the service that the path has been closed.</returns>
        IDisposable TrackOpenFile(FileOrFolderOrMissingPath fileOrFolderOrMissingPath, Func<object> tag);
        
        /// <summary>
        /// Determine which parts of the program currently have opened the specified path.
        /// </summary>
        /// <param name="fileOrFolderOrMissingPath">The path that may be open multiple times right now.</param>
        /// <returns>A list of tags that describe each of the parts of the code that have opened the specified path.</returns>
        IEnumerable<object> GetTagsForOpenFile(FileOrFolderOrMissingPath fileOrFolderOrMissingPath);
    }
}