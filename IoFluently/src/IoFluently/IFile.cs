using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleMonads;
using TreeLinq;

namespace IoFluently
{
    public partial interface IHasAbsolutePath
    {
        AbsolutePath Path { get; }
        IIoService IoService => Path.IoService;
    }

    public partial class FileOrMissingPath : SubTypesOf<IHasAbsolutePath>.Either<File, MissingPath>, IHasAbsolutePath
    {
        public FileOrMissingPath(File item1) : base(item1)
        {
        }

        public FileOrMissingPath(MissingPath item3) : base(item3)
        {
        }

        public FileOrMissingPath(SubTypesOf<IHasAbsolutePath>.Either<File, MissingPath> other) : base(other)
        {
        }

        public FileOrMissingPath(IHasAbsolutePath item) : base(item)
        {
        }

        public override string ToString()
        {
            return Path.ToString();
        }

        public File ExpectFile()
        {
            if (Item1 == default)
            {
                throw new InvalidOperationException($"Expected {Path} to be a file but it was a {Path.Type}");
            }
            return Item1;
        }

        public MissingPath ExpectMissingPath()
        {
            if (Item2 == default)
            {
                throw new InvalidOperationException($"Expected {Path} to be a missing path but it was a {Path.Type}");
            }
            return Item2;
        }

        public AbsolutePath Path => Value.Path;
        public IIoService IoService => Path.IoService;
    }
    
    public partial class FileOrFolder : SubTypesOf<IHasAbsolutePath>.Either<File, Folder>, IHasAbsolutePath
    {
        public FileOrFolder(File item1) : base(item1)
        {
        }

        public FileOrFolder(Folder item3) : base(item3)
        {
        }

        public override string ToString()
        {
            return Path.ToString();
        }

        public FileOrFolder(SubTypesOf<IHasAbsolutePath>.Either<File, Folder> other) : base(other)
        {
        }

        public FileOrFolder(IHasAbsolutePath item) : base(item)
        {
        }

        public File ExpectFile()
        {
            if (Item1 == default)
            {
                throw new InvalidOperationException($"Expected {Path} to be a file but it was a {Path.Type}");
            }
            return Item1;
        }

        public Folder ExpectFolder()
        {
            if (Item2 == default)
            {
                throw new InvalidOperationException($"Expected {Path} to be a folder but it was a {Path.Type}");
            }
            return Item2;
        }

        public AbsolutePath Path => Value.Path;
        public IIoService IoService => Path.IoService;
    }

    public partial class FolderOrMissingPath : SubTypesOf<IHasAbsolutePath>.Either<Folder, MissingPath>, IHasAbsolutePath
    {
        public FolderOrMissingPath(Folder item1) : base(item1)
        {
        }

        public FolderOrMissingPath(MissingPath item3) : base(item3)
        {
        }

        public override string ToString()
        {
            return Path.ToString();
        }

        public FolderOrMissingPath(SubTypesOf<IHasAbsolutePath>.Either<Folder, MissingPath> other) : base(other)
        {
        }

        public FolderOrMissingPath(IHasAbsolutePath item) : base(item)
        {
        }

        public Folder ExpectFolder()
        {
            if (Item1 == default)
            {
                throw new InvalidOperationException($"Expected {Path} to be a folder but it was a {Path.Type}");
            }
            return Item1;
        }

        public MissingPath ExpectMissingPath()
        {
            if (Item2 == default)
            {
                throw new InvalidOperationException($"Expected {Path} to be a missing path but it was a {Path.Type}");
            }
            return Item2;
        }

        public AbsolutePath Path => Value.Path;
        public IIoService IoService => Path.IoService;
    }
    
    public partial class File : IHasAbsolutePath
    {
        public File(AbsolutePath path)
        {
            Path = path;
            if (!Path.IsFile)
            {
                throw new IOException($"{Path} is not a file");
            }
        }

        public override string ToString()
        {
            return Path.ToString();
        }

        public FileOrFolder ExpectFileOrFolder()
        {
            return new(this);
        }
        
        public FileOrMissingPath ExpectFileOrMissingPath()
        {
            return new(this);
        }
        
        public AbsolutePath ExpectFileOrFolderOrMissingPath()
        {
            return Path;
        }
        
        public AbsolutePath Path { get; }
        public IIoService IoService => Path.IoService;
    }

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
        public AbsolutePathDescendants GetDescendants(string pattern) => new AbsolutePathDescendants(Path, pattern, IoService);

        public AbsolutePathDescendants Descendants => new AbsolutePathDescendants(Path, null, IoService);

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