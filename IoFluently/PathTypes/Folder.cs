using System;
using System.Collections.Generic;

namespace IoFluently
{
    public partial class Folder : IHasAbsolutePath
    {
        public AbsolutePath Path { get; }
        public IIoService IoService => Path.IoService;

        public Folder(AbsolutePath path)
        {
            Path = path;
        }
        
        public override string ToString()
        {
            return Path.ToString();
        }

        /// <summary>
        /// Returns the files and folders that this folder contains, but not anything else. This will not return files or folders
        /// that are nested deeper. For example, if this folder contains a folder called Level1, and Level1 contains another
        /// folder called Level2, then this method will return only Level1, not Level2 or anything else in Level1.
        /// </summary>
        /// <param name="pattern">The string pattern that files or folders must match to be included in the return value.
        /// If this is null, then all files and folders in this folder are returned.</param>
        /// <returns>An object representing the children files and folders of this folder.</returns>
        public AbsolutePathChildren GetChildren(string pattern) => new AbsolutePathChildren(this, pattern, IoService);

        public AbsolutePathChildren Children => new AbsolutePathChildren(this, null, IoService);

        /// <summary>
        /// Returns the files and folders that this folder contains, and the files and folders that they contain, etc.
        /// This will return ALL files and folders that are nested deeper as well. For example, if this folder contains
        /// a folder called Level1, and Level1 contains another folder called Level2, then this method will return both
        /// Level1, Level2, and anything else in Level1.
        /// </summary>
        /// <param name="pattern">The string pattern that files or folders must match to be included in the return value.
        /// If this is null, then all files and folders in this folder are returned.</param>
        /// <returns>An object representing the descendant files and folders of this folder.</returns>
        public AbsolutePathDescendants GetDescendants(string pattern) => new AbsolutePathDescendants(this, pattern, IoService);

        public AbsolutePathDescendants Descendants => new AbsolutePathDescendants(this, null, IoService);

        public FileOrFolder ExpectFileOrFolder()
        {
            return new(this);
        }
        
        public FolderOrMissingPath ExpectFolderOrMissingPath()
        {
            return new(this);
        }
        
        public AbsolutePath ExpectFileOrFolderOrMissingPath()
        {
            return Path;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePath operator / (Folder absPath, string whatToAdd)
        {
            return absPath.Path / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (Folder absPath, IEnumerable<RelativePath> whatToAdd)
        {
            return absPath.Path / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (Folder absPath, Func<AbsolutePath, IEnumerable<RelativePath>> whatToAdd)
        {
            return absPath.Path / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePath operator / (Folder absPath, RelativePath whatToAdd)
        {
            return absPath.Path / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (Folder absPath, IEnumerable<string> whatToAdd)
        {
            return absPath.Path / whatToAdd;
        }
    }
}