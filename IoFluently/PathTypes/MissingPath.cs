using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TreeLinq;

namespace IoFluently
{
    public partial class MissingPath : IHasAbsolutePath
    {
        public AbsolutePath Path { get; }
        public IIoService IoService => Path.IoService;

        public MissingPath(AbsolutePath path)
        {
            Path = path;
            if (Path.Exists)
            {
                throw new IOException($"{Path} should not exist");
            }
        }

        public override string ToString()
        {
            return Path.ToString();
        }

        public FileOrMissingPath ExpectFileOrMissingPath()
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
        public static AbsolutePath operator / (MissingPath absPath, string whatToAdd)
        {
            return absPath.Path / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (MissingPath absPath, IEnumerable<RelativePath> whatToAdd)
        {
            return absPath.Path / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (MissingPath absPath, Func<AbsolutePath, IEnumerable<RelativePath>> whatToAdd)
        {
            return absPath.Path / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePath operator / (MissingPath absPath, RelativePath whatToAdd)
        {
            return absPath.Path / whatToAdd;
        }

        /// <summary>
        /// Adds a subpath to all this relative path
        /// </summary>
        /// <param name="relPath">The relative path that will have a subpath added to it</param>
        /// <param name="whatToAdd">The subpath that will be added to this the relative path</param>
        /// <returns>A new RelativePath object that will have an additional subpath appended to it</returns>
        public static AbsolutePaths operator / (MissingPath absPath, IEnumerable<string> whatToAdd)
        {
            return absPath.Path / whatToAdd;
        }
    }
}